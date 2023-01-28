﻿using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading;
using LoLSkinExplorer.Models;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using LoLSkinExplorer.Views;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Essentials;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace LoLSkinExplorer.ViewModels
{


    //skin link https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Aatrox_8.jpg


    public class SkinPageViewModel : BaseViewModel
    {
        //string BaseSkinLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/";
        //21,
        public ObservableRangeCollection<Champion> Champions { get; set; }
        public ObservableRangeCollection<Skin> Skins { get; set; }
        public ObservableRangeCollection<Skin> NewSkins { get; set; }
        public ObservableRangeCollection<Abilities> abilities { get; set; }
        public AsyncCommand GetDataCommand { get; }
        public AsyncCommand getDataCommand { get; }
        public AsyncCommand SearchCommand { get; }
        public AsyncCommand _RefreshCommand { get; }
        public SkinPageViewModel()
        {
            var ScreenSize = DeviceDisplay.MainDisplayInfo;
            var ScreenWidth = ScreenSize.Width;
            var ScreenHeight = ScreenSize.Height;

            var ScreenWidthForCell = ScreenWidth * 0.80;
            var ScreenHightForCell = ScreenHeight * 0.3;

            Title = "ChampSkins";
            Champions = new ObservableRangeCollection<Champion>();
            Skins = new ObservableRangeCollection<Skin>();
            NewSkins = new ObservableRangeCollection<Skin>();
            GetDataCommand = new AsyncCommand(GetDataTask);
            _RefreshCommand = new AsyncCommand(Refresh);
            DeviceScreenSize();
            _=GetData();
            NewSkinsDisplay();
        }
        public async Task GetData()
        {
            Skins.Clear();
            Champions.Clear();
            try
            {
                for (int i = 0; i < ChampionsNames.Count; i++)
                {
                    try
                    {

                        var tmp = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(AboutPage)).Assembly;
                        System.IO.Stream s = tmp.GetManifestResourceStream($"LoLSkinExplorer.Champions.{ChampionsNames[i]}.json");
                        if (s == null)
                        {
                            await Application.Current.MainPage.DisplayAlert("sr", ChampionsNames[i], "ok");
                        }
                        //await Application.Current.MainPage.DisplayAlert("path", $"LoLSkinExplorer.Champions.{ChampionsNames[i]}.json", "OK");
                        System.IO.StreamReader sr = new System.IO.StreamReader(s);



                        string JsonText = sr.ReadToEnd();

                        JObject dobj = JsonConvert.DeserializeObject<dynamic>(JsonText);
                        Champion TempChampion = new Champion();
                        var champID = dobj["id"];
                        TempChampion.ChampionId = (string)champID;
                        //var champKey = dobj["key"];
                        //TempChampion.ChampionKey = (string)champKey;
                        var championAlias = dobj["alias"];
                        TempChampion.ChampionAlias = (string)championAlias;
                        var champName = dobj["name"];
                        string _champName = (string)champName;
                        TempChampion.ChampionName = _champName.ToLower();
                        var champTitle = dobj["title"];
                        TempChampion.ChampionTitle = (string)champTitle;
                        TempChampion.ChampionImage = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/" + TempChampion.ChampionAlias + "_0.jpg";
                        TempChampion._LoadingScreen = $"http://ddragon.leagueoflegends.com/cdn/img/champion/loading/"+TempChampion.ChampionAlias+"_0.jpg";
                        var champBio = dobj["shortBio"];
                        TempChampion._Bio = (string)champBio;
                        
                        var champRoles = dobj["roles"];
                        for(int j = 0; j < champRoles.Count(); j++)
                        {
                            string tempRole = champRoles[j].ToString();
                            //await Application.Current.MainPage.DisplayAlert("Role", tempRole, "OK");
                            TempChampion.Role.Add(tempRole);
                        }
                        



                        abilities = new ObservableRangeCollection<Abilities>();

                        var SpellP = dobj["passive"];
                        Abilities spellP = new Abilities();
                        spellP.SpellName = dobj["passive"]["name"].ToString();
                        spellP.SpellDescription = dobj["passive"]["description"].ToString();
                        spellP.SpellKey = "q";
                        abilities.Add(spellP);
                        TempChampion.Abilities.Add(spellP);

                        var champAbilities = dobj["spells"];

                        foreach ( var champAbility in champAbilities )
                        {
                            


                            var spellKey = champAbility["spellKey"].ToString();

                            var spellName = champAbility["name"].ToString();

                            var spellRanges = champAbility["range"];

                            var spellCoolDown = champAbility["cooldownCoefficients"];

                            var spellDescription = champAbility["description"].ToString();

                            Abilities abilitiess = new Abilities();
                            if (spellRanges.Count() > 0)
                            {
                                for (int j = 0; j < spellRanges.Count(); j++)
                                {
                                    //await Application.Current.MainPage.DisplayAlert("Range", spellRanges.ElementAt(j).ToString(), "OK");
                                    abilitiess._SpellRange.Add(spellRanges.ElementAt(j).ToString());
                                    //Application.Current.MainPage.DisplayAlert("Range", spellRanges.ElementAt(j).ToString(), "OK");
                                }
                                for (int j = 0; j < spellCoolDown.Count(); j++)
                                {
                                    abilitiess._SpellCoolDowns.Add(spellCoolDown.ElementAt(j).ToString());
                                    //await Application.Current.MainPage.DisplayAlert("cooldown", TempChampion.Alias +" "+ spellCoolDown.ElementAt(j).ToString()+" "+spellKey, "ok");
                                }
                            }

                            //abilitiess._SpellRange = spellRanges.Values();

                            abilitiess.SpellName = spellName;
                            abilitiess.SpellKey = spellKey;
                            abilitiess.SpellDescription = spellDescription;

                            abilities.Add(abilitiess);
                            TempChampion.Abilities = abilities.ToList<Abilities>();
                        }
                        Champions.Add(TempChampion);
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                    }
                }
                OnPropertyChanged(nameof(Skins));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("error", ex.Message, "OK");
            }
        }
        async Task GetDataTask()
        {
            IsBusy = true;
            await GetData();
            IsBusy = false;
        }
        async Task Refresh()
        {
            IsBusy = true;
            await GetData();
            IsBusy = false;
        }
        List<string> ChampionsNames = new List<string>()
        {
            "Aatrox","Ahri","Akali","Akshan","Alistar","Amumu","Anivia","Annie","Aphelios","Ashe","AurelionSol","Azir",
            "Bard","Belveth","Blitzcrank","Brand","Braum",
            "Caitlyn","Camille","Cassiopeia","Chogath","Corki",
            "Darius","Diana","Draven","DrMundo",
            "Ekko","Elise","Evelynn","Ezreal",
            "FiddleSticks","Fiora","Fizz",
            "Galio","Gangplank","Garen","Gnar","Gragas","Graves","Gwen",
            "Hecarim","Heimerdinger",
            "Illaoi","Irelia","Ivern",
            "Janna","JarvanIV","Jax","Jayce","Jhin","Jinx",
            "Kaisa","Kalista","Karma","Karthus","Kassadin","Katarina","Kayn","Kennen","Khazix","Kindred","Kled","KogMaw","KSante",
            "Leblanc","LeeSin","Leona","Lillia","Lissandra",
            "Lucian", "Lulu","Lux",
            "Malphite","Malzahar","Maokai","MasterYi","MissFortune","Mordekaiser","Morgana",
            "Nami","Nasus","Neeko","Nidalee","Nilah","Nocturne","Nunu",
            "Olaf","Orianna","Ornn",
            "Pantheon","Poppy","Pyke",
            "Qiyana","Quinn",
            "Rakan","Rammus","RekSai","Rell","Renata","Renekton","Rengar","Riven","Rumble" ,"Ryze",
            "Samira","Sejuani","Senna","Seraphine","Sett","Shaco","Shen","Shyvana","Singed","Sion","Sivir","Skarner","Sona","Soraka","Swain","Sylas","Syndra",
            "TahmKench","Taliyah","Talon","Taric","Teemo","Thresh","Tristana","Trundle","Tryndamere","TwistedFate","Twitch",
            "Udyr","Urgot",
            "Varus","Vayne","Veigar","Velkoz","Vex","Vi","Viego","Viktor","Vladimir","Volibear",
            "Warwick","Wukong",
            "Xayah","Xerath","XinZhao",
            "Yasuo","Yone","Yorick","Yuumi",
            "Zac","Zed","Zeri","Ziggs","Zilean","Zoe","Zyra"
        };
        public int CheckForNetwork()
        {
            var connectivity = Connectivity.NetworkAccess;
            if (connectivity == NetworkAccess.Internet)
                return 1;
            return 0; 
        }
        public void NewSkinsDisplay()
        {
            NewSkins.Add(
                new Skin
                {
                    SkinName = "Winterblessed Swain",
                    SkinPrice = "1350 RP",
                    SkinType = "Epic",
                    ImgLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Swain_21.jpg",
                }) ;
            NewSkins.Add(
                new Skin
                {
                    SkinName = "Winterblessed Diana",
                    SkinPrice = "1820 RP",
                    SkinType = "Legendary",
                    ImgLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Diana_47.jpg"
                });
            NewSkins.Add(
                new Skin
                {
                    SkinName = "Winterblessed Shaco",
                    SkinPrice = "1350 RP",
                    SkinType = "Epic",
                    ImgLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Shaco_33.jpg"
                });
            NewSkins.Add(
                new Skin
                {
                    SkinName = "Winterblessed Zoe",
                    SkinPrice = "1350 RP",
                    SkinType = "Epic",
                    ImgLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Zoe_22.jpg"
                });
            NewSkins.Add(
                new Skin
                {
                    SkinName = "Winterblessed Zilean",
                    SkinPrice = "1350 RP",
                    SkinType = "",
                    ImgLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Zilean_14.jpg"
                });
            NewSkins.Add(
                new Skin
                {
                    SkinName = "Winterblessed Warwick",
                    SkinPrice = "1350 RP",
                    SkinType = "Epic",
                    ImgLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Warwick_45.jpg"
                });
            NewSkins.Add(
                new Skin
                {
                    SkinName = "Prestige Winterblessed Warwick",
                    SkinPrice = "2000 Tokens",
                    SkinType = "Prestige",
                    ImgLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Warwick_46.jpg"
                });
            
            NewSkins.Add(
            new Skin
            {
                SkinName = "Ashen Graveknight Mordekaiser",
                SkinPrice = "100 ME",
                SkinType = "Mythic",
                ImgLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Mordekaiser_42.jpg"
            });
        }
        public List<double> DeviceScreenSize()
        {
            List<double> WidthandHeight = new List<double>();
            WidthandHeight.Add(DeviceDisplay.MainDisplayInfo.Width);
            WidthandHeight.Add(DeviceDisplay.MainDisplayInfo.Height);
            return WidthandHeight;
        }
    }
}
