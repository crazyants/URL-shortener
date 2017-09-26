using System.Data.Entity.ModelConfiguration;
using urlshortner.Entities.Models;

namespace urlshortner.DAL.Config
{
    public class URLModelConfig : EntityTypeConfiguration<URLModel>
    {
        public URLModelConfig()
        {
            ToTable("Links");
            Property(u => u.LongURL).IsRequired();
        }
    }
}
