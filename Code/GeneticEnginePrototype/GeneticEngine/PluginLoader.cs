using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;

namespace GeneticEngineCore
{
    /// <summary>
    /// Loads .Net DLL files and provides methods to identify and instantiate plug-in classes from them.
    /// </summary>
    public class PluginLoader
    {        
        /// <summary>
        /// The constructors of the plugin classes loaded. 
        /// </summary>
        Dictionary<string, ConstructorInfo> constructors = new Dictionary<string,ConstructorInfo>();
        
        /// <summary>
        /// Parameter type array to identify plug-in constructors
        /// </summary>
        private static Type[] ConstructorParamTypes = new Type[] { typeof(object) };

        /// <summary>
        /// Load the plug-in classes from a DLL file
        /// </summary>
        /// <param name="path">The path to the DLL file</param>
        public void LoadDll(string path)
        {
            Assembly assembly = Assembly.LoadFrom(path);

            Type[] types = assembly.GetTypes();

            foreach (Type t in types)
            {
                ConstructorInfo constructor = t.GetConstructor(ConstructorParamTypes);

                if (constructor != null)
                {
                    constructors.Add(t.Name, constructor);
                }
            }
        }

        /// <summary>
        /// Get a list of plug-ins which have been loaded.
        /// </summary>
        /// <param name="pluginType">
        /// The type of plug-ins to list.
        /// If null or omitted then all plug-ins are returned. 
        /// Otherwise only plug-ins which can be cast to the given type are returned.
        /// </param>
        /// <returns>A list of plug-in names.</returns>
        public List<string> GetPluginNames(Type pluginType=null)
        {
            List<string> pluginNames = new List<string>();

            foreach (string pluginName in constructors.Keys)
            {
                if (pluginType == null || pluginType.IsAssignableFrom(constructors[pluginName].DeclaringType))
                {
                    pluginNames.Add(pluginName);
                }
            }

            return pluginNames;
        }

        /// <summary>
        /// Get an instance of a plug-in type.
        /// </summary>
        /// <param name="pluginName">The name of the plug-in type as returned by GetPluginNames()</param>
        /// <param name="config">The configuration object which will be passes to the constructor.</param>
        /// <returns>A new instance of the plug-in type.</returns>
        private object GetInstance(string pluginName, object config)
        {
            ConstructorInfo constructor = constructors[pluginName];
            return constructor.Invoke(new Object[] { config });
        }          
    }
}
