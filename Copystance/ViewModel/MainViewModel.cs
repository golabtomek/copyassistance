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
        public ObservableCollection<string> ClipboardHistory { get; } = new ObservableCollection<string>();

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
                RaisePropertyChanged("selectedItem");
                CopyToClipboard();
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
