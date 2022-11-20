using MvvmHelpers;
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

namespace LoLSkinExplorer.ViewModels
{


    //skin link https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Aatrox_8.jpg


    public class SkinPageViewModel : BaseViewModel
    {
        string BaseSkinLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/";
        public ObservableRangeCollection<Champion> Champions { get; set; }
        public ObservableRangeCollection<Skin> Skins { get; set; }
        public ObservableRangeCollection<Skin> SkinsAtStart { get; set; }
        public AsyncCommand GetDataCommand { get; }
        public AsyncCommand getDataCommand { get; }
        public AsyncCommand SearchCommand { get; }
        public SkinPageViewModel()
        {
            Title = "Skins";
            Champions = new ObservableRangeCollection<Champion>();
            Skins = new ObservableRangeCollection<Skin>();
            SkinsAtStart = new ObservableRangeCollection<Skin>();
            GetDataCommand = new AsyncCommand(GetDataTask);
            
            Champions.Add(new Champion()
            {
                ChampionId = "Ahri",
                ChampionKey = "103",
                ChampionName = "Ahri",
                ChampionTitle = "the Nine-Tailed Fox",
                ChampionImage = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Ahri_0.jpg"
            });
        }
        public async Task GetData()
        {
            Skins.Clear();
            Champions.Clear();
            try
            {
                //for (int i = 0; i < ChampionsNames.Count; i++)
                //{
                    try
                    {
                        //string jsonText = "https://ddragon.leagueoflegends.com/cdn/12.20.1/data/en_US/champion/" + ChampionsNames[i] + ".json";
                        //WebClient webClient = new WebClient();
                        //string downloadedJsonText = webClient.DownloadString(jsonText);
                        //JObject dobj = JsonConvert.DeserializeObject<dynamic>(downloadedJsonText);
                        //Champion TempChampion = new Champion();
                        //var champID = dobj["data"][ChampionsNames[i]]["id"];
                        //TempChampion.ChampionId = (string)champID;
                        //var champKey = dobj["data"][ChampionsNames[i]]["key"];
                        //TempChampion.ChampionKey = (string)champKey;
                        //var champName = dobj["data"][ChampionsNames[i]]["name"];
                        //TempChampion.ChampionName = (string)champName;
                        //var champTitle = dobj["data"][ChampionsNames[i]]["title"];
                        //TempChampion.ChampionTitle = (string)champTitle;
                        //TempChampion.ChampionImage = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/" + TempChampion.ChampionId + "_0.jpg";
                        //Champions.Add(TempChampion);




                        var tmp = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(AboutPage)).Assembly;
                        System.IO.Stream s = tmp.GetManifestResourceStream("LoLSkinExplorer.Champions.Aatrox.json");
                        System.IO.StreamReader sr = new System.IO.StreamReader(s);

                        string JsonText = sr.ReadToEnd();
                        //WebClient webClient = new WebClient();
                        //string downloadedJsonText = webClient.DownloadString(jsonText);

                        JObject dobj = JsonConvert.DeserializeObject<dynamic>(JsonText);
                        Champion TempChampion = new Champion();
                        var champID = dobj["data"]["Aatrox"]["id"];
                        TempChampion.ChampionId = (string)champID;
                        var champKey = dobj["data"]["Aatrox"]["key"];
                        TempChampion.ChampionKey = (string)champKey;
                        var champName = dobj["data"]["Aatrox"]["name"];
                        TempChampion.ChampionName = (string)champName;
                        var champTitle = dobj["data"]["Aatrox"]["title"];
                        TempChampion.ChampionTitle = (string)champTitle;
                        TempChampion.ChampionImage = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/" + TempChampion.ChampionId + "_0.jpg";
                        Champions.Add(TempChampion);

                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                    }
                //}
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
        List<string> ChampionsNames = new List<string>()
        {
            "Aatrox","Ahri","Akali","Akshan","Alistar","Amumu","Anivia","Annie","Aphelios","Ashe","AurelionSol","Azir",
            "Bard","Belveth","Blitzcrank","Brand","Braum",
            "Caitlyn","Camille","Cassiopeia","Chogath","Corki",
            "Darius","Diana","Draven","DrMundo",
            "Ekko","Elise","Evelynn","Ezreal",
            "Fiddlesticks","Fiora","Fizz",
            "Galio","Gangplank","Garen","Gnar","Gragas","Graves","Gwen",
            "Hecarim","Heimerdinger",
            "Illaoi","Irelia","Ivern",
            "Janna","JarvanIV","Jax","Jayce","Jhin","Jinx",
            "Kaisa","Kalista","Karma","Karthus","Kassadin","Katarina","Katarina","Kayn","Kennen","Khazix","Kindred","Kled","KogMaw","KSante",
            "Leblanc","LeeSin","Leona","Lillia","Lissandra",
            "Lucian", "Lulu","Lux",
            "Malphite","Malzahar","Maokai","MasterYi","MissFortune","MonkeyKing","Mordekaiser","Morgana",
            "Nami","Nasus","Neeko","Nidalee","Nilah","Nocturne","Nunu",
            "Olaf","Orianna","Ornn",
            "Pantheon","Poppy","Pyke",
            "Qiyana","Quinn",
            "Rakan","Rammus","RekSai","Rell","Renata","Renekton","Rengar","Riven","Rumble" ,"Ryze",
            "Samira","Sejuani","Senna","Senna","Seraphine","Sett","Shaco","Shen","Shyvana","Singed","Sion","Sivir","Skarner","Sona","Soraka","Swain","Sylas","Syndra",
            "TahmKench","Taliyah","Talon","Taric","Teemo","Thresh","Tristana","Trundle","Tryndamere","TwistedFate","Twitch",
            "Udyr","Urgot",
            "Varus","Vayne","Veigar","Velkoz","Vex","Vi","Viego","Viktor","Vladimir","Volibear",
            "Warwick",
            "Xayah","Xerath","XinZhao",
            "Yasuo","Yone","Yorick","Yuumi",
            "Zac","Zed","Zeri","Ziggs","Zilean","Zoe","Zyra"
        };
    }
}
