using LoLSkinExplorer.Models;
using LoLSkinExplorer.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

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
        //public AsyncCommand GetDataCommand { get; }
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
            //GetDataCommand = new AsyncCommand(GetDataTask);
            _RefreshCommand = new AsyncCommand(Refresh);
            //DeviceScreenSize();
            Thread thread = new Thread(GetData);
            thread.Start();

            //GetData();
            NewSkinsDisplay();
        }
        public async void GetData()
        {
            Skins.Clear();
            Champions.Clear();
            try
            {
                for (int i = 0; i < ChampionsNames.Count; i++)
                {
                    try
                    {
                        //if (s == null)
                        //{
                        //    await Application.Current.MainPage.DisplayAlert("sr", ChampionsNames[i], "ok");
                        //}

                        var tmp = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(AboutPage)).Assembly;
                        System.IO.Stream s = tmp.GetManifestResourceStream($"LoLSkinExplorer.Champions.{ChampionsNames[i]}.json");
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
                        TempChampion._LoadingScreen = $"https://ddragon.leagueoflegends.com/cdn/img/champion/loading/" + TempChampion.ChampionAlias + "_0.jpg";
                        var champBio = dobj["shortBio"];
                        TempChampion._Bio = (string)champBio;

                        var champRoles = dobj["roles"];
                        for (int j = 0; j < champRoles.Count(); j++)
                        {
                            string tempRole = champRoles[j].ToString();
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

                        foreach (var champAbility in champAbilities)
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
                                    abilitiess._SpellRange.Add(spellRanges.ElementAt(j).ToString());
                                }
                                for (int j = 0; j < spellCoolDown.Count(); j++)
                                {
                                    abilitiess._SpellCoolDowns.Add(spellCoolDown.ElementAt(j).ToString());
                                }
                            }
                            abilitiess.SpellName = spellName;
                            abilitiess.SpellKey = spellKey;
                            abilitiess.SpellDescription = spellDescription;

                            abilities.Add(abilitiess);
                            TempChampion.Abilities = abilities.ToList<Abilities>();
                            
                            //small fix for Wukong icon and background in champion details page
                            if(TempChampion.Alias == "Wukong")
                            {
                                TempChampion._LoadingScreen = $"https://ddragon.leagueoflegends.com/cdn/img/champion/loading/" + "MonkeyKing" + "_0.jpg";
                                TempChampion._Image = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/" + "MonkeyKing" + "_0.jpg";
                            }
                        }
                        Champions.Add(TempChampion);
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", ex.Message+ ChampionsNames2[i]+"-"+i, "OK");
                    }
                }
                OnPropertyChanged(nameof(Skins));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("error", ex.Message , "OK");
            }
        }
        //async Task GetDataTask()
        //{
        //    IsBusy = true;
        //    GetData();
        //    IsBusy = false;
        //}
        async Task Refresh()
        {
            IsBusy = true;
            GetData();
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
            "Kaisa","Kalista","Karma","Karthus","Kassadin","Katarina","Kayle","Kayn","Kennen","Khazix","Kindred","Kled","KogMaw","KSante",
            "Leblanc","LeeSin","Leona","Lillia","Lissandra","Lucian","Lulu","Lux",
            "Malphite","Malzahar","Maokai","MasterYi","MissFortune","Mordekaiser","Morgana",
            "Nami","Nasus","Nautilus","Neeko","Nidalee","Nilah","Nocturne","Nunu",
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
        public List<string> ChampionsNames2 = new List<string>()
        {
            "1","10","101","102","103","104","105","106","107","11","110","111","112","113","114","115","117" ,"119","12" ,"120","121","122","126","127","13" ,"131","133","134","136","14" ,"141",
            "142","143","145","147","15","150","154","157","16" ,"161","163","164","166","17","18","19","2","20 ","200","201","202","203","21","22","221","222","223","23","234","235",
            "236","238","24","240","245","246","25","254","26" ,"266","267","268","27","28","29","3","30","31","32","33","34","35 ","350","36","360","37","38","39","4","40","41","412","42",
            "420","421","427","429","43 ","432","44","45","48" ,"497","498","5","50","51","516","517","518","523","526","53","54","55","555","56","57","58","59","6","60","61 ","62 ","63","64",
            "67" ,"68", "69", "7","711","72","74","75","76" ,"77","777","78","79","8","80","81","82","83","84","85","86","875","876","887","888","89","895","897","9","90","91","92","96","98" ,"99"

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
                });
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



