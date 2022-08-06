using LoLSkinExplorer.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LoLSkinExplorer
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SkinsPage),typeof(SkinsPage));
        }

        
    }
}
