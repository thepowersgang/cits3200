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
        /// Get a list of plugin classed available in the DLL.
        /// To be considered a plugin the class must have a constructor which accepts one object as its only parameter.
        /// </summary>
        /// <param name="pluginType">The type of plugins to list. If supplied only classes which can be cast as this type will be listed.</param>
        /// <returns></returns>
        public List<string> GetPlugins(Type pluginType)
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
        
        /// <summary>
        /// Get an instance of a class from this DLL
        /// </summary>
        /// <param name="typeName">The name of the class</param>
        /// <param name="config">The configuration object to pass to the class's constructor</param>
        /// <returns>A new instance of the class.</returns>
        private object GetInstance(string typeName, object config)
        {
            Type type = assembly.GetType(typeName);
            ConstructorInfo constructor = type.GetConstructor(new Type[]{typeof(Object)});
            return constructor.Invoke(new Object[]{config});
        }        
    }
}
