using LoLSkinExplorer.Models;
using MvvmHelpers;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoLSkinExplorer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChromasPage : ContentPage
    {
        string BaseChromaLink = "https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/v1/champion-chroma-images/";
        public string NumberOfChromas;
        public ObservableRangeCollection<Chromas> _Chromas { set; get; }
        public ChromasPage(Skin MainSkin, string champID)
        {
            InitializeComponent();
            BindingContext = this;
            _Chromas = new ObservableRangeCollection<Chromas>();
            GetChromas(MainSkin, champID);
        }
        public void GetChromas(Skin skin, string champID)
        {
            foreach (Chromas chroma in skin.Chromas)
            {
                chroma.ChromaPath = BaseChromaLink + champID + "/" + chroma.ChromaID + ".png";
                _Chromas.Add(chroma);
            }
            OnPropertyChanged(nameof(_Chromas));
            //NumberOfChromas = "Number of Cromas: "+_Chromas.Count().ToString();
            //NumberOfChromasLBL.Text = NumberOfChromas;
            //OnPropertyChanged(nameof(NumberOfChromas));
        }
    }
}