using Prism.Events;
using PrismSample2019.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismSample2019.Core.Events
{
    /// <summary>
    /// 스크립트 실행 이벤트
    /// </summary>
    public class RunScriptEvent : PubSubEvent<RunScriptEventArgs>
    {
    }
}
