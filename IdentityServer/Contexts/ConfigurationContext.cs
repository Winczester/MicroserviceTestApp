using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Contexts
{
    public class ConfigurationContext : ConfigurationDbContext
    {
        public ConfigurationContext(DbContextOptions<ConfigurationDbContext> contextOptions, ConfigurationStoreOptions storeOptions) : base(contextOptions, storeOptions) { }

        
    }
}
