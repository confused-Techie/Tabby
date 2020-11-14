using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tabby_Docker.Models;

namespace Tabby_Docker.Pages
{
    public class apiModel : PageModel
    {
        private readonly Tabby_Docker.Data.Tabby_DockerContext _DockerContext;
        public apiModel(Tabby_Docker.Data.Tabby_DockerContext context)
        {
            _DockerContext = context;
        }

        [BindProperty]
        public Bookmark Bookmark { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string? type, string? bm)
        {
            if (type is null)
            {
                return NotFound();
            }

            if (type == "old")
            {
                if (id == null)
                {
                    return NotFound();
                }
                Bookmark = await _DockerContext.Bookmark.FirstOrDefaultAsync(mbox => mbox.ID == id);
                if (Bookmark == null)
                {
                    return NotFound();
                }
                return new JsonResult(Bookmark);
            }
            if (type == "new")
            {
                if (bm is null)
                {
                    return NotFound();
                }
                else
                {
                    var linkAnswer = await OutgoingAPI(bm);
                    return new JsonResult(linkAnswer);
                }
            }
            else
            {
                return NotFound();
            }
        }

        private async Task<Object> OutgoingAPI(string providedURI)
        {
            using (var client = new HttpClient())
            {
                var uri = providedURI;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                HttpResponseMessage response;

                try
                {
                    response = await client.GetAsync(uri);
                }
                catch (Exception ex)
                {
                    //Even though this would only catch if the Bookmark isn't https:// || http:// 
                    //it will still be saved with Bookmark First! thinking.
                    //Generally this will be triggered by something like chrome://settings
                    System.Console.WriteLine("There was a problem processing the URI: " + ex);
                    System.Console.WriteLine("Attempting to Save anyway with no data...");
                    var noMetaReturn = await noMetaResult(uri);
                    return new JsonResult(noMetaReturn).Value;
                    //.Value is used to only return the value of the JsonResult which in this case is the return
                    //statement of noMetaResult
                    //return "There was a problem processing the URI: " + ex;
                }

                if (response.IsSuccessStatusCode)
                {
                    //The API was successfully able to connect to the URI, and grab its content. Now time to parse

                    //when assigning the OGP values, we will assyme sites follow required formats by default
                    string rawTitle = "";
                    string rawUrl = "";
                    string rawSiteName = "";
                    string rawDescription = "";

                    string rawHTML = await response.Content.ReadAsStringAsync();
                    //for this next part we will use HtmlAgilityPack to make parsing the OGP data easy.
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(rawHTML);
                    try
                    {
                        rawTitle = doc.DocumentNode.SelectSingleNode("//meta[@property='og:title']")?.GetAttributeValue("Content", null);
                        rawUrl = doc.DocumentNode.SelectSingleNode("//meta[@property='og:url']")?.GetAttributeValue("Content", null);
                        rawSiteName = doc.DocumentNode.SelectSingleNode("//meta[@property='og:site_name']")?.GetAttributeValue("Content", null);
                        rawDescription = doc.DocumentNode.SelectSingleNode("//meta[@property='og:description']")?.GetAttributeValue("Content", null);
                        //System.Console.WriteLine(doc.DocumentNode.SelectSingleNode("//"))
                    }
                    catch (Exception ex)
                    {
                        //Since even if some parsing data doesn't exist HTMLAgilityPack fails gracefully returning
                        //null values, this will be left in the case of malformed data, possibly indicating a bad request
                        System.Console.WriteLine("There was a problem parsing the data: " + ex);
                        return "There was a problem parsing the data: " + ex;
                    }
                    //now time to check that each value, actually has value
                    if (String.IsNullOrWhiteSpace(rawTitle))
                    {
                        //if no og:title switch to twitter:title
                        rawTitle = doc.DocumentNode.SelectSingleNode("//meta[@property='twitter:title']")?.GetAttributeValue("Content", null);
                        if (String.IsNullOrWhiteSpace(rawTitle))
                        {
                            //if no og:title or twitter:title switch to html Title
                            rawTitle = doc.DocumentNode.SelectSingleNode("//title")?.InnerText;
                            if (String.IsNullOrWhiteSpace(rawTitle))
                            {
                                //if no og:title, twitter:title, html title, use UNKNOWN
                                rawTitle = "Unknown";
                            }
                        }
                    }

                    if (String.IsNullOrWhiteSpace(rawUrl))
                    {

                        //if no og:url set to provided URI
                        rawUrl = uri;
                    }

                    if (String.IsNullOrWhiteSpace(rawSiteName))
                    {
                        //if no og:site_name the url is used
                        rawSiteName = rawUrl;
                    }

                    if (String.IsNullOrWhiteSpace(rawDescription))
                    {
                        //if no og:description twitter:description will be used
                        rawDescription = doc.DocumentNode.SelectSingleNode("//meta[@property='twitter:description']")?.GetAttributeValue("Content", null);
                        if (String.IsNullOrWhiteSpace(rawDescription))
                        {
                            //if no og:desription, twitter:description use Uknown
                            rawDescription = "Unknown";
                        }
                    }

                    //now time to give this to the Database
                    //and while doing so check applicable values for HTMLCharacterEntities
                    try
                    {
                        //while this uses google to find the favicon, if google is down or not accesible, the favicon
                        //will just not be rendered on the bookmark page

                        _DockerContext.Bookmark.AddRange(
                            new Bookmark
                            {
                                Title = HtmlEntityChart(HtmlLeftovers(rawTitle)),
                                Description = HtmlEntityChart(HtmlLeftovers(rawDescription)),
                                URL = rawUrl,
                                SiteName = rawSiteName,
                                DateAdded = DateTime.Now,
                                FaviconLoc = "https://www.google.com/s2/favicons?domain="+rawUrl
                            });
                        try
                        {
                            _DockerContext.SaveChanges();
                            System.Console.WriteLine("Successfully saving new BookMark");
                            return "Success";
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine("There was a problem saving the Bookmark to the Database: " + ex);
                            return "There was a problem saving the Bookmark to the Database: " + ex;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("Error Writing to Database: " + ex);
                        return "Error Writing to Database: " + ex;
                    }
                }
                else
                {
                    System.Console.WriteLine("There was a problem accessing the URI: " + response.StatusCode + "-" + response.ReasonPhrase);
                    //return "There was a problem accessing the URRI: " + response.StatusCode + "-" + response.ReasonPhrase;
                    System.Console.WriteLine("Attempting to save Bookmark anyway...");
                    var noAccessReturn = await noMetaResult(uri);
                    return new JsonResult(noAccessReturn).Value;
                }
            }
        }

        private async Task<Object> noMetaResult(string noMetaURL)
        {
            //I can use Uri to easily and quickly get host name
            Uri baseUri = new Uri(noMetaURL);

            //since this is used for both noStandard URL's and for noScrapingURL's the description will differ depending
            var noMetaDescription = "";
            if (baseUri.Scheme == "http" || baseUri.Scheme == "https")
            {
                noMetaDescription = "No Description Available.";
            } else
            {
                noMetaDescription = "No Description Available. You may have to copy this link and paste it into the Address Bar because it uses '"+ baseUri.Scheme+ "' instead of 'https' or 'http'.";
            }
            //try saving this blank data to the DB
            try
            {
                _DockerContext.Bookmark.AddRange(
                    new Bookmark
                    {
                        //The warning to copy/paste is provided since this would normally be called after
                        //realizing this is a none standard url
                        Title = baseUri.Host,
                        Description = noMetaDescription,
                        URL = noMetaURL,
                        SiteName = noMetaURL,
                        DateAdded = DateTime.Now,
                        FaviconLoc = "https://www.google.com/s2/favicons?domain="+noMetaURL

                    });

                try
                {
                    _DockerContext.SaveChanges();
                    System.Console.WriteLine("Successfully saving new Bookmark");
                    return "Success";
                } 
                catch (Exception ex)
                {
                    System.Console.WriteLine("There was an error saving the bookmark to the Database: " + ex);
                    return "There was an error saving the bookmark to the Database: " + ex;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error Writing to database: " + ex);
                return "Error Writing to database: " + ex;
            }
        }

        
        private static string HtmlEntityChart(string origString)
        {
            String finalString = "";
            String semiString = "";
            String aposString = "";
            String colonString = "";
            String ltString = "";
            String gtString = "";
            String ampString = "";
            //check for character entity ;
            semiString = origString.Replace("&semi;", ";").Replace("&#x0003B;", ";").Replace("&#x3B", ";").Replace("&#59;", ";");
            //check for entity '
            aposString = semiString.Replace("&apos;", "'").Replace("&#x00027;", "'").Replace("&#x27;", "'").Replace("&#39;", "'");
            colonString = aposString.Replace("&colon;", ":").Replace("&#x0003A;", ":").Replace("&#x3A;", ":").Replace("&#58;", ":");
            ltString = colonString.Replace("&lt;", "<").Replace("&LT;", "<").Replace("&#x0003C;", "<").Replace("&#x3C;", "<").Replace("&#60;", "<");
            gtString = ltString.Replace("&gt;", ">").Replace("&GT;", ">").Replace("&#x0003E;", ">").Replace("&#x3E;", ">").Replace("&#62;", ">");
            ampString = gtString.Replace("&amp;", "&").Replace("&AMP;", "&").Replace("&#x00026;", "&").Replace("&#x26;", "&").Replace("&#38;", "&");

            finalString = ampString;
            return finalString;
        }

        private static string HtmlLeftovers(string origString)
        {
            String finalString = "";
            String boldString = "";
            String pString = "";
            String hString = "";

            boldString = origString.Replace("<b>", "").Replace("</b>", "");
            pString = boldString.Replace("<p>", "").Replace("</p>", "");
            hString = pString.Replace("<h1>", "").Replace("/h1>", "").Replace("<h2>", "").Replace("</h2>", "");

            finalString = hString;
            return finalString;
        }
    }
}
