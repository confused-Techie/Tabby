using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tabby_Docker.Models
{
    public class Bookmark
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string SiteName { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }

    }
}
