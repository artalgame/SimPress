﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimPressDomainModel.EFDbContext;
using SimPressDomainModel.RepositoryClasses;
using SimPressDomainModel.Entities;

namespace SimPressUnitTests
{
    [TestClass]
    public class DataBaseContextTests
    {
        readonly string connectionString = @"Data Source=ARTAL-ПК\SQLEXPRESS;Initial Catalog=SimPressDataBase;Integrated Security=True;Pooling=False";

        private SimPressEFDBContext GetDBContext()
        {
            return new SimPressEFDBContext(connectionString);
        }

        [TestMethod]
        public void CanConnectToDBThrowDBContextUsingConnectionString()
        {
            SimPressEFDBContext context = GetDBContext();
            Assert.IsTrue(context.Database.Exists());
        }

        [TestMethod]
        public void CanAddNewUserToRepository()
        {
            using (var repo = new DBRepository(GetDBContext()))
            {
                repo.CreateUser(new User
                {
                    CreateDate = DateTime.Now,
                    Email = "alkor1@yandex.ru",
                    Info = "This is good user",
                    IsApproved = true,
                    Login = "alkor",
                    Role = "admin",
                    UserId = Guid.NewGuid(),
                    PasswordHash = "df",
                    PasswordSalt = "dfdf"
                });
                repo.SaveChanges();
            }
            using (var repo = new DBRepository(GetDBContext()))
            {
                try
                {
                    User user = repo.Users.FirstOrDefault<User>(x => x.Login == "alkor");
                    Assert.IsNotNull(user);
                    Assert.IsTrue(user.IsApproved);
                    Assert.AreEqual(user.Email, "alkor1@yandex.ru");
                }
                catch
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void CanUpdateUserFromRepository()
        {
            using (var repo = new DBRepository(GetDBContext()))
            {
                if (repo.Users != null)
                {
                    User updateUser = repo.Users.ToArray()[0];
                    updateUser.Info = "someInfo";
                    updateUser.Login = "newLogin";
                    updateUser.Email = "no email";

                    repo.UpdateUser(updateUser);
                    repo.SaveChanges();

                    User testUser = repo.Users.FirstOrDefault(x => x.UserId == updateUser.UserId);
                    Assert.AreEqual(testUser.Login, updateUser.Login);
                    Assert.AreEqual(testUser.Email, updateUser.Email);
                    Assert.AreEqual(testUser.Info, updateUser.Info);
                }

            }
        }

        [TestMethod]
        public void CanDeleteUserFromRepository()
        {
            using (var repo = new DBRepository(GetDBContext()))
            {
                if (repo.Users != null)
                {
                    User user = repo.Users.ToArray()[0];
                    repo.DeleteUser(user);
                    repo.SaveChanges();

                    User testUser = repo.Users.FirstOrDefault(x => x.UserId == user.UserId);
                    Assert.IsNull(testUser);
                }
                else
                {
                    Assert.Fail("No Users in Repository");
                }
            }
        }
        [TestMethod]
        public void CanCreatePresentationFromRepositoryAndHaveAccessFromUser()
        {
            using (var repo = new DBRepository(GetDBContext()))
            {
                if (repo.Users != null)
                {
                    User user = repo.Users.ToArray()[0];
                    repo.CreatePresentation(new Presentation { CreateDate = DateTime.Now, Description = "Simple Presentation", Name = "Presentation", User = user, PresentationId = Guid.NewGuid() });
                    repo.SaveChanges();

                    Presentation presentation = repo.Presentations.FirstOrDefault(x => x.Name == "Presentation");

                    Assert.IsNotNull(presentation);
                    Assert.AreEqual(presentation.User.UserId, user.UserId);
                    Assert.AreEqual(presentation.Description, "Simple Presentation");
                }
                else
                {
                    Assert.Fail("No Users in Repository");
                }
            }
        }


    }
}
