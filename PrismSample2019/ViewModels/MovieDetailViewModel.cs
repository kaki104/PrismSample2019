using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using PrismSample2019.Core.Helpers;
using PrismSample2019.Core.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Windows.UI.Xaml.Navigation;

namespace PrismSample2019.ViewModels
{
    public class MovieDetailViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private string _movieId;
        private MovieDetail _currentMovieDetail;

        public MovieDetailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                Debug.WriteLine(e.Parameter);
                _movieId = e.Parameter as string;

                GetMovieDetail();
            }
        }

        private async void GetMovieDetail()
        {
            if (string.IsNullOrEmpty(_movieId))
            {
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                string url = $"http://www.omdbapi.com/?i={_movieId}&apikey=32941a88";
                string result = await client.GetStringAsync(url);
                if (string.IsNullOrEmpty(result))
                {
                    return;
                }

                MovieDetail movieDetail = await Json.ToObjectAsync<MovieDetail>(result);
                if (movieDetail == null)
                {
                    return;
                }

                CurrentMovieDetail = movieDetail;
            }
        }
        /// <summary>
        /// 현재 영화 상세
        /// </summary>
        public MovieDetail CurrentMovieDetail
        {
            get => _currentMovieDetail;
            set => SetProperty(ref _currentMovieDetail, value);
        }
    }
}
