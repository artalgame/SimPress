using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SimPressBusinessLogic.Enums;
using SimPressDomainModel.Interfaces;
using SimPressBusinessLogic.StaticClasses;

namespace SimPressBusinessLogic
{
    /// <summary>
    /// Some methods for Registration process: all checks and so on.
    /// </summary>
    public class UserRegistration
    {
        public LoginStates LoginState { get; set; }
        public PasswordStates PasswordState { get; set; }
        public PasswordStates ConfirmPasswordState { get; set; }
        public EmailStates EmailState { get; set; }
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
                    LoginState = LoginStates.Occupied;
                    return false;
                }
            }
        }

        public bool IsLoginIsCorrected(string login)
        {
            if(Regex.IsMatch(login, ApplicationSettings.LOGIN_REGEX))
            {
                return true;
            }
            else
            {
                LoginState = LoginStates.WrongFormat;
                return false;
            }
        }

        public bool IsLoginRight(string login)
        {
            if(IsLoginFree(login) && IsLoginIsCorrected(login))
            {
                LoginState = LoginStates.Ok;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPasswordCorrected(string password)
        {
            if(Regex.IsMatch(password, ApplicationSettings.PASSWORD_REGEX))

            {
                return true;
            }
            else
            {
                PasswordState = PasswordStates.WrongFormat;
                return false;
            }
        }

        public bool IsPasswordRight(string password)
        {
            if(IsPasswordCorrected(password))
            {
                PasswordState = PasswordStates.Ok;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsConfirmPasswordMatched(string password,string confirmPassword)
        {
            if(password == confirmPassword)
            {
                return true;
            }
            else
            {
                ConfirmPasswordState = PasswordStates.NotMatch;
                return false;
            }
        }

        public bool IsEmailFree(string email)
        {
            using(IRepository repository = ApplicationSettings.GetRepository())
            {
                string existEmail = repository.Users.Select(x=>x.Email).FirstOrDefault(x=>x==email);
                if(existEmail == null)
                {
                    return true;
                }
                else
                {
                    EmailState = EmailStates.Occupied;
                    return false;
                }
            }
        }

    }
}
