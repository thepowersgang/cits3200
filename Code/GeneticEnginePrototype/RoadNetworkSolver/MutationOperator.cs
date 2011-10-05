using System;
using RoadNetworkSolver;
using GeneticEngineSupport;
using System.Collections;
using System.Collections.Generic;

namespace RoadNetworkSolver
{
	public class MutationOperator: IGeneticOperator
	{
		private Random random = new Random();
		
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
		
		private RoadNetwork Mutate(RoadNetwork ent)
		{
			RoadNetwork	ret = new RoadNetwork(ent);

            ent.Start.Copy = ret.AddVertex(ent.Start.Coordinates);

			for( int ii = 1; ii < ret.VertexCount-1; ii ++ )
			{
				Vertex v = ret.GetVertex(ii);

                
                double x = (double)v.Coordinates.X * (0.5 + random.NextDouble());
                double y = (double)v.Coordinates.Y * (0.5 + random.NextDouble());
                
                // TODO: Clip values

                v.Coordinates = new Coordinates( (int)x, (int)y );
			}

            ent.End.Copy = ret.AddVertex(ent.End.Coordinates);

            return ret;
		}
	}
}

