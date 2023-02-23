using LoLSkinExplorer.Views;
using Xamarin.Forms;

namespace LoLSkinExplorer
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ChampionPage), typeof(ChampionPage));
        }


    }
}
