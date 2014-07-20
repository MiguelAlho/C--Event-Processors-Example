using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    public class ServiceKnownTypeRegister
    {
        //-Next block: known types resolution based on : 
        //http://stackoverflow.com/questions/16220242/generally-accepted-way-to-avoid-knowntype-attribute-for-every-derived-class
        // and http://stackoverflow.com/questions/3044078/provide-serviceknowntype-during-runtime

        /// <summary>
        /// This method is used to scan assemblies and get all known types with 
        /// Event as it's base, in order for service deserialization to work 
        /// correctly
        /// </summary>
        static public IEnumerable<Type> RegisterKnownTypes(ICustomAttributeProvider provider)
        {
            List<Type> types = new List<Type>();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach(Assembly assembly in assemblies)
            {
                types.AddRange(GetDerivedTypes(typeof(Event), assembly));
            }

            return types;
        }

        
        /// <summary>
        /// Gets types -> can be moved to a utilizty class
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static IEnumerable<Type> GetDerivedTypes(Type baseType, Assembly assembly)
        {
            var types = from t in assembly.GetTypes()
                        where t.IsSubclassOf(baseType)
                        select t;

            return types;
        }
    }


}
