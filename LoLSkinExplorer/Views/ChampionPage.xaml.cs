#pragma warning disable CS0219 // Variable is assigned but its value is never used


using LoLSkinExplorer.Models;
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

namespace LoLSkinExplorer.Views
{



    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChampionPage : ContentPage
    {
        public Champion championForXaml = new Champion();
        public string baseSpellIconLink;
        public string basePassiveLink;
        public ChampionPage(Champion TempChampion)
        {
            InitializeComponent();
            Champion MainChampion = TempChampion as Champion;
            championForXaml = TempChampion;
            BindingContext = this;
            //Application.Current.MainPage.DisplayAlert("source", TempChampion.ChampionImage, "OK");
            BackGrounImage.Source = MainChampion.ChampionLoadingScreen;
            BindingMode bindingMode = BindingMode.TwoWay;


            Title = MainChampion.Name.ToUpper();
            basePassiveLink = "http://ddragon.leagueoflegends.com/cdn/13.1.1/img/passive/";
            baseSpellIconLink = "http://ddragon.leagueoflegends.com/cdn/13.1.1/img/spell/" + MainChampion.Alias;
            GetChampionCoreData(MainChampion);//pass the main champion to the function to get the core data such as lore, title,role, etc.
            //Application.Current.MainPage.DisplayAlert("Alias", MainChampion.Name, "OK");
        }


        public void GetChampionCoreData(Champion champion)
        {
            Champion MainChampion = champion;
            BioLabel.Text = MainChampion.Bio;
            string championRoles = "";
            if (MainChampion.Role.Count() == 2)
            {
                Type1Label.Text = MainChampion.Role[0].ToUpper();
                Type2Label.Text = MainChampion.Role[1].ToUpper();
            }
            else
            {
                Type1Label.Text = MainChampion.Role[0].ToUpper();
                Type2Label.IsVisible = false;
                Role2image.IsVisible = false;
            }
            MainChampion.SpellP = basePassiveLink + MainChampion.Alias + "_P.png";
            //Application.Current.MainPage.DisplayAlert("Link", MainChampion.SpellP, "OK ");

            //SpellPImg.Source = MainChampion.SpellP;
            MainChampion.SpellQ = baseSpellIconLink + "Q.png";
            //SpellQImg.Source = MainChampion.SpellQ;

            MainChampion.SpellW = baseSpellIconLink + "W.png";
            //SpellWImg.Source = MainChampion.SpellW;

            MainChampion.SpellE = baseSpellIconLink + "E.png";
            //SpellEImg.Source = MainChampion.SpellE;

            MainChampion.SpellR = baseSpellIconLink + "R.png";
            //SpellRImg.Source = MainChampion.SpellR;
            championForXaml = MainChampion;
            PassiveLBL.Text = championForXaml.Abilities[0].SpellDescription;
            OnPropertyChanged(nameof(championForXaml));
            //Application.Current.MainPage.DisplayAlert("Link", MainChampion.SpellQ, "OK ");
        }
        public string getPassiveLink(string championName)
        {
            string championJson = $"http://ddragon.leagueoflegends.com/cdn/13.1.1/data/en_US/champion/{championName}.json";

            return "";
        }
    }
}