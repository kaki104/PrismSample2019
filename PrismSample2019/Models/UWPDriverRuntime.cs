using PrismSample2019.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PrismSample2019.Models
{
    public class UWPDriverRuntime : IDriver
    {
        public ICar MyCar { get; set; }

        public async Task RunCarAsync()
        {
            var message = $"Running {MyCar?.GetType().Name} - {MyCar?.Run()} Mile";
            var dialog = new MessageDialog(message);
            _ = await dialog.ShowAsync();
        }
    }
}
