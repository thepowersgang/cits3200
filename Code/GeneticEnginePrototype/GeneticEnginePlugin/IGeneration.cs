using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngineSupport
{
    public interface IGeneration
    {        
        int Count
        {
            get;
        }

        uint MinFitness
        {
            get;
        }

        uint MaxFitness
        {
            get;
        }

        ulong TotalFitness
        {
            get;
        }

        float AverageFitness
        {
            get;
        }

        IndividualWithFitness this[int index]
        {
            get;
        }
        
        void Insert(Object individual, uint fitness);

        IndividualWithFitness Get(int index);

        IndividualWithFitness GetByPartialSum(ulong sum);
                
        int GetIndexByPartialSum(ulong sum);

        IndividualWithFitness Remove(int index);
        
        IndividualWithFitness RemoveByPartialSum(ulong sum);

        void Prepare();
    }
}
