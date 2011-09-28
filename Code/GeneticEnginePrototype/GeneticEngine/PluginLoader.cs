using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;

namespace GeneticEngineCore
{
    /// <summary>
    /// Loads a .Net DLL and provides methods to identify and instantiate plugin classes from the DLL.
    /// </summary>
    public class PluginLoader
    {
        /// <summary>
        /// The DLL file loaded.
        /// </summary>
        Assembly assembly;

        /// <summary>
        /// Initialises a new instance of PluginLoader by loading a .Net DLL file.
        /// </summary>
        /// <param name="path">The path to the DLL file</param>
        public PluginLoader(string path)
        {
            assembly = Assembly.LoadFrom(path); 
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="pluginType"></param>
        /// <returns></returns>
        public List<string> GetPlugins(Type pluginType = null)
        {            
            Type[] classes = assembly.GetTypes();

            List<string> plugins = new List<string>();
            
            foreach(Type t in classes)
			{
                if (pluginType == null || pluginType.IsAssignableFrom(t))
                {
                    if (t.GetConstructor(new Type[] { typeof(object) }) != null)
                    {
                        plugins.Add(t.Name);
                    }
                }
            }

            return plugins;
        }
        
        private object GetInstance(string typeName, object config)
        {
            Type type = assembly.GetType(typeName);
            ConstructorInfo constructor = type.GetConstructor(new Type[]{typeof(Object)});
            return constructor.Invoke(new Object[]{config});
        }        
    }
}
