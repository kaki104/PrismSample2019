using System;

using PrismSample2019.ViewModels;

using Windows.UI.Xaml.Controls;

namespace PrismSample2019.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
