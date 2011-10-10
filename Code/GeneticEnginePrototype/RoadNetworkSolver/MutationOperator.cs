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
        static const double chance_move_vertex = 0.1;
		
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

        private void AddVertex(RoadNetwork network, int Position)
        {
//            int x = (int)( random.NextDouble() * network.Map.Width );
//            int y = (int)( random.NextDouble() * network.Map.Height );
//            Vertex v = new Vertex(network, new Coordinates(x, y));
        }

        /// <summary>
        /// Add a random endge to the network
        /// </summary>
        /// <param name="network"></param>
        private void AddEdge(RoadNetwork network)
        {
            int start = (int)(random.NextDouble() * network.VertexCount);
            int end = (int)(random.NextDouble() * network.VertexCount);
            if(end >= start) end ++;
            network.AddEdge(network.GetVertex(start), network.GetVertex(end));
        }

		private RoadNetwork Mutate(RoadNetwork ent)
		{
			RoadNetwork	ret = new RoadNetwork(ent);

            ent.Start.Copy = ret.AddVertex(ent.Start.Coordinates);

            // TODO: Select random verticies to modify instead
			for( int ii = 1; ii < ret.VertexCount-1; ii ++ )
			{
                // Only move the vertex if a random number succeeds
                if (random.NextDouble() > chance_move_vertex) continue;
                MoveVertex(ret, ii);
			}

            ent.End.Copy = ret.AddVertex(ent.End.Coordinates);

            return ret;
		}
	}
}

