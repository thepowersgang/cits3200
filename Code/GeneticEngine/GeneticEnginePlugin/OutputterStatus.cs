using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm.Plugin
{
    /// <summary>
    /// Specifies the status of an IOutputter
    /// </summary>
    public enum OutputterStatus
    {
        /// <summary>
        /// The Outputter is ready to output generations
        /// </summary>
        Open,
        
        /// <summary>
        /// The outputter is not ready to output generations but may
        /// be made ready by calling OpenOutput()
        /// </summary>
        Closed,

        /// <summary>
        /// The outputter is not ready to output generations and cannot
        /// be opened by calling OpenOutput()
        /// </summary>
        Locked
    }
}
