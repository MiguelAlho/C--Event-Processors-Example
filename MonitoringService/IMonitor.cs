using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DomainA.Events;
using Events;

namespace MonitoringService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMonitor" in both code and config file together.
    [ServiceContract]
    public interface IMonitor
    {

        [OperationContract]
        [ServiceKnownType("RegisterKnownTypes", typeof(ServiceKnownTypeRegister))]
        void ReceiveEvent(Event @event);

       
    }
}
