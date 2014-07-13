using System;

namespace DomainA
{
    /// <summary>
    /// Sample Guids of process types in the domain. We'll use it in the example 
    /// to keep a couple of easy to identify ids
    /// </summary>
    public class ProcessGuids
    {
        public static Guid ProcessA = new Guid(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        public static Guid ProcessB = new Guid(2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        public static Guid ProcessC = new Guid(3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    }
}
