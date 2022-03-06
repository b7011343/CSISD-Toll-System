using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Models;
using Microsoft.AspNetCore.Identity;

namespace CSISD_Toll_Operator_Assignment.Service.SimulationServices
{
    public class SimulatedUserTemplate
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class UserSimulationService : ISimulationService<User>
    {
        private UserManager<User> _userManager;

        public UserSimulationService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        private class UserTemplate
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        private void CreateUserAsync(UserTemplate template)
        {
            User user = new User
            {
                UserName = template.Email,
                Email = template.Email,
                PreferenceId = 0
            };

            Task<IdentityResult> createUserTask = _userManager.CreateAsync(user, template.Password);
            createUserTask.Wait();

            IdentityResult result = createUserTask.Result;

            if (!result.Succeeded)
            {
                throw new Exception(string.Format("Failed to create user '{0}'", template.Email));
            }

            Task<IdentityResult> addRoleTask = _userManager.AddToRoleAsync(user, template.Role);
            addRoleTask.Wait();

            result = addRoleTask.Result;

            if (!result.Succeeded)
            {
                throw new Exception(string.Format("Failed to add role '{0}' to user '{1}'",
                    template.Role, template.Email));
            }
        }

        private void CreateUsers(IEnumerable<UserTemplate> templates)
        {
            foreach (UserTemplate user in templates)
                CreateUserAsync(user);
        }

        private void CreateUserSync(UserTemplate template)
        {
            CreateUserAsync(template);
        }

        public List<User> Generate()
        {
            // Create some normal test road user accounts

            UserTemplate[] users = new UserTemplate[]
            {
                new UserTemplate { Email = "test1@test.com", Password = "Test1234!", Role = Roles.RoadUser },
                new UserTemplate { Email = "test2@test.com", Password = "Test1234!", Role = Roles.RoadUser },
                new UserTemplate { Email = "test3@test.com", Password = "Test1234!", Role = Roles.RoadUser },
                new UserTemplate { Email = "test4@test.com", Password = "Test1234!", Role = Roles.RoadUser },
                new UserTemplate { Email = "test5@test.com", Password = "Test1234!", Role = Roles.RoadUser },
            };

            CreateUsers(users);

            // Create a administrator account
            CreateUserSync(new UserTemplate { Email = "admin@admin.com", Password = "Test123!", Role = Roles.Administrator });

            // Create a toll operator account
            CreateUserSync(new UserTemplate { Email = "tolls@tolls.com", Password = "Test123!", Role = Roles.TollOperator });

            return new List<User>();
        }
    }
}