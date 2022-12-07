using LoLSkinExplorer.Models;
using MvvmHelpers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LoLSkinExplorer.ViewModels;
using System.Collections.ObjectModel;

#pragma warning disable IDE1006 // Naming Styles

namespace LoLSkinExplorer.Views
{
    //chromas link https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/v1/champion-chroma-images/
    //[QueryProperty(nameof(ChampName),nameof(ChampName))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SkinsPage : ContentPage
    {
        public string champName;
        public string ChampName
        {
            get => champName;
            set
            {
                if (champName == value)
                    return;
                champName = value;
                OnPropertyChanged();
            }
        }
        string BaseSkinLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/";
        string BaseSplashLink = "https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/v1/champion-splashes/";
        string BaseChromaLink = "https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/v1/champion-chroma-images/";
        string BaseLoadingScrrenLink = "http://ddragon.leagueoflegends.com/cdn/img/champion/loading/";
        public ObservableCollection<Skin> ChampSkins { get; set; }
        public ObservableCollection<Skin> Skinss { get; set; }
        public SkinsPage(/*Champion champion*/ string ChampName)
        {
            InitializeComponent();
            BindingContext = this;
            //ChampName = champion.ChampionAlias;
            ChampSkins = new ObservableCollection<Skin>();
            Skinss = new ObservableCollection<Skin>();
            GetSkins(ChampName);
        }
        public async void GetSkins(string _ChampName)
        {
            ChampSkins.Clear();
            string name = _ChampName;



            var tmp = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(AboutPage)).Assembly;
            System.IO.Stream s = tmp.GetManifestResourceStream($"LoLSkinExplorer.Champions.{name}.json");
            System.IO.StreamReader sr = new System.IO.StreamReader(s);

            string JsonText = sr.ReadToEnd();
            JObject dobj = JsonConvert.DeserializeObject<dynamic>(JsonText);

            List<Skin> skins = new List<Skin>();
            List<Chromas> chromas = new List<Chromas>();
            var skinNames = dobj["skins"].Value<JArray>();
            string champID = (string)dobj["id"];
            JArray NumberOfChromas = new JArray();


            try
            {
                skins = skinNames.ToObject<List<Skin>>();
                chromas = NumberOfChromas.ToObject<List<Chromas>>();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "ok");
            }
            try
            {
                for (int i = 0; i < skins.Count; i++)
                {

                    skins[i].imgLink = BaseSplashLink + champID + "/" + skins[i].SkinID + ".jpg";
                    skins[i].LoadingScreen = BaseLoadingScrrenLink + name + skins[i].SkinID + ".jpg";

                    /// <summary>
                    /// In this aria I declare the skin prices.
                    /// </summary>
                    if (skins[i].skinType == "kEpic")
                        skins[i].SkinPrice = "1350 RP";
                    if (skins[i].skinType == "kLegendary")
                        skins[i].SkinPrice = "1820 RP";
                    if (skins[i].skinType == "kUltimate")
                        skins[i].SkinPrice = "3250 RP";
                    if (skins[i].skinType == "kMythic")
                        skins[i].SkinPrice = "100 ME";
                    if (skins[i].skinType == "kNoRarity")
                        skins[i].SkinPrice = "975 RP";

                    /// <summary>
                    /// In this area I declare if the skin is avialable or not.
                    /// </summary>

                    if (!skins[i].IsAvailable)
                    {
                        if (skins[i].skinType == "kMythic")
                            skins[i].availableString = "Unavailable";
                        else
                            skins[i].AvailableString = "Available";
                    }
                    else
                    {
                        skins[i].availableString = "Unavailable";
                    }






                    ChampSkins.Add(skins[i]);
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }



            Skinss.Clear();
            Skinss.Add(skins[0]);
            ChampSkins.Remove(skins[0]);
            OnPropertyChanged(nameof(ChampSkins));
        }

        public int Price(string Rarity)
        {
            if (Rarity == "kNoRarity")
                return 975;
            if (Rarity == "kEpic")
                return 1350;
            else
                return 0;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Skin skin = ((ListView)sender).SelectedItem as Skin;
            if (skin != null)
                await Application.Current.MainPage.DisplayAlert("Has crhomas?", skin.Chromas == null ? "No" : "Yes", "OK");


        }


        private void _ListView_ItemTapped(object sender, ItemTappedEventArgs e)

        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}