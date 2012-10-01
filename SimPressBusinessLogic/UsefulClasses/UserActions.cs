using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimPressDomainModel.Entities;
using SimPressBusinessLogic.StaticClasses;
using SimPressDomainModel.Interfaces;
using System.Text.RegularExpressions;

namespace SimPressBusinessLogic.UsefulClasses
{
    public class UserActions
    {
        public bool IsUserExist(User user)
        {
            return FindUserById(user.UserId) == null;
        }

        public User FindUserById(Guid userId)
        {
            using(var repo = ApplicationSettings.GetRepository())
            {
                return repo.Users.FirstOrDefault(x => x.UserId == userId); 
            }
        }

        public User GetUserByEmail(string email)
        {
            using(var repository = ApplicationSettings.GetRepository())
            {
                return repository.Users.FirstOrDefault(x => x.Email == email);
            }
        }

        public User GetUserByLogin(string login)
        {
            using (var repository = ApplicationSettings.GetRepository())
            {
                return repository.Users.FirstOrDefault(x => x.Login == login);
            }
        }

        public bool IsLoginFree(string login)
        {
            using (IRepository repository = ApplicationSettings.GetRepository())
            {
                var user = repository.Users.FirstOrDefault(x => x.Login == login);
                if (user == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsLoginIsCorrected(string login)
        {
            if (Regex.IsMatch(login, ApplicationSettings.LOGIN_REGEX))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
