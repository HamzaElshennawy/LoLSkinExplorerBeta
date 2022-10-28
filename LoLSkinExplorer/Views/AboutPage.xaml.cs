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
                return;
            }

        }

        

        private async void ListViewomePage_ItemSelected(object sender, SelectedItemChangedEventArgs e, MvvmHelpers.ObservableRangeCollection<Skin> skins)
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
            //await Navigation.PushAsync(new SkinsPage(champ.ChampionName));
            //List<Skin> ChampSkins = GetSkins(champ.ChampionName);
            SkinPageViewModel viewModel = new SkinPageViewModel();
            viewModel.Skins.Clear();
            viewModel.Skins = Skins;

            await Navigation.PushAsync(new SkinsPage(champ.ChampionName));
        }

        private void ListViewomePage_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
        public List<Skin> GetSkins(string _ChampName)
        {
            string BaseSkinLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/";
            string jsonText = "https://ddragon.leagueoflegends.com/cdn/12.14.1/data/en_US/champion/" + _ChampName + ".json";
            WebClient webClient = new WebClient();
            string downloadedJsonText = webClient.DownloadString(jsonText);
            JObject dobj = JsonConvert.DeserializeObject<dynamic>(downloadedJsonText);
            var skinNames = dobj["data"][_ChampName]["skins"].Value<JArray>();
            List<Skin> skins = skinNames.ToObject<List<Skin>>();
            for (int i = 0; i < skins.Count; i++)
            {
                skins[i].imgLink = BaseSkinLink + _ChampName + "_" + skins[i].SkinNum + ".jpg";


                if (skins[i].SkinName == "default")
                {
                    skins[i].SkinName = _ChampName;
                }
                Skins.Add(skins[i]);
            }
            OnPropertyChanged();
            return skins;
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
            //    var context = Android.App.Application.Context;
            //    var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            //    if (inputMethodManager != null && context is Activity)
            //    {
            //        var activity = context as Activity;
            //        var token = activity.CurrentFocus?.WindowToken;
            //        inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
            //        activity.Window.DecorView.ClearFocus();
            //    }
        }

        private void ListViewomePage_ItemSelected_1Async(object sender, SelectedItemChangedEventArgs e)
        {
            var champ = ((ListView)sender).SelectedItem as Champion;
            if (champ == null)
                return;
            
            
            
            //viewModel.Skins.Clear();
            //viewModel.Skins = Skins;

            Navigation.PushAsync(new SkinsPage(champ.ChampionName));
        }
    }
}