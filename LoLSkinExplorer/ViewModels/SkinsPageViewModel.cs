using LoLSkinExplorer.Models;
using MvvmHelpers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LoLSkinExplorer.ViewModels
{
    public class SkinsPageViewModel : BaseViewModel
    {
        public string ChampName { get; set; }

        string BaseSkinLink = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/";
        public ObservableRangeCollection<Skin> Skins { get; set; }

        public SkinsPageViewModel(string champ)
        {
            Skins = new ObservableRangeCollection<Skin>();
            ChampName = champ;

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


        public SkinsPageViewModel()
{


            //ChampName = cName;
            Skins = new ObservableRangeCollection<Skin>();


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

        public void GetSkins(string _ChampName)
        {
            Skins.Clear();
            string jsonText = "https://ddragon.leagueoflegends.com/cdn/12.14.1/data/en_US/champion/" + _ChampName + ".json";
            WebClient webClient = new WebClient();
            string downloadedJsonText = webClient.DownloadString(jsonText);
            JObject dobj = JsonConvert.DeserializeObject<dynamic>(downloadedJsonText);
            var skinNames = dobj["data"][_ChampName]["skins"].Value<JArray>();
            List<Skin> skins = skinNames.ToObject<List<Skin>>();
            for (int i = 0; i < skins.Count; i++)
            {
                skins[i].imgLink = BaseSkinLink + _ChampName + "_" + skins[i].SkinNum + ".jpg";
                Skins.Add(skins[i]);

                if (Skins[i].SkinName == "default")
                {
                    Skins[i].SkinName = _ChampName;
                }
            }
            OnPropertyChanged();
        }

    }
}
