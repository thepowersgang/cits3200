using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GeneticAlgorithm.Util
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
            try
            {
                Assembly assembly = Assembly.LoadFrom(path);

                Type[] types = assembly.GetTypes();

                foreach (Type t in types)
                {
                    ConstructorInfo constructor = t.GetConstructor(ConstructorParamTypes);

                    if (constructor != null)
                    {
                        if (!constructors.ContainsKey(t.FullName))
                        {
                            constructors.Add(t.FullName, constructor);
                        }
                    }
                }

            }
            catch (ArgumentNullException ex)
            {
                throw new GeneticEngineException("path must not be null", ex);
            }
            catch (ArgumentException ex)
            {
                throw new GeneticEngineException("Bad path", ex);
            }
            catch (FileNotFoundException ex)
            {
                throw new GeneticEngineException("Plugin file does not exist: " + path, ex);
            }
            catch (BadImageFormatException ex)
            {
                throw new GeneticEngineException("Plugin file is not a .Net DLL: " + path, ex);
            }
            catch (Exception ex)
            {
                throw new GeneticEngineException("Error loading plug-in file: " + path, ex);
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
        public object GetInstance(string pluginName, object config)
        {
            if (constructors.ContainsKey(pluginName))
            {
                ConstructorInfo constructor = constructors[pluginName];

                try
                {
                    return constructor.Invoke(new Object[] { config });
                }
                catch (Exception ex)
                {
                    throw new GeneticEngineException("Error initialising plug-in: " + pluginName, ex);
                }
            }
            else
            {
                throw new GeneticEngineException("Plug-in does not exist: " + pluginName);
            }
        }          
    }
}
