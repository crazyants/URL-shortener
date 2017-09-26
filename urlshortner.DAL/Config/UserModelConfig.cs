using System.Data.Entity.ModelConfiguration;
using urlshortner.Entities.Models;

namespace urlshortner.DAL.Config
{
    public class UserModelConfig : EntityTypeConfiguration<UserModel>
    {
        public UserModelConfig ()
        {
            ToTable("Users");
            Property(u => u.Email).IsRequired();
            Property(u => u.Password).IsRequired();
            Property(u => u.Nickname).IsRequired();
        }
    }
}
