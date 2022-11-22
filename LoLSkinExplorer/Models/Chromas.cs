using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace LoLSkinExplorer.Models
{
    public class Chromas : INotifyPropertyChanged
    {
        public string Name;
        public string ChromaName
        {
            get => Name;
            set 
            {
                if(Name == value) 
                    return;
                Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public string id;
        public string ChromaID
        {
            get => id;
            set
            {
                if(id == value) 
                    return;
                id = value;
                NotifyPropertyChanged(nameof(id));
            }
        }

        public string path;
        public string ChromaPath
        {
            get => path;
            set
            {
                if(path == value)
                    return;
                path = value;
                NotifyPropertyChanged(nameof(path));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
