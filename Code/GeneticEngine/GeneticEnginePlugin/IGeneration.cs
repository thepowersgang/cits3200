using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm.Plugin
{
    /// <summary>
    /// Holds the individuals of a generation with their fitness values
    /// </summary>
    public interface IGeneration
    {        
        /// <summary>
        /// Get the number of individuals in this generation
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Get the minimum fitness value of all individuals in the generation
        /// </summary>
        uint MinFitness
        {
            get;
        }

        /// <summary>
        /// Get the maximum fitness value of all individuals in the generation
        /// </summary>
        uint MaxFitness
        {
            get;
        }

        /// <summary>
        /// Get the average of the fitness values of all individuals in the generation
        /// </summary>
        float AverageFitness
        {
            get;
        }

        /// <summary>
        /// Get the individual at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The individual and its fitness value</returns>
        IndividualWithFitness this[int index]
        {
            get;
        }
        
        /// <summary>
        /// Add an individual to the generation
        /// </summary>
        /// <param name="individual">The individual</param>
        /// <param name="fitness">The individual's fitness</param>
        void Insert(Object individual, uint fitness);

        /// <summary>
        /// Gets the individual at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The individual and its fitness value</returns>
        IndividualWithFitness Get(int index);
                
        /// <summary>
        /// Prepare the generation for processing by plug-ins (outputter/terminator/genetic operator).
        /// </summary>
        void Prepare();
    }
}
