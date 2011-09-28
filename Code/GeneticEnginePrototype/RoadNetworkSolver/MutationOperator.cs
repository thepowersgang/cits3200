using System;
using RoadNetworkSolver;

namespace RoadNetworkSolver
{
	public class MutationOperator: IGeneticOperator<MutationOperator>
	{
		public MutationOperator()
		{
		}
		
		public void Operate(IGeneration Population, ArrayList List)
		{
			for(RoadNetwork ent in Population)
			{
				Mutate(ent);
				List.add(ent);
			}
		}
		
		private void Mutate(RoadNetwork ent)
		{
			for( int ii = 1; ii < ent.VertexCount; ii ++ )
			{
				Vertex v = net.GetVertex(ii);
				v.Coordinates.x += Random(-5, 5);
				// TODO: Clip values
				v.Coordinates.y += Random(-5, 5);
			}
		}
	}
}

