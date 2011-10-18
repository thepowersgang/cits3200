using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
using RoadNetworkDefinition;

namespace RoadNetworkSolver
{
    public class AllInOneOperator : IGeneticOperator
    {
        public AllInOneOperator(object config)
        {
        }

        public void Operate(IGeneration source, ArrayList destination)
        {
            int index1 = 1;

            while (destination.Count < source.Count)
            {
                RoadNetwork parent1 = (RoadNetwork)source[index1].Individual;
                destination.Add(new RoadNetwork(parent1));

                if (destination.Count < source.Count)
                {
                    destination.Add(StepMutator.Mutate(parent1));
                }

                int index2 = 0;

                while (index2 < index1 && destination.Count < source.Count)
                {
                    RoadNetwork parent2 = (RoadNetwork)source[index2].Individual;

                    RoadNetwork child1;
                    RoadNetwork child2;

                    StepConjugator.Conjugate(parent1, parent2, out child1, out child2);

                    destination.Add(child1);

                    if (destination.Count < source.Count)
                    {
                        destination.Add(child2);
                    }

                    index2++;
                }

                index1++;
            }
        }
    }
}
