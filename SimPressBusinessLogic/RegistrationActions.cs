using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SimPressDomainModel.EFDbContext;
using SimPressDomainModel.Interfaces;
using SimPressDomainModel.RepositoryClasses;

namespace SimPressBusinessLogic
{
    /// <summary>
    /// Some methods for Registration process: all checks and so on.
    /// </summary>
    public static class RegistrationActions
    {
        private static IRepository repository;
        private static readonly string loginRegex = @"[A-Za-z][A-Za-z0-9_.]{3,31}";
        static RegistrationActions()
        {
            //Rebuild using ninject
            repository = new DBRepository(new SimPressEFDBContext(@"Data Source=ARTAL-ПК\SQLEXPRESS;Initial Catalog=SimPressDataBase;Integrated Security=True;Pooling=False"));
        }
        public static bool IsLoginFree(string login)
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
        
        public  static bool IsLoginIsCorrected(string login)
        {
            return Regex.IsMatch(login, loginRegex);
        }

        public static bool IsLoginIsGood(string login)
        {
            return IsLoginFree(login) && IsLoginIsCorrected(login);
        }
    }
}
