using System;

using PrismSample2019.ViewModels;

using Windows.UI.Xaml.Controls;

namespace PrismSample2019.Views
{
    public sealed partial class MovieDetailPage : Page
    {
        private MovieDetailViewModel ViewModel => DataContext as MovieDetailViewModel;

        public MovieDetailPage()
        {
            InitializeComponent();
        }
    }
}
