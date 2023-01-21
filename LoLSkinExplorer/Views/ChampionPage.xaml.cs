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
        Champion championForXaml = new Champion();
        public ChampionPage(Champion TempChampion)
        {
            InitializeComponent();
            Champion MainChampion = TempChampion as Champion;
            championForXaml = TempChampion;
            BindingContext = championForXaml;
            //Application.Current.MainPage.DisplayAlert("source", TempChampion.ChampionImage, "OK");
            BackGrounImage.Source = MainChampion.ChampionLoadingScreen;
            BindingMode bindingMode = BindingMode.TwoWay;
            
            Title = MainChampion.Name.ToUpper();

            GetChampionCoreData(MainChampion);//pass the main champion to the function to get the core data such as lore, title,role, etc.

        }
        /// <summary>
        /// This method gets the champion abilities from the json files and stores them in collection list
        /// </summary>
        public void GetChampionAbilities(Champion _MainChampion)
        {
           
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
        }
    }
}