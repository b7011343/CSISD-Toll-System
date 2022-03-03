﻿using CSISD_Tolling_System.Data.Service.SimulationServices;
using CSISD_Tolling_System.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSISD_Tolling_System.UnitTests
{
    class Test_UserSimulationService
    {
        private Mock<UserManager<User>> MockUserManager(List<User> users, Dictionary<User, string> roles)
        {
            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            mgr.Object.UserValidators.Add(new UserValidator<User>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<User>());

            mgr.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Callback<User, string>((x, y) => users.Add(x));

            mgr.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Callback<User, string>((x, y) => roles.Add(x, y));

            return mgr;
        }

        [Test]
        public void Test_Generate_ContainsAtLeastOneAdminAccount()
        {
            // Arrange
            List<User> users = new List<User>();
            Dictionary<User, string> roles = new Dictionary<User, string>();

            UserManager<User> userManager = MockUserManager(users, roles).Object;
            UserSimulationService generator = new UserSimulationService(userManager);

            // Act
            generator.Generate();

            // Assert
            Assert.That(roles.Count > 0);
            Assert.That(users.Count > 0);
            Assert.That(roles.ContainsValue("admin"));
        }

        [Test]
        public void Test_Generate_ContainsAtLeastOneRoadUser()
        {
            // Arrange
            List<User> users = new List<User>();
            Dictionary<User, string> roles = new Dictionary<User, string>();

            UserManager<User> userManager = MockUserManager(users, roles).Object;
            UserSimulationService generator = new UserSimulationService(userManager);

            // Act
            generator.Generate();

            // Assert
            Assert.That(roles.Count > 0);
            Assert.That(users.Count > 0);
            Assert.That(roles.ContainsValue("road-user"));
        }

        [Test]
        public void Test_Generate_ContainsAtLeastOneTollOperator()
        {
            // Arrange
            List<User> users = new List<User>();
            Dictionary<User, string> roles = new Dictionary<User, string>();

            UserManager<User> userManager = MockUserManager(users, roles).Object;
            UserSimulationService generator = new UserSimulationService(userManager);

            // Act
            generator.Generate();

            // Assert
            Assert.That(roles.Count > 0);
            Assert.That(users.Count > 0);
            Assert.That(roles.ContainsValue("toll-operator"));
        }
    }
}
