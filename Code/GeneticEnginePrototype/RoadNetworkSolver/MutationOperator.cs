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
			RoadNetwork	ret = new RoadNetwork();

            ent.Start.Copy = ret.AddVertex(ent.Start.Coordinates);

			for( int ii = 1; ii < ent.VertexCount-1; ii ++ )
			{
				Vertex v = ret.GetVertex(ii);
				
				int x = v.Coordinates.X * (int) (random.NextDouble() * 1.5);
				int y = v.Coordinates.Y * (int) (random.NextDouble() * 1.5);
                
                // TODO: Clip values

                v.Copy = ret.AddVertex(x, y);
			}

            ent.End.Copy = ret.AddVertex(ent.End.Coordinates);

            ret.CopyEdges(ent);

            return ret;
		}
	}
}

