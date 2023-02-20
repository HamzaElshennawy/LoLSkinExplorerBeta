#pragma warning disable CS0219 // Variable is assigned but its value is never used


using LoLSkinExplorer.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoLSkinExplorer.Views
{



    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChampionPage : TabbedPage
    {

        //Important links
        string BaseSplashLink = "https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/v1/champion-splashes/";
        string BaseChromaLink = "https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/v1/champion-chroma-images/";
        string BaseLoadingScrrenLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/loading/";


        string EpicSkinLink = "https://static.wikia.nocookie.net/leagueoflegends/images/4/40/Epic_Skin.png/revision/latest/scale-to-width-down/20?cb=20171016035243";
        string LegendarySkinLink = "https://static.wikia.nocookie.net/leagueoflegends/images/f/f1/Legendary_Skin.png/revision/latest/scale-to-width-down/20?cb=20171016035307";
        string MythicSkinLink = "https://static.wikia.nocookie.net/leagueoflegends/images/4/4d/Hextech_Skin.png/revision/latest/scale-to-width-down/20?cb=20171016035256";
        string UltimateSkinLink = "https://static.wikia.nocookie.net/leagueoflegends/images/2/25/Ultimate_Skin.png/revision/latest/scale-to-width-down/20?cb=20171016035317";


        /// <summary>
        /// This is Champion Skins section
        /// </summary>
        public ObservableCollection<Skin> ChampSkins { get; set; }
        public ObservableCollection<Skin> Skinss { get; set; }

        public ObservableCollection<string> ChampionRoles { get; set; }



        /// <summary>
        /// This is Champion details section
        /// </summary>
        public Champion MainChampion { set; get; }
        public ObservableCollection<Abilities> ChampAbilities { get; set; }

        public string baseSpellIconLink;
        public string basePassiveLink;
        public double screenHeight { set; get; }
        public ChampionPage(Champion TempChampion)
        {
            InitializeComponent();
            screenHeight = DeviceDisplay.MainDisplayInfo.Height;
            MainChampion = new Champion();
            MainChampion = TempChampion;

            ChampSkins = new ObservableCollection<Skin>();
            Skinss = new ObservableCollection<Skin>();
            ChampAbilities = new ObservableCollection<Abilities>();
            ChampionRoles = new ObservableCollection<string>();

            BindingContext = this;
            
            basePassiveLink = "http://ddragon.leagueoflegends.com/cdn/13.1.1/img/passive/";
            baseSpellIconLink = "http://ddragon.leagueoflegends.com/cdn/13.1.1/img/spell/" + MainChampion.Alias;
            GetChampionCoreData(MainChampion);//pass the main champion to the function to get the core data such as lore, title,role, etc.
            //Application.Current.MainPage.DisplayAlert("Alias", MainChampion.Name, "OK");
            string ChampName = MainChampion.ChampionAlias;
            Thread SkinsThread = new Thread(() => { GetSkins(ChampName); });
            SkinsThread.Start();

        }


        public void GetChampionCoreData(Champion champion)
        {
            Champion _MainChampion = champion;
            string championRoles = "";
            if (_MainChampion.Role.Count() == 2)
            {
                
                ChampionRoles.Add(MainChampion.Role[0]);
                ChampionRoles.Add(MainChampion.Role[1]);

            }
            else
            {
                ChampionRoles.Add(MainChampion.Role[0]);
                Type2Label.IsVisible = false;
                Role2image.IsVisible = false;
            }
            _MainChampion.SpellP = basePassiveLink + _MainChampion.Alias + "_P.png";
            _MainChampion.SpellQ = baseSpellIconLink + "Q.png";
            _MainChampion.SpellW = baseSpellIconLink + "W.png";
            _MainChampion.SpellE = baseSpellIconLink + "E.png";
            _MainChampion.SpellR = baseSpellIconLink + "R.png";
            _MainChampion.ChampionTitle = _MainChampion.ChampionTitle.ToUpper();
            MainChampion = _MainChampion;
            AddAbilitiesToCollection();
            OnPropertyChanged(nameof(MainChampion));
        }
        public string getPassiveLink(string championName)
        {
            string championJson = $"http://ddragon.leagueoflegends.com/cdn/13.1.1/data/en_US/champion/{championName}.json";

            return "";
        }
        public void AddAbilitiesToCollection()
        {
            for (int i = 0; i < MainChampion.Abilities.Count; i++)
            {
                 ChampAbilities.Add(MainChampion.Abilities[i]);
            }
            OnPropertyChanged(nameof(ChampAbilities));
        }


        private async void GetSkins(string _ChampName)
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
                    {
                        skins[i].SkinPrice = "1350 RP";
                        skins[i].SkinTypeIconLink = EpicSkinLink;
                    }
                    if (skins[i].skinType == "kLegendary")
                    {
                        skins[i].SkinPrice = "1820 RP";
                        skins[i].SkinTypeIconLink = LegendarySkinLink;
                    }
                    if (skins[i].skinType == "kUltimate")
                    {
                        skins[i].SkinPrice = "3250 RP";
                        skins[i].SkinTypeIconLink = UltimateSkinLink;
                    }
                    if (skins[i].skinType == "kMythic")
                    {
                        skins[i].SkinPrice = "100 ME";
                        skins[i].SkinTypeIconLink = MythicSkinLink;
                    }
                    if (skins[i].skinType == "kNoRarity")
                    {
                        skins[i].SkinPrice = "975 RP";
                        skins[i].SkinTypeIconLink = "";
                    }

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
                    OnPropertyChanged(nameof(ChampSkins));
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
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Skin skin = ((ListView)sender).SelectedItem as Skin;
            if (skin != null)
            //await Application.Current.MainPage.DisplayAlert("Has crhomas?", skin.Chromas == null ? "No" : "Yes", "OK");
            //Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
            {
                if (skin.Chromas != null)
                {
                    await Navigation.PushAsync(new ChromasPage(skin, MainChampion.ChampionId), true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Opps!", "This skin has no chromas", "OK");
                }
            }
        }


        private void _ListView_ItemTapped(object sender, ItemTappedEventArgs e)

        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}