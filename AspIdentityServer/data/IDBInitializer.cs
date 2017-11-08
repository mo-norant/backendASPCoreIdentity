using AspIdentityServer.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIdentityServer
{
    public interface IDbInitializer
    {
        Task Initialize(ApplicationDBcontext context);
    }
}
