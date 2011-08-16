using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    interface Selector<T>
    {
        PopulationGroups<T> doSelection(Population<T> population);
    }
}
