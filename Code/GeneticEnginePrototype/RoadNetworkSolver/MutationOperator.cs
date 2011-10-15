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
        /// Chance from 0 to 1 of an idividual edge to be deleted
        /// </summary>
        double chance_delete_edge = 0.1;
        /// <summary>
        /// Maximum fraction of the total edge population to add (linear spread)
        /// </summary>
        double max_new_edges = 0.1;
        /// <summary>
        /// Fraction of the population to mutate
        /// </summary>
        double fraction_to_mutate = 0.25;


        public MutationOperator(object config)
		{
		}
		
		public void Operate(IGeneration source, ArrayList destination)
		{
            int num_to_mutate = (int)( source.Count * fraction_to_mutate );
            for (int ii = 0; ii < num_to_mutate; ii++)
			{
				destination.Add( Mutate( (RoadNetwork)source.Get(ii).Individual ) );
			}
		}

        private Vertex MoveVertex(RoadNetwork network, Vertex template)
		{

            double x = (double)template.Coordinates.X * (0.5 + random.NextDouble());
            double y = (double)template.Coordinates.Y * (0.5 + random.NextDouble());

			// Clip values
			if (x >= network.Map.Width)
				x = network.Map.Width;
			if (y >= network.Map.Height)
				y = network.Map.Height;

			return network.AddVertex( new Coordinates((int)x, (int)y) );
		}

		private void AddVertex(RoadNetwork network)
		{
            Vertex prev = null;
            Vertex next = null;
			int x = (int)( random.NextDouble() * network.Map.Width );
			int y = (int)( random.NextDouble() * network.Map.Height );

            if (network.VertexCount >= 2)
            {
                // Create new links
                int prev_idx = (int)(random.NextDouble() * (network.VertexCount));
                int next_idx = (int)(random.NextDouble() * (network.VertexCount - 1));
                prev = network.GetVertex(prev_idx);
                // Get the next vertex, and make sure it's not the previous
                if (next_idx == prev_idx)
                    next_idx = (next_idx + 1) % network.VertexCount;
                next = network.GetVertex(next_idx);
            }

            Vertex v = network.AddVertex(new Coordinates(x, y));

			if(network.VertexCount >= 2)
			{
				// Create two edges for it
                network.AddEdge(prev, v);
				network.AddEdge(v, next);
			}
		}

        private void DeleteVertex(RoadNetwork network, int VertexID)
        {
            //Vertex v = network.GetVertex(VertexID);
            // TODO: Need to be able to delete verticies and edges from the network
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
		public RoadNetwork Mutate(RoadNetwork ent)
		{
			RoadNetwork	ret = new RoadNetwork(ent.Map);

			ent.Start.Copy = ret.AddVertex(ent.Start.Coordinates);

			// Copy over verticies
            for (int ii = 1; ii < ent.VertexCount - 1; ii++)
			{
                Vertex v = ent.GetVertex(ii);
                v.Copy = null;
				// Only move the vertex if a random number succeeds
                if (random.NextDouble() < chance_delete_vertex)
                    continue;
                else if (random.NextDouble() < chance_move_vertex)
                    v.Copy = MoveVertex(ret, v);
                else
                    v.Copy = ret.AddVertex(v.Coordinates);
			}

			// Create some new verticies
            int num_new_verts = (int)(ent.VertexCount * max_new_verticies * random.NextDouble());
			for( int ii = 0; ii < num_new_verts; ii ++ )
			{
				AddVertex(ret);
            }

            ent.End.Copy = ret.AddVertex(ent.End.Coordinates);

            // Copy over edges
            for (int ii = 0; ii < ent.EdgeCount; ii++)
            {
                Edge e = ent.GetEdge(ii);
                if (random.NextDouble() < chance_delete_edge)
                    continue;
                else if (e.Start.Copy == null || e.End.Copy == null)
                    continue;
                else
                    ret.AddEdge(e.Start.Copy, e.End.Copy);
            }

			// Create some new edges
			int num_new_edges = (int)(ent.EdgeCount * max_new_edges * random.NextDouble());
			for( int ii = 0; ii < num_new_edges; ii ++ )
			{
				AddEdge(ret);
			}

            RepairMesh(ret);

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
				Vertex v = null;

                // While there's still unchecked nodes in this group
				while(to_check.Count > 0)
				{
                    v = to_check.First.Value;
                    to_check.RemoveFirst();
					v.Visited = true;
					num_unknown --;
						
					for( int i = 0; i < v.EdgeCount; i ++ )
					{
                        Vertex next = v.GetEdge(i).End;
                        if (next.Visited) continue;
                        to_check.AddLast(next);
					}
				}
				
				// If there are still unreached verticies, pick the first and connect
				if( num_unknown > 0 )
				{
                    Vertex end = null;
					// TODO:
                    for (int i = 0; i < network.VertexCount; i++)
                    {
                        if (network.GetVertex(i).Visited) continue;
                        end = network.GetVertex(i);
                        break;
                    }

                    network.AddEdge(v, end);
                    to_check.AddLast(end);
				}
			}
		}
	}
}

