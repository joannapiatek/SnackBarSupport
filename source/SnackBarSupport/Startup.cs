﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SnackBarSupport.Startup))]

namespace SnackBarSupport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
