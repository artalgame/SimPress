using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimPressBusinessLogic;
using SimPressDomainModel.Entities;
using SimPressBusinessLogic.Enums;

namespace SimPressUnitTests
{
    [TestClass]
    public class UserRegistrationTests
    {
        [TestMethod]
        public void CanRegistrateUserWithErrorLogin()
        {
            var registration = new UserRegistration();
            //login is too short
            PreRegistrateUser user = new PreRegistrateUser
            {
                Login = "bb",
                Password = "lsadfj2",
                ConfirmPassword = "lsadfj2",
                Email = "alkor@yandex.ru",
                Info = "dsfsdfsdf"
            };
            registration.TryRegistrateUser(user);
            Assert.AreEqual(registration.RegistrationState, RegistrationStates.LoginError);
            //login to long
            user = new PreRegistrateUser
            {
                Login = "skfjklsjdflkjskldfjlkasjfkljasdklfjsadklfjklsdjfklsdjfklasdjfkljasdkl",
                Password = "lsadfj2",
                ConfirmPassword = "lsadfj2",
                Email = "alkor@yandex.ru",
                Info = "dsfsdfsdf"
            };
            registration.TryRegistrateUser(user);
            Assert.AreEqual(registration.RegistrationState, RegistrationStates.LoginError);
            //login includes wrong symbols
            user = new PreRegistrateUser
            {
                Login = "sdfsdf dsfsdffs",
                Password = "lsadfj2",
                ConfirmPassword = "lsadfj2",
                Email = "alkor@yandex.ru",
                Info = "dsfsdfsdf"
            };
            registration.TryRegistrateUser(user);
            Assert.AreEqual(registration.RegistrationState, RegistrationStates.LoginError);
        }

        public void CanRegistrateUserWithWrongPassword()
        {
            var registration = new UserRegistration();
            //password is too short
            PreRegistrateUser user = new PreRegistrateUser
            {
                Login = "fafdsaf",
                Password = "lsa",
                ConfirmPassword = "lsadfj2",
                Email = "alkor@yandex.ru",
                Info = "dsfsdfsdf"
            };
            registration.TryRegistrateUser(user);
            Assert.AreEqual(registration.RegistrationState, RegistrationStates.LoginError);
            //password too long
            user = new PreRegistrateUser
            {
                Login = "fkjsaklf",
                Password = "skfjklsjdflkjskldfjlkasjfkljasdklfjsadklfjklsdjfklsdjfklasdjfkljasdkl",
                ConfirmPassword = "lsadfj2",
                Email = "alkor@yandex.ru",
                Info = "dsfsdfsdf"
            };
            registration.TryRegistrateUser(user);
            Assert.AreEqual(registration.RegistrationState, RegistrationStates.LoginError);
            //password doesn't include number
            user = new PreRegistrateUser
            {
                Login = "asdbdfsf",
                Password = "lsadfj2",
                ConfirmPassword = "lsadfj2",
                Email = "alkor@yandex.ru",
                Info = "dsfsdfsdf"
            };
            registration.TryRegistrateUser(user);
            Assert.AreEqual(registration.RegistrationState, RegistrationStates.LoginError);
            //password includes some wrong numbers
            user = new PreRegistrateUser
            {
                Login = "asdbdfsf",
                Password = "lsadfj2 df",
                ConfirmPassword = "lsadfj2",
                Email = "alkor@yandex.ru",
                Info = "dsfsdfsdf"
            };
            registration.TryRegistrateUser(user);
            Assert.AreEqual(registration.RegistrationState, RegistrationStates.LoginError);
        }
    }
}
