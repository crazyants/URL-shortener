using System.Collections.Generic;
using urlshortner.Entities.Models;

namespace urlshortener.Models
{
    public class ShortnerViewModel
    {
        public URLViewModel SingleURL { get; set; }
        public IEnumerable<URLModel> ListURL { get; set; }
    }
}