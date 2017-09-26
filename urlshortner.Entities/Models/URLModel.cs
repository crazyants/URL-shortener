namespace urlshortner.Entities.Models
{
    public class URLModel
    {
        public int Id { get; set; }
        public string LongURL { get; set; }
        public string ShortURL { get; set; }
        public int ClickCount { get; set; }

        public int UserId { get; set; }
        public UserModel UserCreator { get; set; }
    }
}
