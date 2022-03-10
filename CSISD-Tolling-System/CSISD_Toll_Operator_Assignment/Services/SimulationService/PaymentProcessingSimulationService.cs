using System;
using System.Collections.Generic;

using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Service.SimulationServices;

namespace CSISD_Toll_Operator_Assignment.Service
{
    public class PaymentProcessingSimulationService : ISimulationService<Card>
    {
        private IEnumerable<User> _users;

        public PaymentProcessingSimulationService(IEnumerable<User> users)
        {
            _users = users;
        }

        public List<Card> GenerateAsync()
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
                DateTime expiry = DateTime.Today.AddMonths(-(random.Next(3, 14)));

                Card card = new Card()
                {
                    CardNumber = cardNumbers[index],
                    Cvv = 123,
                    ExpiryDate = expiry,
                    NameOnCard = cardNames[index],
                    OwnerID = user.Id
                };

                cards.Add(card);
                index++;
            }
            return cards;
        }
    }
}
