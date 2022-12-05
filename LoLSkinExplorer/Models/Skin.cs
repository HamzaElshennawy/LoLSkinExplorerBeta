using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace LoLSkinExplorer.Models
{
    public class Skin : INotifyPropertyChanged
    {
        [JsonProperty ("id")]
        public string skinId { get; set; }
        public string SkinID {
            get => skinId;
            set
            {
                if(skinId == value)
                    return;
                skinId = value;
                NotifyPropertyChanged(nameof(SkinID));
            }
        }

        [JsonProperty("num")]
        public int skinNum;
        public int SkinNum {
            get => skinNum; 
            set
            {
                if (skinNum == value)
                    return;
                skinNum = value;
                NotifyPropertyChanged(nameof(SkinNum));
            }
        }
        [JsonProperty("name")]
        public string skinName;
        public string SkinName {
            get => skinName;
            set
            {
                if (skinName == value)
                    return;
                skinName = value;
                NotifyPropertyChanged(nameof(SkinName));
            }
        }
        [JsonProperty("chromas")]
        public List<Chromas> chromas;
        public List<Chromas> Chromas
        {
            get => chromas;
            set
            {
                if (chromas == value)
                    return;
                chromas = value;

                NotifyPropertyChanged(nameof(chromas));
            }
        }
        
        
        //public bool hasChromas;
        //public bool HasChromas {
        //    get => hasChromas;
        //    set
        //    {
        //        if (hasChromas == value)
        //            return;
        //        NotifyPropertyChanged(nameof(HasChromas));
        //    }
        //}
        public string imgLink;
        public string ImgLink {
            get => imgLink;
            set
            {
                if (imgLink == value)
                    return;
                imgLink = value;
                NotifyPropertyChanged(nameof(ImgLink));
            }
        }

        public string _LoadingScreen;
        public string LoadingScreen
        {
            get => _LoadingScreen;
            set
            {
                if(_LoadingScreen == value)
                    return;
                _LoadingScreen = value;
                NotifyPropertyChanged(nameof(_LoadingScreen));
            }
        }

        [JsonProperty("rarity")]
        public string skinType;
        public string SkinType
        {
            get => skinType;
            set
            {
                if (skinType == value)
                    return;
                skinType = value;
                NotifyPropertyChanged(nameof(SkinType));
            }
        }

        public int skinPrice;
        public int SkinPrice
        {
            get => skinPrice;
            set
            {
                if (skinPrice == value)
                    return;
                skinPrice = value;
                NotifyPropertyChanged(nameof(SkinPrice));
            }
        }
        
        [JsonProperty("isLegacy")]
        public bool isavailable;
        public bool IsAvailable
        {
            get => isavailable;
            set
            {
                if (isavailable == value)
                    return;
                isavailable = value;
                NotifyPropertyChanged(nameof(isavailable));
            }
        }

        public string availableString;
        public string AvailableString
        {
            get => availableString;
            set
            {
                if (availableString == value)
                    return;
                availableString = value;
                NotifyPropertyChanged(nameof(availableString));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
