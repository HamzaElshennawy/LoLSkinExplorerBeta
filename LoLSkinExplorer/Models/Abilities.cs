using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LoLSkinExplorer.Models
{
    public class Abilities : INotifyPropertyChanged
    {

        //https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/assets/characters/{championAliis}/hud/icons2d/{championalies+spellKey}.png

        [JsonProperty("spellKey")]
        public string _SpellKey;
        public string SpellKey
        {
            get => _SpellKey;
            set
            {
                if(value == _SpellKey) 
                    return;
                _SpellKey = value;
                NotifyPropertyChanged(nameof(_SpellKey));
            }
        }


        [JsonProperty("name")]
        public string _SpellName;
        public string SpellName
        {
            get => _SpellName;
            set
            {
                if(value == _SpellName) 
                    return;
                _SpellName = value;
                NotifyPropertyChanged(nameof(_SpellName));
            }
        }

        [JsonProperty("description")]
        public string _SpellDescription;
        public string SpellDescription
        {
            get => _SpellDescription;
            set
            {
                if(value == _SpellDescription) 
                        return;
                _SpellDescription = value;
                NotifyPropertyChanged(nameof(_SpellDescription));
            }
        }

        [JsonProperty("range")]
        public List<string> _SpellRange = new List<string>();

        [JsonProperty("cooldownCoefficients")]
        public List<string> _SpellCoolDowns = new List<string>();



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
