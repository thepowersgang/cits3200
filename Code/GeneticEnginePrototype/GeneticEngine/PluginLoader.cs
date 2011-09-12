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

        public string[] GetPlugins()
        {            
            Type[] classes = assembly.GetTypes();

            string[] plugins = new string[classes.Length];

            int i=0;

            foreach(Type t in classes) {
                plugins[i++] = t.Name;
            }

            return plugins;
        }

        private object GetInstance(Type type, object[] parameters)
        {
            Type[] parameterTypes = new Type[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameterTypes[i] = parameters[i].GetType();
            }
                        
            ConstructorInfo constructor = type.GetConstructor(parameterTypes);
            return constructor.Invoke(parameters);
        }        
    }
}
