using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimPressDomainModel.Interfaces;
using SimPressDomainModel.EFDbContext;
using SimPressDomainModel.Entities;

namespace SimPressDomainModel.RepositoryClasses
{
    /// <summary>
    /// This class wraps dbContext that returns data from database
    /// </summary>
    public class DBRepository:IRepository
    {
        public SimPressEFDBContext dbContext;
        public DBRepository(SimPressEFDBContext dbContext)
        {
            if (dbContext != null)
            {
                this.dbContext = dbContext;
            }
            else
            {
                throw new NullReferenceException("dbContext is null");
            }
        }

        public IQueryable<User> Users
        {
            get { return dbContext.Users; }
        }

        public IQueryable<Entities.Presentation> Presentations
        {
            get { return dbContext.Presentations; }
        }

        public IQueryable<Entities.Comment> Comments
        {
            get { return dbContext.Comments; }
        }

        public IQueryable<Entities.Like> Likes
        {
            get { return dbContext.Likes; }
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DBRepository()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool isDisposed)
        {
            dbContext.Dispose();
        }


        public void CreateUser(User user)
        {
            dbContext.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            User existUser = dbContext.Users.FirstOrDefault<User>(x => x.UserId == user.UserId);
            if ( existUser == null)
            {
                dbContext.Users.Add(user);
            }
            else
            {
                dbContext.Entry(user).State = System.Data.EntityState.Modified;
            }
        }

        public void DeleteUser(User user)
        {
            dbContext.Users.Remove(user);
        }

        public void CreateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void CreatePresentation(Presentation presentation)
        {
            throw new NotImplementedException();
        }

        public void UpdatePresentation(Presentation presentation)
        {
            throw new NotImplementedException();
        }

        public void DeletePresentation(Presentation presentation)
        {
            throw new NotImplementedException();
        }

        public void CreateLike(Like like)
        {
            throw new NotImplementedException();
        }

        public void UpdateLike(Like like)
        {
            throw new NotImplementedException();
        }

        public void DeleteLike(Like like)
        {
            throw new NotImplementedException();
        }
    }
}
