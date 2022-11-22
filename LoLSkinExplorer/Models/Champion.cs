using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace LoLSkinExplorer.Models
{
    public class Champion : INotifyPropertyChanged
    {
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
        public string Key;
        public string ChampionKey {
            get => Key;
            set
            {
                if(Key == value) return;
                Key = value;
                NotifyPropertyChanged(nameof(ChampionKey));
            }
        }
        public string Title;
        public string ChampionTitle {
            get => Title;
            set
            {
                if(Title == value) return;
                Title = value;
                NotifyPropertyChanged(nameof(ChampionTitle));
            }
        }
        //public List<Skin> _Skins;
        public List<Skin> ChampionSkins = new List<Skin>();

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
