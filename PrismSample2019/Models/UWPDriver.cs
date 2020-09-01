using PrismSample2019.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PrismSample2019.Models
{
    public class UWPDriver : IDriver
    {
        private readonly ICar _car;

        public UWPDriver(ICar car)
        {
            _car = car;
        }
        public async Task RunCarAsync()
        {
            var message = $"Running {_car.GetType().Name} - {_car.Run()} Mile";
            var dialog = new MessageDialog(message);
            _ = await dialog.ShowAsync();
        }
    }
}
