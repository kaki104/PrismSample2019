using PrismSample2019.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PrismSample2019.Models
{
    public class UWPDriverRuntimeMethod : IDriver
    {
        private ICar _car;
        private ICarKey _key;

        public void MyCar(ICar car)
        {
            _car = car;
        }

        public void MyCarKey(ICarKey key)
        {
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
