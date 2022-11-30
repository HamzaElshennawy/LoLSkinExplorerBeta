using System;
using Xamarin.Forms;
using LoLSkinExplorer.ViewModels;
using Newtonsoft.Json;
using System.Net;
using LoLSkinExplorer.Models;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using System.Linq;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Collections.ObjectModel;
using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LoLSkinExplorer.Views
{
    public partial class AboutPage : ContentPage
    {
        public MvvmHelpers.ObservableRangeCollection<Skin> Skins { get; set; }
        public AboutPage()
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
                ListViewomePage.ItemsSource = _container.Champions.Where(i => i.ChampionName.Contains(e.NewTextValue));
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
            await Navigation.PushAsync(new SkinsPage(champ.ChampionAlias));
            //await Navigation.PushAsync(new ChampionPage(champ.ChampionAlias));
        }

        

        
    }
}