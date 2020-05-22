using System;
using Stylet;
using StyletIoC;
using QrCodeTools.Pages;

namespace QrCodeTools
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            // Configure the IoC container in here
            //builder.
        }

        protected override void Configure()
        {
            // Perform any other configuration before the application starts
        }
    }
}
