using Prism.Events;
using PrismSample2019.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismSample2019.Core.Events
{
    public class TextChangedEvent : PubSubEvent<TextChangedEventArgs>
    {
    }
}
