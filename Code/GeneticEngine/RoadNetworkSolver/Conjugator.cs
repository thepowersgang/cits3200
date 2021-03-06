﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
using RoadNetworkDefinition;

namespace RoadNetworkSolver
{
    /// <summary>
    /// IGeneticOperator which uses two parent RoadNetworks to generate two child RoadNetworks.
    /// Each of the parents is split into a start half partition and an end partition then the
    /// end of each is matched to the start of the other to produce the children.
    /// </summary>
    public class Conjugator : IGeneticOperator
    {
        /// <summary>
        /// The random number generator used when splitting and rejoining RoadNetworks
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Initialise a new Conjugator
        /// </summary>
        /// <param name="config">Configuration object (ignored)</param>
        public Conjugator(object config)
        {
        }

        /// <summary>
        /// Process a generation to produce the individuals which will make up the next generation
        /// </summary>
        /// <param name="source">The current generation</param>
        /// <param name="destination">An empty collection of individuals to be populated</param>
        public void Operate(IGeneration source, ArrayList destination)
        {
            int index1 = 1;

            while (destination.Count < source.Count)
            {
                int index2 = 0;

                while (index2 < index1 && destination.Count < source.Count)
                {
                    RoadNetwork parent1 = (RoadNetwork)source[index1].Individual;
                    RoadNetwork parent2 = (RoadNetwork)source[index2].Individual;

                    RoadNetwork child1;
                    RoadNetwork child2;

                    Conjugate(parent1, parent2, out child1, out child2);

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

        /// <summary>
        /// Generate two child RoadNetworks by recombining halves of two parent RoadNetworks.
        /// </summary>
        /// <param name="parent1">The first parent RoadNetwork</param>
        /// <param name="parent2">The second parent RoadNetwork</param>
        /// <param name="child1">The first child RoadNetwork</param>
        /// <param name="child2">The second child RoadNetwork</param>
        public static void Conjugate(RoadNetwork parent1, RoadNetwork parent2, out RoadNetwork child1, out RoadNetwork child2)
        {   
            Cut(parent1);
            Cut(parent2);
                     
            List<Vertex> startVertexPartition1 = new List<Vertex>();
            List<Vertex> endVertexPartition1 = new List<Vertex>();
            List<Vertex> startVertexPartition2 = new List<Vertex>();
            List<Vertex> endVertexPartition2 = new List<Vertex>();

            List<Edge> startEdgePartition1 = new List<Edge>();
            List<Edge> endEdgePartition1 = new List<Edge>();
            List<Edge> startEdgePartition2 = new List<Edge>();
            List<Edge> endEdgePartition2 = new List<Edge>();
            List<Edge> brokenEdgePartition1 = new List<Edge>();
            List<Edge> brokenEdgePartition2 = new List<Edge>();

            parent1.PartitionVertices(startVertexPartition1, endVertexPartition1);
            parent2.PartitionVertices(startVertexPartition2, endVertexPartition2);
            
            parent1.PartitionEdges(startEdgePartition1, endEdgePartition1, brokenEdgePartition1);
            parent2.PartitionEdges(startEdgePartition2, endEdgePartition2, brokenEdgePartition2);

            child1 = new RoadNetwork(parent1.Map);
            child2 = new RoadNetwork(parent2.Map);

            child1.CopyVertices(startVertexPartition1);
            child1.CopyEdges(startEdgePartition1);
            
            child1.CopyVertices(endVertexPartition2);
            child1.CopyEdges(endEdgePartition2);
                        
            child2.CopyVertices(startVertexPartition2);
            child2.CopyEdges(startEdgePartition2);
            
            child2.CopyVertices(endVertexPartition1);
            child2.CopyEdges(endEdgePartition1);
            
            ShuffleEdges(brokenEdgePartition1);
            ShuffleEdges(brokenEdgePartition2);

            int nEdges = Math.Max(brokenEdgePartition1.Count, brokenEdgePartition2.Count);

            for (int i = 0; i < nEdges; i++)
            {
                Vertex start1;
                Vertex end1;
                Vertex start2;
                Vertex end2;

                GetStartAndEnd(brokenEdgePartition1, i, endVertexPartition1, startVertexPartition1, out start1, out end1);
                GetStartAndEnd(brokenEdgePartition2, i, endVertexPartition2, startVertexPartition2, out start2, out end2);

                if (start1 != null && start2 != null)
                {
                    child2.AddEdge(start1.Copy, end2.Copy);
                    child1.AddEdge(start2.Copy, end1.Copy);
                }
            }
        }

        /// <summary>
        /// Get the start and end vertices of the broken edges
        /// (those which formerly joined the start and end partitions)
        /// If there aren't enough broken edges then randomly choose vertices
        /// from the corresponding partitions.
        /// </summary>
        /// <param name="brokenEdges">The edges formerly joining the two partitions</param>
        /// <param name="index">The index of the broken edge to use</param>
        /// <param name="visitedVertices">The vertices in the visited (end) partition</param>
        /// <param name="unvisitedVertices">The vertices in the unvisited (start) partition</param>
        /// <param name="start">The start vertex</param>
        /// <param name="end">The end vertex</param>
        private static void GetStartAndEnd(List<Edge> brokenEdges, int index, List<Vertex> visitedVertices, List<Vertex> unvisitedVertices, out Vertex start, out Vertex end)
        {
            if (index < brokenEdges.Count)
            {
                Edge edge = brokenEdges[index];

                if (edge.End.Visited)
                {
                    edge = edge.Reversed;
                }

                start = edge.Start;
                end = edge.End;
            }
            else
            {
                if (random.Next(2) == 0)
                {
                    start = visitedVertices[random.Next(visitedVertices.Count)];
                    end = unvisitedVertices[random.Next(unvisitedVertices.Count)];
                }
                else
                {
                    start = null;
                    end = null;
                }
            }
        }

        /// <summary>
        /// Randomise a list of edges.
        /// </summary>
        /// <param name="edges">The list of edges to randomise</param>
        private static void ShuffleEdges(List<Edge> edges)
        {
            for (int i = edges.Count-1; i > 0 ; i--)
            {
                int j = random.Next(i + 1);
                Edge temp = edges[i];
                edges[i] = edges[j];
                edges[j] = temp;
            }
        }

        /// <summary>
        /// Split a RoadNetwork into two paritions by repeatedly breaking one edge along
        /// the shortest path between the start and end until the start and end are
        /// no longer connected.
        /// </summary>
        /// <param name="network">The RoadNetwork to split</param>
        public static void Cut(RoadNetwork network)
        {
            network.SetBroken(false);

            List<Edge> edges;
            while ((edges = network.FindPath()) != null)
            {
                edges[random.Next(edges.Count)].Broken = true;
            }
        }
        
    }
}
