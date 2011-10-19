using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
using GeneticAlgorithm.Plugin.Util;
using RoadNetworkDefinition;

namespace RoadNetworkSolver
{
    /// <summary>
    /// IEvaluator which evaluates RoadNetworks based on the total length of road and the
    /// total distance of each town from its closest vertex in the network.
    /// </summary>
    public class Evaluator : IEvaluator
    {
        /// <summary>
        /// Initialise a new Evaluator
        /// </summary>
        /// <param name="config">Configuration object (ignored)</param>
        public Evaluator(object config)
        {
        }

        /// <summary>
        /// Prepare the evaluator to process a new genration of individuals
        /// </summary>
        /// <param name="generationCount">The number of previous generations</param>
        /// <param name="individuals">The individuals which will be evaluated</param>
        public void Initialise(int generationCount, ArrayList individuals)
        {
        }

        /// <summary>
        /// Evaluate an individual
        /// </summary>
        /// <param name="individual">The individual to evaluate</param>
        /// <returns>The fitness value of the individual</returns>
        public uint Evaluate(object individual)
        {
            RoadNetwork network = (RoadNetwork)individual;

            double totalLength = 0.0;

            for (int i = 0; i < network.EdgeCount; i++)
            {
                Edge edge = network.GetEdge(i);
                totalLength += edge.Start.Coordinates.GetDistance(edge.End.Coordinates);
            }

            double totalDistance = 0.0;

            Map map = network.Map;

            for (int i = 0; i < map.TownCount; i++)
            {
                Coordinates town = map.GetTown(i);
                int minDistanceSquared = int.MaxValue;

                for (int j = 0; j < network.VertexCount; j++)
                {
                    int distanceSquared = network.GetVertex(j).Coordinates.GetDistanceSquared(town);
                    if (distanceSquared < minDistanceSquared)
                    {
                        minDistanceSquared = distanceSquared;
                    }
                }

                totalDistance += Math.Sqrt(minDistanceSquared);
            }

            double totalCost = totalLength + totalDistance * totalDistance;

            // This check was causing performance problems so I've removed it.
            // What's the worst that could happen?
            //
            //if (thisAlgorithmBecomesSkynet())
            //{
            //    totalCost = double.PositiveInfinity;
            //}
            
            double floatingPointFitness = 1.0 / (totalCost + 1.0);
            return FitnessConverter.FromFloat((float)floatingPointFitness);
        }
    }
}
