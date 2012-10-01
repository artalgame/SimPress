using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SimPressBusinessLogic.Enums;
using SimPressBusinessLogic.StaticClasses;
using SimPressDomainModel.Entities;
using SimPressBusinessLogic.UsefulClasses;

namespace SimPressBusinessLogic.UsersActions
{
    public class UserAuthentification
    {
        public TypeOfAuthentificatedLogin LoginType { get; set; }
        public AuthentificationStates AuthentificationState { get; set; }

        public TypeOfAuthentificatedLogin RecognizeLoginType(string login)
        {
            if(Regex.IsMatch(login,ApplicationSettings.LOGIN_REGEX))
            {
                return TypeOfAuthentificatedLogin.Login;
            }
            else
            {
                if(Regex.IsMatch(login,ApplicationSettings.EMAIL_REGEX))
                {
                    return TypeOfAuthentificatedLogin.Email;
                }
                else
                {
                    return TypeOfAuthentificatedLogin.Indefinite;
                }
            }
        }

        public User TryToAuthentificateUser(string login, string password)
        {
             LoginType = RecognizeLoginType(login);
            switch (LoginType)
            {
                case TypeOfAuthentificatedLogin.Login:
                    return GetUserByLogin(login, password);
                case TypeOfAuthentificatedLogin.Email:
                    return GetUserByEmail(login, password);
                default:
                    AuthentificationState = AuthentificationStates.WrongLogin;
                    return null;
            }
        }

        private User GetUserByEmail(string email, string password)
        {
                return GetUser(new UserActions().GetUserByEmail(email), password);
        }

        private User GetUserByLogin(string login, string password)
        {
                return GetUser(new UserActions().GetUserByLogin(login), password);
        }

        private User GetUser(User findedUser,string password)
        {
            if (findedUser == null)
            {
                AuthentificationState = AuthentificationStates.LoginNotExist;
                return null;
            }
            if (!PasswordActions.CheckPassword(password, findedUser))
            {
                AuthentificationState = AuthentificationStates.WrongPassword;
                return null;
            }
            AuthentificationState = AuthentificationStates.Ok;
            return findedUser;
        }
    }
}
