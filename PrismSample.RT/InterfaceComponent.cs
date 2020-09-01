using Prism.Events;
using Prism.Unity.Windows;
using PrismSample2019.Core.Enums;
using PrismSample2019.Core.Events;
using PrismSample2019.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace PrismSample.RT
{
    [AllowForWeb]
    public sealed class InterfaceComponent
    {
        private string _answer;
        private readonly IEventAggregator _eventAggregator;

        public InterfaceComponent()
        {
            _eventAggregator = PrismUnityApplication.Current.Container.TryResolve<IEventAggregator>();
        }

        public string GetAnswer()
        {
            //return "The answer is 42.";
            var answer = _answer ?? "null";
            return $"The answer is {answer}";
        }

        public void SetAnswer(string answer)
        {
            _answer = answer;
        }

        public IAsyncOperation<string> GetUriContentAsync(string uri)
        {
            return GetUriContentHelperAsync(uri).AsAsyncOperation();
        }
        private async Task<string> GetUriContentHelperAsync(string uri)
        {
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync(uri);
            return content;
        }

        public void GoHome()
        {
            _eventAggregator.GetEvent<NavigateEvent>()
                .Publish(new NavigateEventArgs
                {
                    NavigateEventAction = NavigateEventAction.GoHome
                });
        }

        public void GoMovie()
        {
            _eventAggregator.GetEvent<NavigateEvent>()
                .Publish(new NavigateEventArgs
                {
                    NavigateEventAction = NavigateEventAction.Navigate,
                    NavigatePageName = "Movie"
                });
        }

        public void TextBoxTextChanged(string id, string text)
        {
            _eventAggregator.GetEvent<TextChangedEvent>()
                .Publish(new TextChangedEventArgs
                {
                    Id = id,
                    Text = text
                });
        }
    }
}
