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
    //chromas link https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/v1/champion-chroma-images/
    //[QueryProperty(nameof(ChampName),nameof(ChampName))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SkinsPage : ContentPage
    {
        public string ChampName;

        string BaseSkinLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/";
        string BaseSplashLink = "https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/v1/champion-splashes/";
        string BaseChromaLink = "https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/v1/champion-chroma-images/";
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
            System.IO.Stream s = tmp.GetManifestResourceStream($"LoLSkinExplorer.Champions.{name}.json");
            System.IO.StreamReader sr = new System.IO.StreamReader(s);
            
            string JsonText = sr.ReadToEnd();
            JObject dobj = JsonConvert.DeserializeObject<dynamic>(JsonText);

            List<Skin> skins = new List<Skin>();
            List<Chromas> chromas = new List<Chromas>();
            var skinNames = dobj["skins"].Value<JArray>();
            string champID = (string)dobj["id"];
            JArray NumberOfChromas = new JArray();
            //await Application.Current.MainPage.DisplayAlert("..", (string)dobj["skins"]["id"]["chromaPath"], "ok");
            try
            {
                //await Application.Current.MainPage.DisplayAlert("Error",)
                //if ((string)dobj["skins"]["id"]["chromaPath"]!= "null")
                //    NumberOfChromas = dobj["skins"][]["chromas"].Value<JArray>();
                string test = "";
                throw new Exception();
            }
            catch (Exception e)
            {
                NumberOfChromas = null;
            }
            
            try
            {
                skins = skinNames.ToObject<List<Skin>>();
                chromas = NumberOfChromas.ToObject<List<Chromas>>();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error",e.Message,"ok");
            }
            try
            {
                for (int i = 0; i < skins.Count; i++)
                {
                    
                    skins[i].imgLink = BaseSplashLink + champID+ "/" +skins[i].SkinID+".jpg";
                    //await Application.Current.MainPage.DisplayAlert("Skin Type",skins[i].skinType,"OK");
                    //if (skins[i].SkinName == "default")
                    //{
                    //    skins[i].SkinName = _ChampName;
                    //}
                    string  price = null;
                    if (skins[i].skinType == "kEpic")
                        skins[i].SkinPrice = 1350;
                    if (skins[i].skinType == "kLegendary")
                        skins[i].SkinPrice = 1820;
                    if (skins[i].skinType == "kUltimate")
                        skins[i].SkinPrice = 3250;
                    if (skins[i].skinType == "kMythic")
                        skins[i].SkinPrice = 100;
                    if (skins[i].skinType == "kNoRarity")
                        skins[i].SkinPrice = 975;



                    if (skins[i].IsAvailable)
                    {
                        Label ALBL = _ListView.FindByName("AvailabltyLBL");
                        ALBL.Text = "Available";
                    }
                        





                    Skins.Add(skins[i]);
                }
            }
            catch(Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            

            
            Skinss.Clear();
            Skinss.Add(skins[0]);
            Skins.Remove(skins[0]);
            OnPropertyChanged(nameof(Skins));
        }

        public int Price(string Rarity)
        {
            if(Rarity == "kNoRarity")
                return 975;
            if(Rarity == "kEpic")
                return 1350;
            else
                return 0;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}