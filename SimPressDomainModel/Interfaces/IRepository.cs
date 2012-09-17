using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimPressDomainModel.Entities;

namespace SimPressDomainModel.Interfaces
{
    /// <summary>
    /// This interface describes behaviour of classes, which provide interaction with some source of data(databases, xml files or other)
    /// </summary>
   public  interface IRepository:IDisposable
    {
       IQueryable<User> Users { get; }
       IQueryable<Presentation> Presentations { get; }
       IQueryable<Comment> Comments { get; }
       IQueryable<Like> Likes { get; }

       void CreateUser(User user);
       void UpdateUser(User user);
       void DeleteUser(User user);

       void CreateComment(Comment comment);
       void UpdateComment(Comment comment);
       void DeleteComment(Comment comment);

       void CreatePresentation(Presentation presentation);
       void UpdatePresentation(Presentation presentation);
       void DeletePresentation(Presentation presentation);

       void CreateLike(Like like);
       void UpdateLike(Like like);
       void DeleteLike(Like like);

       void SaveChanges();
    }
}
