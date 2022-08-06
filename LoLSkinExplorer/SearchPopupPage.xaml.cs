using LoLSkinExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoLSkinExplorer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPopupPage : Popup
    {
        public SearchPopupPage()
        {
            InitializeComponent();
        }
        
    }
}