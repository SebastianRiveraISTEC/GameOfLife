using GameOfLife.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameOfLife.Model
{
    public class PageHandler : INotifyPropertyChanged
    {
        private Uri currentPage;

        public Uri CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged();
            }
        }

        private void SwitchPage(string pageName)
        {
            switch (pageName)
            {
                case "Page1":
                    CurrentPage = new Uri("MainGame.xaml", UriKind.Relative);
                    break;
                case "Page2":
                    CurrentPage = new Uri("Add.xaml", UriKind.Relative);
                    break;
                case "Page3":
                    CurrentPage = new Uri("Rules.xaml", UriKind.Relative);
                    break;
            }
        }

        public void Quit(object nothing)
        {
            Trace.WriteLine("Shutdown called");
            Application.Current.Shutdown();
        }

        public PageHandler()
        {

            CurrentPage = new Uri("MainGame.xaml", UriKind.Relative);
            SwitchPageCommand = new RelayCommand<string>(SwitchPage);
            QuitCommand = new RelayCommand<object>(Quit);
        }

        public ICommand SwitchPageCommand { get; }
        public ICommand QuitCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
