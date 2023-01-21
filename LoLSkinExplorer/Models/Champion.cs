using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

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
                if(Alias == value)
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
        public string ChampionName {
            get => Name;
            set
            {
                if(Name == value) return;
                Name = value;
                NotifyPropertyChanged(nameof(ChampionName));
            }
        }


        [JsonProperty("id")]
        public string ID;
        public string ChampionId {
            get => ID;
            set
            {
                if(ID == value) return;
                ID = value;
                NotifyPropertyChanged(nameof(ChampionId));
            }
        }
        //public string Key;
        //public string ChampionKey {
        //    get => Key;
        //    set
        //    {
        //        if(Key == value) return;
        //        Key = value;
        //        NotifyPropertyChanged(nameof(ChampionKey));
        //    }
        //}

        //public string Title;
        //public string ChampionTitle {
        //    get => Title;
        //    set
        //    {
        //        if(Title == value) return;
        //        Title = value;
        //        NotifyPropertyChanged(nameof(ChampionTitle));
        //    }
        //}

        [JsonProperty("spells")]
        public List<Abilities> Abilities;


        public List<string> Role = new List<string>();


        //public List<Skin> _Skins;
        [JsonProperty("skins")]
        public List<Skin> ChampionSkins = new List<Skin>();


        [JsonProperty("shortBio")]
        public string _Bio;
        public string Bio
        {
            get => _Bio;
            set
            {
                if(Bio == value) 
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
