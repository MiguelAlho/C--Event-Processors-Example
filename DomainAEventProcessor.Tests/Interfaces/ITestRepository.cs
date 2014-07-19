using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainAEventProcessor.Tests.Interfaces
{
    /// <summary>
    /// allows inspection into repository test doubles
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface ITestRepository<T>
    {
        Dictionary<string, T> GetPersistedData();
    }
}
