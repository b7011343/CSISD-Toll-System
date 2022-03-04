using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSISD_Toll_Operator_Assignment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSISD_Toll_Operator_Assignment.Service.SimulationServices
{
    public interface ISimulationService<T>
    {
        public List<T> Generate();
    }
}