using System.Collections.Generic;

namespace urlshortner.Entities.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public virtual List<URLModel> URLs { get; set; }
    }
}
