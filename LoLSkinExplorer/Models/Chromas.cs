using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace LoLSkinExplorer.Models
{
    public class Chromas : INotifyPropertyChanged
    {
        [JsonProperty("name")]
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
        [JsonProperty("id")]
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
        
        public class Description : Chromas
        {
            [JsonProperty("region")]
            public string region;
            public string Region
            {
                get => region;
                set
                {
                    if(region == value)
                        return;
                    region = value;
                    NotifyPropertyChanged(nameof(region));
                }
            }
            [JsonProperty("description")]
            public string description;
            public string ChromaDescription
            {
                get => description;
                set
                {
                    if(description == value) return;
                    description = value;
                    NotifyPropertyChanged(nameof(description));                
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
