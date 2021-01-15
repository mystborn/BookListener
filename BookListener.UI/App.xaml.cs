using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BookListener.UI
{
    public partial class App : PrismApplication
    {
        // RegisterTypes function is here

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override Window CreateShell()
        {
            var window = Container.Resolve<MainWindow>();
            return window;
        }
    }
}
