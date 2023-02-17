using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LoLSkinExplorer.Models
{
    public class Champion : INotifyPropertyChanged
    {
        [JsonProperty("alias")]
        public string Alias;
        public string ChampionAlias
        {
            get => Alias;
            set
            {
                if (Alias == value)
                    return;
                Alias = value;
                NotifyPropertyChanged(nameof(ChampionAlias));
            }
        }

        [JsonProperty("title")]
        public string Title;
        public string ChampionTitle
        {
            get => Title;
            set
            {
                if (Title == value)
                    return;
                Title = value;
                NotifyPropertyChanged(nameof(ChampionTitle));
            }
        }


        [JsonProperty("name")]
        public string Name;
        public string ChampionName
        {
            get => Name;
            set
            {
                if (Name == value) return;
                Name = value;
                NotifyPropertyChanged(nameof(ChampionName));
            }
        }


        [JsonProperty("id")]
        public string ID;
        public string ChampionId
        {
            get => ID;
            set
            {
                if (ID == value) return;
                ID = value;
                NotifyPropertyChanged(nameof(ChampionId));
            }
        }


        [JsonProperty("spells")]
        public List<Abilities> Abilities = new List<Abilities>();


        public List<string> Role = new List<string>();


        public string spellP;
        public string SpellP
        {
            get => spellP;
            set
            {
                if (spellP == value)
                    return;
                spellP = value;
                NotifyPropertyChanged(nameof(spellP));
            }
        }
        public string spellQ;
        public string SpellQ
        {
            get => spellQ;
            set
            {
                if (spellQ == value)
                    return;
                spellQ = value;
                NotifyPropertyChanged(nameof(spellQ));
            }
        }
        public string spellW;
        public string SpellW
        {
            get => spellW;
            set
            {
                if (spellW == value)
                    return;
                spellW = value;
                NotifyPropertyChanged(nameof(spellW));
            }
        }
        public string spellE;
        public string SpellE
        {
            get => spellE;
            set
            {
                if (spellE == value)
                    return;
                spellE = value;
                NotifyPropertyChanged(nameof(spellE));
            }
        }
        public string spellR;
        public string SpellR
        {
            get => spellR;
            set
            {
                if (spellR == value)
                    return;
                spellR = value;
                NotifyPropertyChanged(nameof(spellR));
            }
        }




        [JsonProperty("skins")]
        public List<Skin> ChampionSkins = new List<Skin>();


        [JsonProperty("shortBio")]
        public string _Bio;
        public string Bio
        {
            get => _Bio;
            set
            {
                if (Bio == value)
                    return;
                Bio = value;
                NotifyPropertyChanged(nameof(_Bio));
            }
        }

        public string _Image;

        public string ChampionImage
        {
            get => _Image;
            set
            {
                if (_Image == value) return;
                _Image = value;
                NotifyPropertyChanged(nameof(ChampionImage));
            }
        }

        public string _LoadingScreen;
        public string ChampionLoadingScreen
        {
            get => _LoadingScreen;
            set
            {
                if (_LoadingScreen == value) return;
                _LoadingScreen = value;
                NotifyPropertyChanged(nameof(_LoadingScreen));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

