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

namespace LoLSkinExplorer.Views
{
    //[QueryProperty(nameof(ChampName),nameof(ChampName))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SkinsPage : ContentPage
    {
        public string ChampName;

        string BaseSkinLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/";
        public ObservableCollection<Skin> Skins { get; set; }

        public SkinsPage(string cName)
        {
            InitializeComponent();
            BindingContext = this;
            ChampName = cName;
            Skins = new ObservableCollection<Skin>();


            Skins.Add(new Skin()
            {
                SkinID = "266001",
                SkinNum = 1,
                SkinName = "Justicar Aatrox",
                HasChromas = false,
                ImgLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Aatrox_1.jpg",
            });
            GetSkins(ChampName);
        }
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    SkinsPageViewModel skinsPageViewModel = new SkinsPageViewModel()
        //    {
        //        ChampName = ChampName
        //    };

        //}

        public void GetSkins(string _ChampName)
        {
            Skins.Clear();
            string name = _ChampName;
            //string jsonText = "https://ddragon.leagueoflegends.com/cdn/12.20.1/data/en_US/champion/" +"Aatrox"+ ".json";
            string jsonText = "https://ddragon.leagueoflegends.com/cdn/12.20.1/data/en_US/champion/" + _ChampName + ".json";
            WebClient webClient = new WebClient();
            string downloadedJsonText = webClient.DownloadString(jsonText);
            JObject dobj = JsonConvert.DeserializeObject<dynamic>(downloadedJsonText);

            List<Skin> skins = new List<Skin>();

            var skinNames = dobj["data"][_ChampName]["skins"].Value<JArray>();
            try
            {
                skins = skinNames.ToObject<List<Skin>>();
            }
            catch (Exception)
            {

                throw;
            }

            for (int i = 0; i < skins.Count; i++)
            {
                skins[i].imgLink = BaseSkinLink + _ChampName + "_" + skins[i].SkinNum + ".jpg";


                if (skins[i].SkinName == "default")
                {
                    skins[i].SkinName = _ChampName;
                }
                Skins.Add(skins[i]);

            }

            //await Application.Current.MainPage.DisplayAlert("Skin name", skins[0].skinName, "ok");

            OnPropertyChanged(nameof(Skins));
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}