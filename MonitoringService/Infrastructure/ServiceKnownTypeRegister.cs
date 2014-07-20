using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Events;

namespace MonitoringService
{
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