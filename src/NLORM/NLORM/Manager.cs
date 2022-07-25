using System.Reflection;
using System.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLORM.Core;

namespace NETORM
{
    public static class Manager
    {
        // Construct the dictionary with the desired concurrencyLevel and initialCapacity
        private static ConcurrentDictionary<Guid, string> cd;

        private static IEnumerable<Type> ExportedTypes;

        private static INETORMDb Instance;

        static Manager()
        {
            // The higher the concurrencyLevel, the higher the theoretical number of operations
            // that could be performed concurrently on the ConcurrentDictionary.  However, global
            // operations like resizing the dictionary take longer as the concurrencyLevel rises.
            // For the purposes of this example, we'll compromise at numCores * 2.
            int numProcs = Environment.ProcessorCount;
            int concurrencyLevel = numProcs * 2;

        }

        public static INETORMDb Init(string connectionString, SupportedDb dbType)
        {
            Instance = NETORMFactory.Instance.GetDb(connectionString, dbType);

            LoadObjects();

            return Instance;
        }


        private static void LoadObjects() 
        {
            // Only public ones
            ExportedTypes = Assembly.GetEntryAssembly()
                                  .GetExportedTypes()
                                  .Where(i => i.CustomAttributes.Any(a => a.AttributeType == typeof(TableAttribute)));

            CreateTables();
        }

        private static void CreateTables()
        {
            var tabelTasks = ExportedTypes.Select(i => Task.Run(() => { }));
        }

        // TODO on hold
        private static void CreateRelations() { }

    }
}
