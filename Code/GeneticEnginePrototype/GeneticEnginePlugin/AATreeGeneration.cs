using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngineSupport
{
    /// <summary>
    /// Default implementation of IGeneration.
    /// Allows insertion, lookup and deletion in O(lg(n)) time where n is the number of individuals in the generation.
    /// Individuals are sorted in order of decreasing fitness and lookup/deletion can be performed based on either index or partial fitness sum.
    /// </summary>
    public class AATreeGeneration : IGeneration
    {
        /// <summary>
        /// The root node of the tree.
        /// </summary>
        protected Node root;

        /// <summary>
        /// The number of individuals in the generation.
        /// </summary>
        protected int count;

        /// <summary>
        /// The minimum fitness value of all individuals in the generation.
        /// </summary>
        protected uint minFitness;

        /// <summary>
        /// The maximum fitness value of all individuals in the generation.
        /// </summary>
        protected uint maxFitness;

        /// <summary>
        /// The sum of the fitness values of all individuals in the generation.
        /// </summary>
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
        /// Gets the individual at the specified index.
        /// The individual with the highest fitness will be at index 0
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The individual and its fitness value</returns>
        public IndividualWithFitness this[int index]
        {
            get
            {
                return Get(index);
            }
        }
        
        /// <summary>
        /// Initialises an empty AATreeGeneration
        /// </summary>
        public AATreeGeneration()
        {
            root = null;
            count = 0;
            minFitness = uint.MaxValue;
            maxFitness = 0;
            totalFitness = 0;
        }

        /// <summary>
        /// Add an individual to the generation
        /// </summary>
        /// <param name="individual">The individual</param>
        /// <param name="fitness">The individual's fitness</param>
        public void Insert(Object individual, uint fitness)
        {
            count++;
            minFitness = Math.Min(minFitness, fitness);
            maxFitness = Math.Max(maxFitness, fitness);
            totalFitness += fitness;
            Insert(ref root, individual, fitness);
        }

        /// <summary>
        /// Gets the individual at the specified index.
        /// The individual with the highest fitness will be at index 0
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The individual and its fitness value</returns>
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

        /// <summary>
        /// Gets the last individual for which the sum of fitneses of all individuals
        /// up to and including it is less than the given partial sum.
        /// 
        /// This is intended to be used for random sampling with the probability of each individual weighted by its fitness.
        /// Calling this method with A random integer between 0 (inclusive) and TotalFitness (exclusive) will give each individual
        /// a probability of p=(individual fitness)/TotalFitness.
        /// </summary>
        /// <param name="sum">The partial sum</param>
        /// <returns>The individual and its fitness value</returns>
        public IndividualWithFitness GetByPartialSum(ulong sum)
        {
            if (sum >= totalFitness)
            {
                throw new ArgumentOutOfRangeException("sum", sum, "Sum must be less than TotalFitness.");
            }

            return GetByPartialSum(ref root, sum);
        }
       
        /// <summary>
        /// Gets the index of the last individual for which the sum of fitneses of all individuals
        /// up to and including it is less than the given partial sum.
        /// </summary>
        /// <param name="sum">The partial sum</param>
        /// <returns>The index</returns>
        public int GetIndexByPartialSum(ulong sum)
        {
            if (sum >= totalFitness)
            {
                throw new ArgumentOutOfRangeException("sum", sum, "Sum must be less than TotalFitness.");
            }

            return GetIndexByPartialSum(ref root, sum);
        }

        /// <summary>
        /// Remove and return the individual at the specified index.
        /// The individual with the highest fitness will be at index 0
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The individual and its fitness value</returns>
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

        /// <summary>
        /// Gets the last individual for which the sum of fitneses of all individuals
        /// up to and including it is less than the given partial sum.
        /// </summary>
        /// <param name="sum">The partial sum</param>
        /// <returns>The individual and its fitness value</returns>
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

        /// <summary>
        /// Prepare the generation for processing by a genetic operator.
        /// (Do nothing)
        /// </summary>
        public void Prepare() { }
        
        /// <summary>
        /// A node in the AATree
        /// </summary>
        protected class Node
        {
            /// <summary>
            /// The level of the node
            /// </summary>
            public int level;

            /// <summary>
            /// The left child node
            /// </summary>
            public Node leftChild;

            /// <summary>
            /// The right child node
            /// </summary>
            public Node rightChild;
            
            /// <summary>
            /// The individual contained in this node
            /// </summary>
            public object individual;

            /// <summary>
            /// The fitness of the individual
            /// </summary>
            public uint fitness;

            /// <summary>
            /// The sum of all fitness values left of this node plus the fitness of this node
            /// </summary>
            public ulong partialSum;

            /// <summary>
            /// The number of nodes left of this node plus 1 (for this node)
            /// </summary>
            public int partialCount;

            public IndividualWithFitness IndividualWithFitness
            {
                get
                {
                    return new IndividualWithFitness(individual, fitness);
                }
            }

            /// <summary>
            /// Initialise a new Node object
            /// </summary>
            /// <param name="individual">The individual contained in the node</param>
            /// <param name="fitness">The fitness of the individual</param>
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

        /// <summary>
        /// Recursively travel down the tree to insert the individual in a new leaf node in the right order.
        /// Then perform rotations to ensure that the tree remains balanced.
        /// </summary>
        /// <param name="currentNode">The current node in the tree</param>
        /// <param name="individual">The individual to insert</param>
        /// <param name="fitness">The individual's fitness value</param>
        protected static void Insert(ref Node currentNode, object individual, uint fitness)
        {
            if (currentNode == null)
            {
                //If the current node does not exist then insert the individual here.
                currentNode = new Node(individual, fitness);
            }
            else if (fitness > currentNode.fitness)
            {
                //If fitness is greater than the fitness of the current node then go left.
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
                //If fitness is less than or equal to the fitness of the current node then go left.
                if (currentNode.rightChild == null)
                {
                    currentNode.rightChild = new Node(individual, fitness);
                }
                else
                {
                    Insert(ref currentNode.rightChild, individual, fitness);
                }
            }

            //Rebalance the tree
            Skew(ref currentNode);
            Split(ref currentNode);
        }
        
        /// <summary>
        /// Recursively travel down the tree to find the individual identified by a particular index.
        /// </summary>
        /// <param name="currentNode">The current node in the tree</param>
        /// <param name="index">The zero-based index of the element to get within the subtree rooted at currentNode</param>
        /// <returns>The individual and its fitness value</returns>
        protected static IndividualWithFitness Get(ref Node currentNode, int index)
        {
            if (index < currentNode.partialCount)
            {
                //If the index is less than the index of the current node then go left.
                return Get(ref currentNode.leftChild, index);
            }
            else if (index > currentNode.partialCount)
            {
                //If the index id greater than the index of the current node then go right
                return Get(ref currentNode.rightChild, index - currentNode.partialCount);
            }
            else
            {
                //If the index is equal to the index of the current node then return the individual held in the current node.
                return currentNode.IndividualWithFitness;
            }
        }

        /// <summary>
        /// Recursively travel down the tree to find the individual identified by a particular partial sum.
        /// </summary>
        /// <param name="currentNode">The current node in the tree</param>
        /// <param name="sum">The partial sum within the subtree rooted at currentNode</param>
        /// <returns>The individual and its fitness value</returns>
        protected static IndividualWithFitness GetByPartialSum(ref Node currentNode, ulong sum)
        {
            if (sum < currentNode.partialSum - currentNode.fitness)
            {
                //If the partial sum is less than the sum of nodes to the left of the current node then go left
                return GetByPartialSum(ref currentNode.leftChild, sum);
            }
            else if (sum < currentNode.partialSum)
            {
                //If the partial sum is greater than or equal to the sum of nodes to the left of the current node but
                //less than the partial sum of the current node then return the individual held in the current node.
                return currentNode.IndividualWithFitness;
            }
            else
            {
                //If the partial sum is greater than or equal to the partial sum of the current node then
                //go right
                return GetByPartialSum(ref currentNode.rightChild, sum - currentNode.partialSum);
            }
        }

        /// <summary>
        /// Recursively travel down the tree to find the index of the individual identified by a particular partial sum.
        /// </summary>
        /// <param name="currentNode">The current node in the tree</param>
        /// <param name="sum">The partial sum within the subtree rooted at currentNode</param>
        /// <returns>The index</returns>
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

        /// <summary>
        /// Recursively travel down the tree to find and remove the individual identified by a particular index.
        /// Then rebalance the tree.
        /// </summary>
        /// <param name="currentNode">The current node in the tree</param>
        /// <param name="index">The zero-based index of the element to get within the subtree rooted at currentNode</param>
        /// <returns>The individual and its fitness value</returns>
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

        /// <summary>
        /// Recursively travel down the tree to find and remove the individual identified by a particular partial sum.
        /// Then rebalance the tree.
        /// </summary>
        /// <param name="currentNode">The current node in the tree</param>
        /// <param name="sum">The partial sum within the subtree rooted at currentNode</param>
        /// <returns>The individual and its fitness value</returns>
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
