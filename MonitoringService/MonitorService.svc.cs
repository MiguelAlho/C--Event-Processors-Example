using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MonitoringService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MonitorService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MonitorService.svc or MonitorService.svc.cs at the Solution Explorer and start debugging.
    public class MonitorService : IMonitor
    {
        public void ReceiveEvent(Event value)
        {
            throw new NotImplementedException();
        }
    }
}
