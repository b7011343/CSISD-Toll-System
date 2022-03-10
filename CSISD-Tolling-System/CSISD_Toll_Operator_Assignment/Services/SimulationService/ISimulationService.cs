using System.Collections.Generic;

namespace CSISD_Toll_Operator_Assignment.Service.SimulationServices
{
    public interface ISimulationService<T>
    {
        public List<T> GenerateAsync();
    }
}