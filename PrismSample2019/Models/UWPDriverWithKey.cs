using PrismSample2019.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PrismSample2019.Models
{
    public class UWPDriverWithKey : IDriver
    {
        private readonly ICar _car;
        private readonly ICarKey _key;

        public UWPDriverWithKey(ICar car, ICarKey key)
        {
            _car = car;
            _key = key;
        }
        public async Task RunCarAsync()
        {
            var message = $"Running {_car.GetType().Name} with {_key.GetType().Name} - {_car.Run()} Mile";
            var dialog = new MessageDialog(message);
            _ = await dialog.ShowAsync();
        }
    }
}
