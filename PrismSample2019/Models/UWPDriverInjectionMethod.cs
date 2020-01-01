using Microsoft.Practices.Unity;
using PrismSample2019.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PrismSample2019.Models
{
    public class UWPDriverInjectionMethod : IDriver
    {
        private ICar _car;

        [InjectionMethod]
        public void UseCar(ICar car)
        {
            _car = car;
        }

        public async Task RunCarAsync()
        {
            var message = $"Running {_car?.GetType().Name} - {_car?.Run()} Mile";
            var dialog = new MessageDialog(message);
            _ = await dialog.ShowAsync();
        }
    }
}
