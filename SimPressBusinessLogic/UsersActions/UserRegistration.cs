using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SimPressDomainModel.Interfaces;
using SimPressBusinessLogic.StaticClasses;

namespace SimPressBusinessLogic
{
    /// <summary>
    /// Some methods for Registration process: all checks and so on.
    /// </summary>
    public class UserRegistration
    {
        public UserRegistration()
        {
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
            return Regex.IsMatch(login, ApplicationSettings.LOGIN_REGEX);
        }

        public bool IsLoginIsGood(string login)
        {
            return IsLoginFree(login) && IsLoginIsCorrected(login);
        }



    }
}
