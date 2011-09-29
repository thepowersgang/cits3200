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
				//destination.add( Mutate(source.Get(ii)) );
			}
		}
		
		private RoadNetwork Mutate(RoadNetwork ent)
		{
			RoadNetwork	ret = new RoadNetwork(ent);
			for( int ii = 1; ii < ent.VertexCount; ii ++ )
			{
				Vertex v = ret.GetVertex(ii);
				// TODO: Clip values
				v.Coordinates.X *= (int) (random.NextDouble() * 1.5);
				v.Coordinates.Y *= (int) (random.NextDouble() * 1.5);
			}
            return ret;
		}
	}
}

