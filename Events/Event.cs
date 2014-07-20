using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Events
{
    /// <summary>
    /// Event Base class. Could be in another project / class library for sharing
    /// </summary>
    [DataContract]
    [KnownType("DerivedTypes")]
    public abstract class Event
    {
        /// <summary>
        /// Unique Id Event
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// DateTime the event was created
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Base constructor for Events. Initializes Id and creation time
        /// </summary>
        public Event()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }


        #region Known types resolution

        //-Next block: known types resolution based on : 
        //http://stackoverflow.com/questions/16220242/generally-accepted-way-to-avoid-knowntype-attribute-for-every-derived-class

        /// <summary>
        /// This method is used to scan assemblies and get all known types with 
        /// Event as it's base, inorder for service deserialization to work 
        /// correctly
        /// </summary>
        /// <returns></returns>
        private static Type[] DerivedTypes()
        {
            return GetDerivedTypes(typeof(Event), Assembly.GetExecutingAssembly()).ToArray();
        }

        /// <summary>
        /// Gets types -> can be moved to a utility class
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

        #endregion
    }
}
