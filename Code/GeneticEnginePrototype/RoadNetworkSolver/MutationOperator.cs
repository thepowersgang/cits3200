using System;
using RoadNetworkSolver;
using GeneticAlgorithm.Plugin;
using System.Collections;
using System.Collections.Generic;

namespace RoadNetworkSolver
{
	public class MutationOperator: IGeneticOperator
	{
        private Random random = new Random();
        /// <summary>
        /// Chance out of 1 for an individual vertex to altered
        /// </summary>
        double chance_move_vertex = 0.1;
        /// <summary>
        /// Chance out of 1 for an individual vertex to be deleted
        /// </summary>
        double chance_delete_vertex = 0.01;
        /// <summary>
        /// Maximum fraction of the total vertex population to add (linear spread)
        /// </summary>
        double max_new_verticies = 0.1;
        /// <summary>
        /// Maximum fraction of the total edge population to add (linear spread)
        /// </summary>
        double max_new_edges = 0.1;
        
		
		public MutationOperator()
		{
		}
		
		public void Operate(IGeneration source, ArrayList destination)
		{
			for(int ii = 0; ii < source.Count; ii ++)
			{
				destination.Add( Mutate( (RoadNetwork)source.Get(ii).Individual ) );
			}
		}

		private void MoveVertex(RoadNetwork network, int Index)
		{
			Vertex v = network.GetVertex(Index);

			double x = (double)v.Coordinates.X * (0.5 + random.NextDouble());
			double y = (double)v.Coordinates.Y * (0.5 + random.NextDouble());

			// Clip values
			if (x >= network.Map.Width)
				x = network.Map.Width;
			if (y >= network.Map.Height)
				y = network.Map.Height;

			v.Coordinates = new Coordinates((int)x, (int)y);
		}

		private void AddVertex(RoadNetwork network)
		{
			int x = (int)( random.NextDouble() * network.Map.Width );
			int y = (int)( random.NextDouble() * network.Map.Height );
			Vertex v = network.AddVertex( new Coordinates(x, y) );

			if(network.VertexCount >= 2)
			{			
				// Create new links
				int prev_idx = (int)(random.NextDouble() * (network.VertexCount-1));
				int next_idx = (int)(random.NextDouble() * (network.VertexCount-2));
				// Get previous vertex and ensure it's not the newly created one
				Vertex prev = network.GetVertex(prev_idx);
				if(prev == v)
				{
					prev_idx = (prev_idx + 1) % network.VertexCount;
					prev = network.GetVertex(prev_idx);
				}
				// Get the next vertex, and make sure it's not either the previous or new
				if(next_idx == prev_idx)
					next_idx = (next_idx + 1) % network.VertexCount;
				Vertex next = network.GetVertex(next_idx);
				if(next == v)
				{
					next_idx = (next_idx + 1) % network.VertexCount;
					next = network.GetVertex(next_idx);
				}
				
				// Create two edges for it
                network.AddEdge(prev, v);
				network.AddEdge(v, next);
			}
		}

        private void DeleteVertex(RoadNetwork network, int VertexID)
        {
            Vertex v = network.GetVertex(VertexID);
        }

		/// <summary>
		/// Add a random edge to the network
		/// </summary>
		/// <param name="network"></param>
		/// <returns>
		/// False if the edge was not created (due to a collision), true if it was
		/// </returns>
		private bool AddEdge(RoadNetwork network)
		{
			// Get two verticies (-1 creates a smaller pool)
			int start_idx = (int)(random.NextDouble() * network.VertexCount);
			int end_idx = (int)(random.NextDouble() * (network.VertexCount - 1));
			// Handle collision
			if(end_idx >= start_idx) end_idx ++;
			Vertex start = network.GetVertex(start_idx);
            Vertex end = network.GetVertex(end_idx);
			
			// Make sure this isn't making a duplicate (or reverse duplicate)
			for( int i = 0; i < network.EdgeCount; i ++ )
			{
				Edge edge = network.GetEdge(i);
				if( edge.Start == start && edge.End == end )
					return false;
				if( edge.End == start && edge.Start == end )
					return false;
			}
			network.AddEdge(start, end);
			return true;
		}

		/// <summary>
		/// Perform mutations on the network
		/// </summary>
		/// <param name="ent">Network to mutate</param>
		private RoadNetwork Mutate(RoadNetwork ent)
		{
			RoadNetwork	ret = new RoadNetwork(ent);

			ent.Start.Copy = ret.AddVertex(ent.Start.Coordinates);

			// Iterate through all verticies and randomly change some
			for( int ii = 1; ii < ret.VertexCount-1; ii ++ )
			{
				// Only move the vertex if a random number succeeds
				if(random.NextDouble() < chance_move_vertex)
					MoveVertex(ret, ii);
				if(random.NextDouble() < chance_delete_vertex)
					DeleteVertex(ret, ii);
			}

			// Create some new verticies
			int num_new_verts = (int)(ret.VertexCount * max_new_verticies * random.NextDouble());
			for( int ii = 0; ii < num_new_verts; ii ++ )
			{
				AddVertex(ret);
			}

			// Create some new edges
			int num_new_edges = (int)(ret.EdgeCount * max_new_edges * random.NextDouble());
			for( int ii = 0; ii < num_new_edges; ii ++ )
			{
				AddEdge(ret);
			}

			ent.End.Copy = ret.AddVertex(ent.End.Coordinates);

			return ret;
		}

		
		private void RepairMesh(RoadNetwork network)
		{
			network.ClearVisited();
			int num_unknown = network.VertexCount;
			LinkedList<Vertex> to_check = new LinkedList<Vertex>();
			to_check.AddLast( network.Start );
			
			while( num_unknown > 0 )
			{
				while(to_check.Count > 0)
				{
					Vertex v = to_check.First.Value;
                    to_check.RemoveFirst();
					v.Visited = true;
					num_unknown --;
						
					for( int i = 0; i < v.EdgeCount; i ++ )
					{
						if( v.Visited )	continue;
                        to_check.AddLast(v);
					}
				}
				
				// If there are still unreached verticies, pick one and connect
				if( num_unknown > 0 )
				{
					// TODO:
				}
			}
		}
	}
}

