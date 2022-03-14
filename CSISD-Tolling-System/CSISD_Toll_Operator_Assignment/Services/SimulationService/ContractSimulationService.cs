using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Identity;

using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Service.SimulationServices;

namespace CSISD_Toll_Operator_Assignment.Services.SimulationService
{
    public class ContractSimulationService : ISimulationService<Contract>
    {
        private readonly UserManager<User> _userManager;
        public ContractSimulationService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        //this method creates a list of contracts
        public List<Contract> GenerateAsync()
        {
            //create basic info for contracts
            List<Contract> contracts = new List<Contract>();
            List<string> companyNames = new List<string>() { "Belhour Garvel Ltd", "Bjorn Avon Car" };
            List<string> type = new List<string>() { "Business", "Personal" };
            List<double> fee = new List<double>() { 120.00, 50.00 };
            Random random = new Random();
            for (int i = 0; i < companyNames.Count; i++)
            {
                //create random default expiry date
                DateTime expiry = DateTime.Today.AddMonths(-(random.Next(3, 14)));
                //create new contract
                Contract contract = new Contract()
                {
                    Company = companyNames[i],
                    Fee = (decimal)fee[i],
                    Type = type[i],
                    ExpiryDate = expiry,
                    UserId = _userManager.Users.Where(x => x.Email == "test1@test.com").First().Id
                };
                //add contract to list
                contracts.Add(contract);
            }
            return contracts;
        }
    }
}
