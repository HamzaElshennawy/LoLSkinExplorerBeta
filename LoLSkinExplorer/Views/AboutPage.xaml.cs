using System;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LoLSkinExplorer.ViewModels;
using Newtonsoft.Json;
using System.Net;
using LoLSkinExplorer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json.Linq;
using Xamarin.CommunityToolkit.Extensions;

namespace LoLSkinExplorer.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
             Navigation.ShowPopup(new SearchPopupPage());
        }

        private async void ListViewomePage_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var skin = ((ListView)sender).SelectedItem as Skin;
            //if(skin == null)
            //    return;
            //await DisplayAlert("Skin link", skin.ImgLink, "OK");
            var champ = ((ListView)sender).SelectedItem as Champion;
            if (champ == null)
                return;
            //var route = $"{nameof(SkinsPage)}?ChampName = {champ.Name}";
            //try
            //{
            //    await Shell.Current.GoToAsync(route);
            //}
            //catch(Exception ex)
            //{
            //    await Application.Current.MainPage.DisplayAlert("error", ex.Message, "OK");
            //}
            await Navigation.PushAsync(new SkinsPage(champ.ChampionName));
        }

        private void ListViewomePage_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}