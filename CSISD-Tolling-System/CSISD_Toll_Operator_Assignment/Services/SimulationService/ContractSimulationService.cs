using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Service.SimulationServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Toll_Operator_Assignment.Services.SimulationService
{
    public class ContractSimulationService : ISimulationService<Contract>
    {
        private readonly UserManager<User> _userManager;
        public ContractSimulationService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public List<Contract> Generate()
        {
            List<Contract> contracts = new List<Contract>();
            List<string> companyNames = new List<string>() { "Belhour Garvel Ltd", "Bjorn Avon Car" };
            List<string> type = new List<string>() { "Business", "Personal" };
            List<double> fee = new List<double>() { 120.00, 50.00 };
            Random random = new Random();
            for (int i = 0; i < companyNames.Count; i++)
            {
                DateTime expiry = DateTime.Today.AddMonths(-(random.Next(3, 14)));
                Contract card = new Contract()
                {
                    Company = companyNames[i],
                    Fee = (decimal)fee[i],
                    Type = type[i],
                    ExpiryDate = expiry,
                    UserId = _userManager.Users.First().Id
                };
                contracts.Add(card);
            }
            return contracts;
        }
    }
}
