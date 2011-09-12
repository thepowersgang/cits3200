using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngineSupport
{
    /// <summary>
    /// Default implementation of IGeneration.
    /// Allows insertion, lookup and deletion in O(lg(n)) time where n is the number of individuals in the population.
    /// Individuals are sorted in order of decreasing fitness and lookup/deletion can be performed based on either index or partial fitness sum.
    /// </summary>
    public class AATreeGeneration : IGeneration
    {
        //The root node of the tree.
        protected Node root;

        //The number of individuals in the generation.
        protected int count;

        //The minimum fitness value of all individuals in the generation.
        protected uint minFitness;

        //The maximum fitness value of all individuals in the generation.
        protected uint maxFitness;

        //The sum of the fitness values of all individuals in the generation.
        protected ulong totalFitness;

        /// <summary>
        /// Gets the number of individuals in the generation.
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
        }

        /// <summary>
        /// Gets the minimum fitness value of all individuals in the generation.
        /// </summary>
        public uint MinFitness
        {
            get
            {
                return minFitness;
            }
        }

        /// <summary>
        /// Gets the maximum fitness value of all individuals in the generation.
        /// </summary>
        public uint MaxFitness
        {
            get
            {
                return maxFitness;
            }
        }

        /// <summary>
        /// Gets the sum of the fitness values of all individuals in the generation.
        /// </summary>
        public ulong TotalFitness
        {
            get
            {
                return totalFitness;
            }
        }

        /// <summary>
        /// Gets the average fitness value of the individuals in the generation.
        /// </summary>
        public float AverageFitness
        {
            get
            {
                return (float)totalFitness / count;
            }
        }

        /// <summary>
        /// Gets the element at the specified index.
        /// The ma
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns></returns>
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
                throw new ArgumentOutOfRangeException("sum", sum, "Sum must be less than TotalFitness.");
            }

            return GetByPartialSum(ref root, sum);
        }
                
        public int GetIndexByPartialSum(ulong sum)
        {
            if (sum >= totalFitness)
            {
                throw new ArgumentOutOfRangeException("sum", sum, "Sum must be less than TotalFitness.");
            }

            return GetIndexByPartialSum(ref root, sum);
        }

        public IndividualWithFitness Remove(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, "Index must not be negative.");
            }

            if (index >= count)
            {
                throw new ArgumentOutOfRangeException("index", index, "Index must be less than Count.");
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
                throw new ArgumentOutOfRangeException("sum", sum, "Sum must be less than TotalFitness.");
            }

            IndividualWithFitness removed = RemoveByPartialSum(ref root, sum);
            totalFitness -= removed.Fitness;
            count--;
            return removed;
        }

        public void Prepare() { }
                
        protected class Node
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
        }

        protected static void Insert(ref Node currentNode, object individual, uint fitness)
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
        
        protected static IndividualWithFitness Get(ref Node currentNode, int index)
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

        protected static IndividualWithFitness GetByPartialSum(ref Node currentNode, ulong sum)
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
                
        protected static int GetIndexByPartialSum(ref Node currentNode, float sum)
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

        protected static IndividualWithFitness Remove(ref Node currentNode, int index)
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

        protected static IndividualWithFitness RemoveByPartialSum(ref Node currentNode, ulong sum)
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

        protected static IndividualWithFitness Remove(ref Node node)
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

        protected static Node GetReplacement(ref Node currentNode)
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

        protected static void FixDeletion(ref Node currentNode)
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

        protected static void Skew(ref Node root)
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

        protected static void Split(ref Node root)
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
