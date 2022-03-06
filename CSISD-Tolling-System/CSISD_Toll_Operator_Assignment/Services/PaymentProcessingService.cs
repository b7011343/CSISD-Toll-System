using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Service.SimulationServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Toll_Operator_Assignment.Service
{
    public class PaymentProcessingService : ISimulationService<Card>
    {
        private IEnumerable<User> _users;
        private UserManager<User> _userManager;
        public PaymentProcessingService(IEnumerable<User> users, UserManager<User> userManager)
        {
            _users = users;
            _userManager = userManager;
        }

        public List<Card> Generate()
        {
            string[] cardNumbers = new string[]
            {
                "1234 5678 8910 1234",
                "1234 5678 8910 4235",
                "2162 4393 1711 2930",
                "4373 3833 4403 2839",
                "3737 4932 5433 2349"
            };

            string[] cardNames = new string[]
            {
                "Joe Smithurst",
                "Jacob Whitehead",
                "Adam Skitt",
                "Barney Wilks",
                "Zak Edwards"
            };

            List<Card> cards = new List<Card>();
            Random random = new Random();
            int index = 0;

            foreach (User user in _users)
            {
                if (_userManager.IsInRoleAsync(user, Roles.RoadUser).Result == true)
                {
                    DateTime expiry = DateTime.Today.AddMonths(-(random.Next(3, 14)));

                    Card card = new Card()
                    {
                        CardNumber = cardNumbers[index],
                        Cvv        = 123,
                        ExpiryDate = expiry,
                        NameOnCard = cardNames[index],
                        OwnerID    = user.Id
                    };

                    cards.Add(card);
                    index++;
                }
            }
            return cards;
        }
    }
}
