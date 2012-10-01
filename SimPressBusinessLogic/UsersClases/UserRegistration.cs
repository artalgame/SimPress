using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SimPressBusinessLogic.Enums;
using SimPressDomainModel.Interfaces;
using SimPressBusinessLogic.StaticClasses;
using SimPressDomainModel.Entities;
using System.Security.Cryptography;
using SimPressBusinessLogic.UsefulClasses;

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
        public RegistrationStates RegistrationState { get; set; }
        public string DataBaseErrorString { get; set; }

        public UserRegistration()
        {
        }

        private bool IsLoginFree(string login)
        {
            if (new UserActions().IsLoginFree(login) == null)
            {
                return true;
            }
            else
            {
                LoginState = LoginStates.Occupied;
                return false;
            }
        }

        private bool IsLoginIsCorrected(string login)
        {
            if (new UserActions().IsLoginIsCorrected(login))
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
            if (IsLoginFree(login) && IsLoginIsCorrected(login))
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
            if (Regex.IsMatch(password, ApplicationSettings.PASSWORD_REGEX))
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
            if (IsPasswordCorrected(password))
            {
                PasswordState = PasswordStates.Ok;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsConfirmPasswordMatched(string password, string confirmPassword)
        {
            if (PasswordActions.PasswordsAreEqual(password, confirmPassword))
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
            using (IRepository repository = ApplicationSettings.GetRepository())
            {
                string existEmail = repository.Users.Select(x => x.Email).FirstOrDefault(x => x == email);
                if (existEmail == null)
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
        public bool IsEmailCorrected(string email)
        {
            if (Regex.IsMatch(email, ApplicationSettings.EMAIL_REGEX))
            {
                return true;
            }
            else
            {
                EmailState = EmailStates.WrongFormat;
                return false;
            }
        }

        public bool IsEmailRight(string email)
        {
            if (IsEmailCorrected(email) && (IsEmailFree(email)))
            {
                EmailState = EmailStates.Ok;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string CorrectInfo(string info)
        {
            if (info == null)
            {
                return "";
            }
            else
                if (info.Length > ApplicationSettings.USER_INFO_MAX_LENGTH)
                {
                    return new string(info.Take(ApplicationSettings.USER_INFO_MAX_LENGTH).ToArray());
                }
                else
                {
                    return info;
                }
        }



        public void TryRegistrateUser(PreRegistrateUser user)
        {
            try
            {
                //check user parameters
                if (!IsLoginRight(user.Login))
                {
                    RegistrationState = RegistrationStates.LoginError;
                    return;
                }
                if (!IsPasswordRight(user.Password))
                {
                    RegistrationState = RegistrationStates.PasswordError;
                    return;
                }
                if (!IsConfirmPasswordMatched(user.Password, user.ConfirmPassword))
                {
                    RegistrationState = RegistrationStates.ConfirmPasswordError;
                    return;
                }
                if (!IsEmailRight(user.Email))
                {
                    RegistrationState = RegistrationStates.EmailError;
                    return;
                }
                RegistrateUser(user);
                RegistrationState = RegistrationStates.Ok;
            }
            catch (Exception ex)
            {
                DataBaseErrorString = ex.Message;
                RegistrationState = RegistrationStates.DataBaseError;
            }
        }

        private void RegistrateUser(PreRegistrateUser user)
        {
            string salt = PasswordActions.GenerateSalt();
            //create user
            var regUser = new User
            {
                CreateDate = DateTime.Now,
                Email = user.Email,
                Info = CorrectInfo(user.Info),
                IsApproved = true,
                Login = user.Login,
                PasswordHash = PasswordActions.GetHashForPassword(user.Password, salt),
                PasswordSalt = salt,
                Role = Roles.SimpleUser.ToString(),
                UserId = Guid.NewGuid()
            };
            using (var repo = ApplicationSettings.GetRepository())
            {
                repo.CreateUser(regUser);
                repo.SaveChanges();
            }
        }
    }
}
