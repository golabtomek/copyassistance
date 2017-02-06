using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace Copystance.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //
        private ObservableCollection<string> ClipboardHistory { get; } = new ObservableCollection<string>();
        private ObservableCollection<string> SearchResult { get; set; } = new ObservableCollection<string>();
        
        public ObservableCollection<string> List
        {
            get
            {
                if (SearchResult.Count == 0 && (SearchBarText==""||SearchBarText=="Search")) return ClipboardHistory;
                else return SearchResult;
            }
        }

        public MainViewModel()
        {
            this.ClipboardUpdateCommand = new DelegateCommand(OnClipboardUpdate, OnCanClipboardUpdate);
        }

        private string selectedItem;
        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem == value)
                    return;
                selectedItem = value;
                if(selectedItem!=null)
                Clipboard.SetText(SelectedItem);
            }
        }

        private void CopyToClipboard()
        {
            Clipboard.SetText(SelectedItem);
        }

        #region clipboardupdate
        public DelegateCommand ClipboardUpdateCommand { get; private set; }

        public bool CheckHistory(string text)
        {
            bool isTextAlreadyInHistory = false;
            foreach(string t in ClipboardHistory)
            {
                if (text == t) isTextAlreadyInHistory=true;
            }
            return isTextAlreadyInHistory;
        }

        public void OnClipboardUpdate()
        {
            string text = Clipboard.GetText();
            if(text!=null && text!="" && text!=" " && CheckHistory(text)!=true )
                 ClipboardHistory.Add(text);
        }
        

        public bool OnCanClipboardUpdate()
        {
            return true;
        }

        private string searchBarText = "Search";
        public string SearchBarText
        {
            get { return searchBarText; }
            set
            {
                if (searchBarText == value)
                    return;
                searchBarText = value;
                RaisePropertyChanged("searchBarText");
                if (SearchBarText != "" || SearchBarText != "Search")
                    Search();
                else
                    SearchResult = new ObservableCollection<string>();
            }
        }
        
        public void Search()
        {
            SearchResult = new ObservableCollection<string>();
            foreach(string item in ClipboardHistory)
            {
                string item_downcase = item.ToLower();
                if (item_downcase.Contains(SearchBarText.ToLower()))
                    SearchResult.Add(item);
            }
            RaisePropertyChanged("List");
        }

        #endregion

        #region Commands

        #endregion

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }

}
