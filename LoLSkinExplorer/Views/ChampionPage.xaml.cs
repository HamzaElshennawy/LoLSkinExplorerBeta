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
using System.Threading.Tasks;

namespace LoLSkinExplorer.Views
{



    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChampionPage : TabbedPage
    {


        public Command DifficultyExpCommand { set; get; }
        public Command MobilityExpCommand { set; get; }

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
        public double screenWidth { set; get; }

        public double PBWidth { set; get; }
        public ChampionPage(Champion TempChampion)
        {
            InitializeComponent();

            DifficultyExpCommand = new Command(AnimateDifficulty);
            MobilityExpCommand = new Command(AnimateMobility);


            screenHeight = DeviceDisplay.MainDisplayInfo.Height;
            screenWidth = DeviceDisplay.MainDisplayInfo.Width;
            PBWidth = (screenWidth / 3) - 30;
            MainChampion = new Champion();
            MainChampion = TempChampion;
            BackgroundImageSource = MainChampion.ChampionLoadingScreen;

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

            
            this.BackgroundImageSource = MainChampion.ChampionLoadingScreen;
            
        }

        

        private void ReformatDamageType()
        {
            //replace kPhysical to Physical in the damagetype

            if (MainChampion.TacticalInfo.DamageType == "kPhysical")
                MainChampion.TacticalInfo.DamageType = "Physical";
            if (MainChampion.TacticalInfo.DamageType == "kMagic")
                MainChampion.TacticalInfo.DamageType = "Magic";
            if (MainChampion.TacticalInfo.DamageType == "kMixed")
                MainChampion.TacticalInfo.DamageType = "Mixed";
            OnPropertyChanged(nameof(MainChampion));
        }

        public void GetChampionCoreData(Champion champion)
        {
            Champion _MainChampion = champion;
            string championRoles = "";
            if (_MainChampion.Role.Count() == 2)
            {

                ChampionRoles.Add(MainChampion.Role[0].ToUpper());
                ChampionRoles.Add(MainChampion.Role[1].ToUpper());

            }
            else
            {
                ChampionRoles.Add(MainChampion.Role[0].ToUpper());
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
            ReformatDamageType();

            
            OnPropertyChanged(nameof(MainChampion));
            OnPropertyChanged(nameof(ChampionRoles));
        }

        private async void AnimateMobilityProgressBar()
        {
            if (MainChampion.PlaystyleInfo.Mobility == 1)
            {
                await MobilityPB1.ProgressTo(1, 850, Easing.Linear);
            }
            if (MainChampion.PlaystyleInfo.Mobility == 2)
            {
                await MobilityPB1.ProgressTo(1, 850, Easing.Linear);
                await MobilityPB2.ProgressTo(1, 750, Easing.Linear);
            }
            if (MainChampion.PlaystyleInfo.Mobility == 3)
            {
                await MobilityPB1.ProgressTo(1, 850, Easing.Linear);
                await MobilityPB2.ProgressTo(1, 750, Easing.Linear);
                await MobilityPB3.ProgressTo(1, 850, Easing.Linear);
            }
        }
        private async void AnimateMobilityProgressBarBack()
        {
            await MobilityPB1.ProgressTo(0, 600, Easing.Linear);
            await MobilityPB2.ProgressTo(0, 600, Easing.Linear);
            await MobilityPB3.ProgressTo(0, 600, Easing.Linear);
        }

        private async void AnimateMobility()
        {
            if (MobilityExp.IsExpanded)
                await Task.Run(AnimateMobilityProgressBar);
            else
                Task.Run(AnimateMobilityProgressBarBack);
        }
        private async void AnimateDifficultyProgressBarBack()
        {
            await DifficultyPB1.ProgressTo(0, 600, Easing.Linear);
            await DifficultyPB2.ProgressTo(0, 600, Easing.Linear);
            await DifficultyPB3.ProgressTo(0, 600, Easing.Linear);
        }
        private async void AnimateDifficultyProgressBar()
        {

            if (MainChampion.TacticalInfo.Difficulty == 3)
            {
                await DifficultyPB1.ProgressTo(1, 850, Easing.Linear);
                await DifficultyPB2.ProgressTo(1, 850, Easing.Linear);
                await DifficultyPB3.ProgressTo(1, 750, Easing.Linear);
            }
            if (MainChampion.TacticalInfo.Difficulty == 2)
            {
                await DifficultyPB1.ProgressTo(1, 850, Easing.Linear);
                await DifficultyPB2.ProgressTo(1, 750, Easing.Linear);
            }
            if (MainChampion.TacticalInfo.Difficulty == 1)
            {
                await DifficultyPB1.ProgressTo(1, 950, Easing.Linear);
            }
        }
        private async void AnimateDifficulty()
        {
            if (DifficultyExp.IsExpanded)
                await Task.Run(AnimateDifficultyProgressBar);
            else /*(!DifficultyExp.IsExpanded)*/
                await Task.Run(AnimateDifficultyProgressBarBack);
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


            var tmp = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(HomePage)).Assembly;
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