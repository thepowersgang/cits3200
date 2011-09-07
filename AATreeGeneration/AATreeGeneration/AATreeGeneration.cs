using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generations
{
    /// <summary>
    /// Default implementation of IGeneration.
    /// Allows insertion, lookup and deletion in O(lg(n)) time where n is the number of individuals in the population.
    /// Individuals are sorted in order of decreasing fitness and lookup/deletion can be performed based on either index or partial fitness sum.
    /// </summary>
    public class AATreeGeneration : IGeneration
    {
        private Node root;
        private int count;
        private uint minFitness;
        private uint maxFitness;
        private ulong totalFitness;

        /// <summary>
        /// The number of individuals in the generation.
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
        }

        public uint MinFitness
        {
            get
            {
                return minFitness;
            }
        }

        public uint MaxFitness
        {
            get
            {
                return maxFitness;
            }
        }

        public ulong TotalFitness
        {
            get
            {
                return totalFitness;
            }
        }

        public float AverageFitness
        {
            get
            {
                return (float)totalFitness / count;
            }
        }

        public IndividualWithFitness this[int index]
        {
            get
            {
                return Get(index);
            }
        }
                
        public AATreeGeneration()
        {
            root = null;
            count = 0;
            minFitness = uint.MaxValue;
            maxFitness = 0;
            totalFitness = 0;
        }

        public void Insert(Object individual, uint fitness)
        {
            count++;
            minFitness = Math.Min(minFitness, fitness);
            maxFitness = Math.Max(maxFitness, fitness);
            totalFitness += fitness;
            Insert(ref root, individual, fitness);
        }

        public IndividualWithFitness Get(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, "Index must not be negative.");
            }

            if (index >= count)
            {
                throw new ArgumentOutOfRangeException("index", index, "Index must be less than the size of the generation.");
            }

            return Get(ref root, index+1);
        }

        public IndividualWithFitness GetByPartialSum(ulong sum)
        {
            if (sum >= totalFitness)
            {
                throw new ArgumentOutOfRangeException("sum", sum, "Must be less than TotalFitness.");
            }

            return GetByPartialSum(ref root, sum);
        }
                
        public int GetIndexByPartialSum(ulong sum)
        {
            if (sum >= totalFitness)
            {
                throw new ArgumentOutOfRangeException("sum", sum, "Must be less than TotalFitness.");
            }

            return GetIndexByPartialSum(ref root, sum);
        }

        public IndividualWithFitness Remove(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, "Must not be negative.");
            }

            if (index >= count)
            {
                throw new ArgumentOutOfRangeException("index", index, "Must be less than Count.");
            }

            IndividualWithFitness removed = Remove(ref root, index + 1);
            totalFitness -= removed.Fitness;
            count--;
            return removed;
        }
        
        public IndividualWithFitness RemoveByPartialSum(ulong sum)
        {
            if (sum >= totalFitness)
            {
                throw new ArgumentOutOfRangeException("sum", sum, "Must be less than TotalFitness.");
            }

            IndividualWithFitness removed = RemoveByPartialSum(ref root, sum);
            totalFitness -= removed.Fitness;
            count--;
            return removed;
        }

        public void Prepare() { }

        public void Print()
        {
            root.Print("");
        }

        public void Test()
        {
            int lCount, rCount;
            ulong lSum, rSum;
            root.Test(out lCount, out rCount, out lSum, out rSum);

            if (lCount + rCount != count)
            {
                Console.WriteLine("Bad Count");
            }

            if (lSum + rSum != totalFitness)
            {
                Console.WriteLine("Bad Sum");
            }
            
            for (int i = 1; i < count; i++)
            {
                if (Get(i).Fitness > Get(i - 1).Fitness)
                {
                    Console.WriteLine("Bad Order");
                }
            }
        }

        private class Node
        {
            public int level;
            public Node leftChild;
            public Node rightChild;
            public object individual;
            public uint fitness;
            public ulong partialSum;
            public int partialCount;

            public IndividualWithFitness IndividualWithFitness
            {
                get
                {
                    return new IndividualWithFitness(individual, fitness);
                }
            }

            public Node(object individual, uint fitness)
            {
                level = 1;
                leftChild = null;
                rightChild = null;
                this.individual = individual;
                this.fitness = fitness;
                partialSum = fitness;
                partialCount = 1;
            }

            public void Print(string indent)
            {
                Console.Write("-(" + fitness + ", " + level + ", " + partialSum + ", " + partialCount + ")");

                if (leftChild != null)
                {
                    leftChild.Print(indent + "|             ");
                }

                if (rightChild != null)
                {
                    Console.WriteLine();
                    Console.Write("             " + indent + "\\");

                    rightChild.Print(indent + "             ");
                }
            }

            public void Test(out int lCount, out int rCount, out ulong lSum, out ulong rSum)
            {
                int tCount;
                ulong tSum;

                if (leftChild == null)
                {
                    lCount = 1;
                    lSum = fitness;

                    if (level != 1)
                    {
                        Console.WriteLine("Bad Left Child");
                    }
                }
                else
                {
                    leftChild.Test(out lCount, out tCount, out lSum, out tSum);
                    lCount += tCount + 1;
                    lSum += tSum + fitness;

                    if (level != leftChild.level + 1)
                    {
                        Console.WriteLine("Bad Left Level");
                    }
                }

                if (rightChild == null)
                {
                    rCount = 0;
                    rSum = 0;

                    if (level != 1)
                    {
                        Console.WriteLine("Bad Right Child");
                    }
                }
                else
                {
                    rightChild.Test(out tCount, out rCount, out tSum, out rSum);
                    rCount += tCount;
                    rSum += tSum;

                    if (level != rightChild.level && level != rightChild.level + 1 )
                    {
                        Console.WriteLine("Bad Right Level");
                    }

                    if (rightChild.rightChild != null && level == rightChild.rightChild.level)
                    {
                        Console.WriteLine("Double Horizontal Link");
                    }
                }

                if (lCount != partialCount)
                {
                    Console.WriteLine("Left Count Wrong");
                }

                if (lSum != partialSum)
                {
                    Console.WriteLine("Left Fitness Sum Wrong");
                }
            }
        }

        private static void Insert(ref Node currentNode, object individual, uint fitness)
        {
            if (currentNode == null)
            {
                currentNode = new Node(individual, fitness);
            }
            else if (fitness > currentNode.fitness)
            {
                currentNode.partialSum += fitness;
                currentNode.partialCount++;

                if (currentNode.leftChild == null)
                {
                    currentNode.leftChild = new Node(individual, fitness);
                }
                else
                {
                    Insert(ref currentNode.leftChild, individual, fitness);
                }
            }
            else
            {
                if (currentNode.rightChild == null)
                {
                    currentNode.rightChild = new Node(individual, fitness);
                }
                else
                {
                    Insert(ref currentNode.rightChild, individual, fitness);
                }
            }

            Skew(ref currentNode);
            Split(ref currentNode);
        }
        
        private static IndividualWithFitness Get(ref Node currentNode, int index)
        {
            if (index < currentNode.partialCount)
            {
                return Get(ref currentNode.leftChild, index);
            }
            else if (index > currentNode.partialCount)
            {
                return Get(ref currentNode.rightChild, index - currentNode.partialCount);
            }
            else
            {
                return currentNode.IndividualWithFitness;
            }
        }

        private static IndividualWithFitness GetByPartialSum(ref Node currentNode, ulong sum)
        {
            if (sum < currentNode.partialSum - currentNode.fitness)
            {
                return GetByPartialSum(ref currentNode.leftChild, sum);
            }
            else if (sum < currentNode.partialSum)
            {
                return currentNode.IndividualWithFitness;
            }
            else
            {
                return GetByPartialSum(ref currentNode.rightChild, sum - currentNode.partialSum);
            }
        }
                
        private static int GetIndexByPartialSum(ref Node currentNode, float sum)
        {
            if (sum < currentNode.partialSum - currentNode.fitness)
            {
                if (currentNode.leftChild == null)
                {
                    return currentNode.partialCount;
                }
                else
                {
                    return GetIndexByPartialSum(ref currentNode.leftChild, sum);
                }
            }
            else if (sum < currentNode.partialSum)
            {
                return currentNode.partialCount;
            }
            else
            {
                if (currentNode.rightChild == null)
                {
                    return currentNode.partialCount;
                }
                else
                {
                    return currentNode.partialCount + GetIndexByPartialSum(ref currentNode.rightChild, sum - currentNode.partialSum);
                }
            }
        }

        private static IndividualWithFitness Remove(ref Node currentNode, int index)
        {
            IndividualWithFitness removed;

            if (index < currentNode.partialCount)
            {
                removed = Remove(ref currentNode.leftChild, index);

                currentNode.partialSum -= removed.Fitness;
                currentNode.partialCount--;
            }
            else if (index > currentNode.partialCount)
            {
                removed = Remove(ref currentNode.rightChild, index - currentNode.partialCount);
            }
            else
            {
                removed = Remove(ref currentNode);
            }

            FixDeletion(ref currentNode);
            return removed;
        }

        private static IndividualWithFitness RemoveByPartialSum(ref Node currentNode, ulong sum)
        {
            IndividualWithFitness removed;

            if (sum < currentNode.partialSum - currentNode.fitness)
            {
                removed = RemoveByPartialSum(ref currentNode.leftChild, sum);

                currentNode.partialSum -= removed.Fitness;
                currentNode.partialCount--;                
            }
            else if (sum < currentNode.partialSum)
            {
                removed = Remove(ref currentNode);
            }
            else
            {
                removed = RemoveByPartialSum(ref currentNode.rightChild, sum - currentNode.partialSum);
            }

            FixDeletion(ref currentNode);
            return removed;
        }

        private static IndividualWithFitness Remove(ref Node node)
        {
            IndividualWithFitness removed = node.IndividualWithFitness;

            if (node.rightChild == null)
            {
                node = null;
            }
            else
            {
                Node replacement = GetReplacement(ref node.rightChild);

                node.individual = replacement.individual;
                node.fitness = replacement.fitness;
                node.partialSum -= removed.Fitness - replacement.fitness;
            }

            return removed;
        }

        private static Node GetReplacement(ref Node currentNode)
        {
            Node replacement;

            if (currentNode.leftChild == null)
            {
                replacement = currentNode;
                currentNode = currentNode.rightChild;
            }
            else
            {
                replacement = GetReplacement(ref currentNode.leftChild);
                currentNode.partialSum -= replacement.fitness;
                currentNode.partialCount--;
            }

            FixDeletion(ref currentNode);
            return replacement;
        }

        private static void FixDeletion(ref Node currentNode)
        {
            if (currentNode != null)
            {
                int levelShouldBe;

                if (currentNode.leftChild == null || currentNode.rightChild == null)
                {
                    levelShouldBe = 1;
                }
                else
                {
                    levelShouldBe = Math.Min(currentNode.leftChild.level, currentNode.rightChild.level) + 1;
                }

                if (levelShouldBe < currentNode.level)
                {
                    currentNode.level = levelShouldBe;
                    if (currentNode.rightChild != null && levelShouldBe < currentNode.rightChild.level)
                    {
                        currentNode.rightChild.level = levelShouldBe;
                    }
                }

                Skew(ref currentNode);

                if (currentNode.rightChild != null)
                {
                    Skew(ref currentNode.rightChild);

                    if (currentNode.rightChild.rightChild != null)
                    {
                        Skew(ref currentNode.rightChild.rightChild);
                    }
                }

                Split(ref currentNode);

                if (currentNode.rightChild != null)
                {
                    Split(ref currentNode.rightChild);
                }
            }
        }

        private static void Skew(ref Node root)
        {
            Node left = root.leftChild;

            if (left != null && root.level == left.level)
            {
                root.leftChild = left.rightChild;
                left.rightChild = root;

                root.partialSum -= left.partialSum;
                root.partialCount -= left.partialCount;

                root = left;
            }
        }

        private static void Split(ref Node root)
        {
            Node right = root.rightChild;

            if (right != null && right.rightChild != null && root.level == right.rightChild.level)
            {
                root.rightChild = right.leftChild;
                right.leftChild = root;

                right.partialSum += root.partialSum;
                right.partialCount += root.partialCount;

                root = right;
                root.level++;
            }
        }
    }
}
