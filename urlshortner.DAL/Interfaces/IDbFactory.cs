using System;

namespace urlshortner.DAL.Interfaces
{
    public interface IDbFactory : IDisposable
    {
        DatabaseContext Init();
    }
}
