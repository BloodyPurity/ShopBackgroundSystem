﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthenticationWithRSAMD5.Startup))]
namespace AuthenticationWithRSAMD5
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
