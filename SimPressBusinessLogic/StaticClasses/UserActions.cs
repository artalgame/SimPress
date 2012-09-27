using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimPressDomainModel.Entities;

namespace SimPressBusinessLogic.StaticClasses
{
    public static class UserActions
    {
        public static bool UserExist(Guid userId)
        {
            if(FindUserById(userId) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public static User FindUserById(Guid userId)
        {
            using(var repo = ApplicationSettings.GetRepository())
            {
                return repo.Users.FirstOrDefault(x => x.UserId == userId); 
            }
        }

    }
}
