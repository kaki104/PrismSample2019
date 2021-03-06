﻿using Microsoft.Practices.Unity;
using Prism.Unity.Windows;
using PrismSample2019.Services;
using PrismSample2019.ViewModels;

using Windows.UI.Xaml.Controls;

namespace PrismSample2019.Views
{
    public sealed partial class WebViewPage : Page
    {
        private WebViewViewModel ViewModel => DataContext as WebViewViewModel;

        public WebViewPage()
        {
            InitializeComponent();

            // This is an unusual way to initialize a service to a ViewModel, but since this service
            // requires a reference to the WebView this is one way to provide the required reference.
            // In this case teh WebViewService isn't a traditional Service but more of a shim to provide to better
            // separation of View and ViewModel and unit testing of a ViewModel that uses the WebViewService since the
            // WebViewService implements the IWebViewService interface that allows for mocking of the service.
            //ViewModel.WebViewService = new WebViewService(webView);
            var service = PrismUnityApplication.Current.Container
                .Resolve(typeof(IWebViewService), ""
                    , new ParameterOverride("webViewInstance", webView)) as IWebViewService;
            ViewModel.WebViewService = service;


            var service2 = PrismUnityApplication.Current.Container
                    .Resolve(typeof(IWebViewService), ""
                    , new ParameterOverride("webViewInstance", webView2)) as IWebViewService;
            ViewModel.WebViewService2 = service2;

        }
    }
}
