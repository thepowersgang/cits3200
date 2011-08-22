using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEnginePlugin;

namespace CITS3200GeneticEngine
{
    public class PluginLoader
    {
        Assembly assembly;

        public PluginLoader(string path)
        {
            assembly = Assembly.LoadFrom(path); 
        }

        public string[] getPlugins()
        {            
            Type[] classes = assembly.GetTypes();

            string[] plugins = new string[classes.Length];

            int i=0;

            foreach(Type t in classes) {
                plugins[i++] = t.Name;
            }

            return plugins;
        }

        public object getType(string typeName)
        {
            return assembly.GetType(typeName);
        }
        
        public IChromosomeGenerator GetChromosomeGenerator(string typeName)
        {
            return (IChromosomeGenerator)assembly.CreateInstance(typeName);
        }

        public IEvaluator GetEvaluator(string typeName)
        {
            return (IEvaluator)assembly.CreateInstance(typeName);
        }

        public IGeneticOperator GetGeneticOperator(string typeName)
        {
            return (IGeneticOperator)assembly.CreateInstance(typeName);
        }

        public ITerminator GetTerminator(string typeName)
        {
            return (ITerminator)assembly.CreateInstance(typeName);
        }
    }
}
