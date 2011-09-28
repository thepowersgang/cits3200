using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;

namespace GeneticEngineCore
{
    public class PluginLoader
    {
        Assembly assembly;

        public PluginLoader(string path)
        {
            assembly = Assembly.LoadFrom(path); 
        }

        public List<string> GetPlugins(Type pluginType = null)
        {            
            Type[] classes = assembly.GetTypes();

            List<string> plugins = new List<string>();
            
            foreach(Type t in classes) {
                if (pluginType == null || pluginType.IsAssignableFrom(t))
                {
                    t.GetConstructor(new Type[] { typeof(object) });
                    plugins.Add(t.Name);
                }
            }

            return plugins;
        }
        
        private object GetInstance(Type type, object config)
        {               
            ConstructorInfo constructor = type.GetConstructor(new Type[]{typeof(Object)});
            return constructor.Invoke(new Object[]{config});
        }        
    }
}
