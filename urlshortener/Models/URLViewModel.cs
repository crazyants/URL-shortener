using System.ComponentModel.DataAnnotations;

namespace urlshortener.Models
{
    public class URLViewModel
    {
        [RegularExpression(@"^[\S]*$", ErrorMessage = "Spaces in URL are not allowed")]
        public string LongURL { get; set; }
    }
}