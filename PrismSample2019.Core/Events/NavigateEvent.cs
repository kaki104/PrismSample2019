using Prism.Events;
using PrismSample2019.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismSample2019.Core.Events
{
    /// <summary>
    /// 네비게이트 이벤트
    /// </summary>
    public class NavigateEvent : PubSubEvent<NavigateEventArgs>
    {
    }
}
