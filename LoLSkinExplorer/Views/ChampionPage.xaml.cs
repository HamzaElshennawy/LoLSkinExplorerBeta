#pragma warning disable CS0219 // Variable is assigned but its value is never used


using LoLSkinExplorer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoLSkinExplorer.Views
{



    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChampionPage : ContentPage
    {
        

        public Champion MainChampion { set; get; }
        public ObservableCollection<Abilities> ChampAbilities { get; set; }

        public string baseSpellIconLink;
        public string basePassiveLink;
        public ChampionPage(Champion TempChampion)
        {
            InitializeComponent();
            MainChampion = new Champion();
            MainChampion = TempChampion;
            ChampAbilities = new ObservableCollection<Abilities>();

            BindingContext = this;
            
            basePassiveLink = "http://ddragon.leagueoflegends.com/cdn/13.1.1/img/passive/";
            baseSpellIconLink = "http://ddragon.leagueoflegends.com/cdn/13.1.1/img/spell/" + MainChampion.Alias;
            GetChampionCoreData(MainChampion);//pass the main champion to the function to get the core data such as lore, title,role, etc.
            //Application.Current.MainPage.DisplayAlert("Alias", MainChampion.Name, "OK");
        }


        public void GetChampionCoreData(Champion champion)
        {
            Champion _MainChampion = champion;
            //BioLabel.Text = MainChampion.Bio;
            string championRoles = "";
            if (_MainChampion.Role.Count() == 2)
            {
                Type1Label.Text = _MainChampion.Role[0].ToUpper();
                Type2Label.Text = _MainChampion.Role[1].ToUpper();
            }
            else
            {
                Type1Label.Text = _MainChampion.Role[0].ToUpper();
                Type2Label.IsVisible = false;
                Role2image.IsVisible = false;
            }
            _MainChampion.SpellP = basePassiveLink + _MainChampion.Alias + "_P.png";
            //Application.Current.MainPage.DisplayAlert("Link", MainChampion.SpellP, "OK ");

            //SpellPImg.Source = MainChampion.SpellP;
            _MainChampion.SpellQ = baseSpellIconLink + "Q.png";
            //SpellQImg.Source = MainChampion.SpellQ;

            _MainChampion.SpellW = baseSpellIconLink + "W.png";
            //SpellWImg.Source = MainChampion.SpellW;

            _MainChampion.SpellE = baseSpellIconLink + "E.png";
            //SpellEImg.Source = MainChampion.SpellE;

            _MainChampion.SpellR = baseSpellIconLink + "R.png";
            //SpellRImg.Source = MainChampion.SpellR;
            //championForXaml = MainChampion;
            //PassiveLBL.Text = championForXaml.Abilities[0].SpellDescription;
            _MainChampion.ChampionTitle = _MainChampion.ChampionTitle.ToUpper();
            MainChampion = _MainChampion;
            AddAbilitiesToCollection();
            OnPropertyChanged(nameof(MainChampion));
            //Application.Current.MainPage.DisplayAlert("Link", MainChampion.SpellQ, "OK ");
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
    }
}