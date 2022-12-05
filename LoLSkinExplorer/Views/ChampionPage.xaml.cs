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
        public ChampionPage(Champion TempChampion)
        {
            InitializeComponent();
            Champion MainChampion = TempChampion as Champion;
            BindingContext = MainChampion;
            //Application.Current.MainPage.DisplayAlert("source", TempChampion.ChampionImage, "OK");
            BackGrounImage.Source = MainChampion.ChampionLoadingScreen;
            BindingMode bindingMode = BindingMode.TwoWay;
            
            Title = MainChampion.Name;
            BioLabel.Text = MainChampion.Bio;
            //Application.Current.MainPage.DisplayAlert("BIO", MainChampion.Bio, "OK");
        }
        
        public void GetChampionAbilities(Champion _MainChampion)
        {
            //string name = _ChampName;



            //var tmp = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(AboutPage)).Assembly;
            //System.IO.Stream s = tmp.GetManifestResourceStream($"LoLSkinExplorer.Champions.{name}.json");
            //System.IO.StreamReader sr = new System.IO.StreamReader(s);

            //string JsonText = sr.ReadToEnd();
            //JObject dobj = JsonConvert.DeserializeObject<dynamic>(JsonText);







        }
    }
}