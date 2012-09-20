using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SimPressDomainModel.EFDbContext;
using SimPressDomainModel.Interfaces;
using SimPressDomainModel.RepositoryClasses;
using Ninject;
using Moq;

namespace SimPressBusinessLogic
{
    /// <summary>
    /// Some methods for Registration process: all checks and so on.
    /// </summary>
    public class RegistrationActions
    {
        private static readonly string CONNECTION_STRING = @"Data Source=ARTAL-ПК\SQLEXPRESS;Initial Catalog=SimPressDataBase;Integrated Security=True;Pooling=False";
        private static readonly string LOGIN_REGEX = @"[A-Za-z][A-Za-z0-9_.]{3,31}";
        private static IKernel ninjectKernel;

        static RegistrationActions()
        {
            ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<SimPressEFDBContext>().ToSelf().WithConstructorArgument("connectionString", CONNECTION_STRING);
        }

        private static SimPressEFDBContext GetContext()
        {
            return new SimPressEFDBContext(CONNECTION_STRING);
        }

        public bool IsLoginFree(string login)
        {
            using (IRepository repository = ninjectKernel.Get<DBRepository>())
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
            return Regex.IsMatch(login, LOGIN_REGEX);
        }

        public bool IsLoginIsGood(string login)
        {
            return IsLoginFree(login) && IsLoginIsCorrected(login);
        }


    }
}
