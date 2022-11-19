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
        public ObservableCollection<Skin> Skinss { get; set; }
        public SkinsPage(string cName)
        {
            InitializeComponent();
            BindingContext = this;
            ChampName = cName;
            Skins = new ObservableCollection<Skin>();
            Skinss = new ObservableCollection<Skin>();
            GetSkins(ChampName);
        }
        public async void GetSkins(string _ChampName)
        {
            Skins.Clear();
            string name = _ChampName;
            //string jsonText = "https://ddragon.leagueoflegends.com/cdn/12.20.1/data/en_US/champion/" +"Aatrox"+ ".json";
            //string jsonText = "https://ddragon.leagueoflegends.com/cdn/12.20.1/data/en_US/champion/" + _ChampName + ".json";
            //WebClient webClient = new WebClient();
            //string downloadedJsonText = webClient.DownloadString(jsonText);

            var tmp = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(AboutPage)).Assembly;
            System.IO.Stream s = tmp.GetManifestResourceStream("LoLSkinExplorer.Champions.Annie.json");
            System.IO.StreamReader sr = new System.IO.StreamReader(s);

            string JsonText = sr.ReadToEnd();
            JObject dobj = JsonConvert.DeserializeObject<dynamic>(JsonText);

            List<Skin> skins = new List<Skin>();

            var skinNames = dobj["data"][_ChampName]["skins"].Value<JArray>();
            try
            {
                skins = skinNames.ToObject<List<Skin>>();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error",e.Message,"ok");
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

            //List<string> skinIDs = new List<string>();
            //foreach (var skin in skins)
            //{
            //    skinIDs.Add(skin.SkinID.ToString());
            //}

            //jsonText = "https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/v1/skins.json";
            //downloadedJsonText = webClient.DownloadString(jsonText);
            //dobj = JsonConvert.DeserializeObject<dynamic>(downloadedJsonText);

            //skins.Clear();
            //try
            //{
            //    for (int i = 0; i < skinIDs.Count; i++)
            //    {
            //        var _skins = dobj[skinIDs[i]].Value<JArray>();
            //        skins[i].SkinType = _skins["rarity"].ToString();
            //        await Application.Current.MainPage.DisplayAlert("Skin type", skins[i].skinType, "ok");
            //    }
            //}
            //catch(Exception e)
            //{
            //    await Application.Current.MainPage.DisplayAlert("error", e.Message, "ok");
            //}

            Skinss.Clear();
            Skinss.Add(skins[0]);
            Skins.Remove(skins[0]);
            OnPropertyChanged(nameof(Skins));
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}