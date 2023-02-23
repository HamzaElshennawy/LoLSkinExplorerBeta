using LoLSkinExplorer.Models;
using LoLSkinExplorer.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoLSkinExplorer.Views
{
    public partial class HomePage : ContentPage
    {
        public MvvmHelpers.ObservableRangeCollection<Skin> Skins { get; set; }
        public HomePage()
        {
            InitializeComponent();
            Skins = new MvvmHelpers.ObservableRangeCollection<Skin>();

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //Navigation.ShowPopup(new SearchPopupPage());
            if (!SearchEntryBox.IsVisible)
            {
                SearchEntryBox.IsVisible = true;

            }
            else
            {
                SearchEntryBox.IsVisible = false;
            }

        }


        private void ListViewomePage_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }


        private void SearchEntryBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as SkinPageViewModel;
            ListViewomePage.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                ListViewomePage.ItemsSource = _container.Champions;
            }
            else
            {
                if (e.NewTextValue[0].ToString() == e.NewTextValue[0].ToString().ToUpper())
                {
                    ListViewomePage.ItemsSource = _container.Champions.Where(i => i.ChampionAlias.ToLower().Contains(e.NewTextValue));
                }
                else
                {
                    ListViewomePage.ItemsSource = _container.Champions.Where(i => i.ChampionAlias.ToLower().Contains(e.NewTextValue));
                }
            }
            ListViewomePage.EndRefresh();
        }

        private void SearchEntryBox_Unfocused(object sender, FocusEventArgs e)
        {
            //SearchEntryBox.IsVisible = false;
        }

        private async void ListViewomePage_ItemSelected_1Async(object sender, SelectedItemChangedEventArgs e)
        {
            var champ = ((ListView)sender).SelectedItem as Champion;
            if (champ == null)
                return;

            await Task.WhenAll(
            ((ListView)sender).TranslateTo(-300, 0, 1500, Easing.Linear),
            Navigation.PushAsync(new ChampionPage(champ), true)
            );
            await ((ListView)sender).TranslateTo(0, 0, 0, Easing.Linear);

        }




    }
}