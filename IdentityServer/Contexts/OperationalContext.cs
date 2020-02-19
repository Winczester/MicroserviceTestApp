using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Contexts
{
    public class OperationalContext : PersistedGrantDbContext
    { 

        public OperationalContext(DbContextOptions<PersistedGrantDbContext> contextOptions, OperationalStoreOptions storeOptions) : base(contextOptions, storeOptions) { }

    }
}
