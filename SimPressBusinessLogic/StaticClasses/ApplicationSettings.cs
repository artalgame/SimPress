using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimPressDomainModel.EFDbContext;
using SimPressDomainModel.RepositoryClasses;

namespace SimPressBusinessLogic.StaticClasses
{
    /// <summary>
    /// This class realize general settings and basic mechanism of bussiness logic
    /// </summary>
    public static class ApplicationSettings
    {
        private static readonly string CONNECTION_STRING = @"Data Source=ARTAL-ПК\SQLEXPRESS;Initial Catalog=SimPressDataBase;Integrated Security=True;Pooling=False";
        
        public static readonly string LOGIN_REGEX = @"[A-Za-z][A-Za-z0-9_.]{3,31}";
        public static readonly string PASSWORD_REGEX = @"^.*(?=.{6,24})(?=.*\d)(?=.*[a-zA-Z])$";
        public static readonly string EMAIL_REGEX = @"^(?=.*\d)(?=.*[a-zA-Z]).{6,24}$";


        private static SimPressEFDBContext GetContext()
        {
            return new SimPressEFDBContext(CONNECTION_STRING);
        }

        public  static SimPressDomainModel.Interfaces.IRepository GetRepository()
        {
            return new DBRepository(GetContext());
        }
    }
}
