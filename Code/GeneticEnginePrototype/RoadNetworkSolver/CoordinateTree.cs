using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoadNetworkDefinition;

namespace RoadNetworkSolver
{
    public class CoordinateTree
    {

        private Coordinates point;
        private CoordinateTree leftChild;
        private CoordinateTree rightChild;
        private int axis;

        public CoordinateTree(List<Coordinates> points)
            : this(points, 0)
        {
        }

        public CoordinateTree(List<Coordinates> points, int axis)
        {
            this.axis = axis;

            int childAxis = (axis + 1) % 2;

            points.Sort(DifferenceAlongAxis);

            int count = points.Count;
            int medianIndex = (count - 1) / 2;

            point = points[medianIndex];

            if (medianIndex > 0)
            {
                leftChild = new CoordinateTree(points.GetRange(0, medianIndex), childAxis);
            }
            else
            {
                leftChild = null;
            }

            int rightStart = medianIndex + 1;

            if (rightStart < points.Count)
            {
                rightChild = new CoordinateTree(points.GetRange(rightStart, count - rightStart), childAxis);
            }
            else
            {
                rightChild = null;
            }
        }

        public double minDistance(Coordinates queryPoint)
        {
            return Math.Sqrt(minDistanceSquared(queryPoint));
        }

        public int minDistanceSquared(Coordinates queryPoint)
        {
            int distanceToSplit = DifferenceAlongAxis(point, queryPoint);

            CoordinateTree child0;
            CoordinateTree child1;

            if (distanceToSplit < 0)
            {
                child0 = leftChild;
                child1 = rightChild;
            }
            else
            {
                child0 = rightChild;
                child1 = leftChild;
            }

            int min = queryPoint.GetDistanceSquared(point);

            if (child0 != null)
            {
                min = Math.Min(child0.minDistanceSquared(queryPoint), min);
            }

            if (child1 != null && distanceToSplit * distanceToSplit <= min)
            {
                min = Math.Min(child1.minDistanceSquared(queryPoint), min);
            }

            return min;
        }

        private int DifferenceAlongAxis(Coordinates a, Coordinates b)
        {
            return a[axis] - b[axis];
        }

    }
}
