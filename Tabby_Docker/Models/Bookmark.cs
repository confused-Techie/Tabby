using System;
using System.ComponentModel.DataAnnotations;

namespace Tabby_Docker.Models
{
    public class Bookmark
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        [Display(Name = "Website Name")]
        public string SiteName { get; set; }
        [Display(Name = "Date Added to Tabby")]
        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }
        [Display(Name = "Favicon URL")]
        public string FaviconLoc { get; set; }

    }
}
