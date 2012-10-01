using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimPressDomainModel.Entities;
using SimPressDomainModel.Interfaces;
using SimPressBusinessLogic.StaticClasses;
using SimPressDomainModel.Entities.PresentationEntities;

namespace SimPressBusinessLogic.UsefulClasses
{
    public class PresentationActions
    {
        public bool IsPresentationExist(Guid presentationId)
        {
            return FindPresentationById(presentationId) == null ? false : true;
        }

        public Presentation FindPresentationById(Guid presentationId)
        {
            using (IRepository repo = ApplicationSettings.GetRepository())
            {
                return repo.Presentations.FirstOrDefault(x => x.PresentationId == presentationId);
            }
        }
        public List<Presentation> GetSomeLatestPresentation(int count)
        {
            using (IRepository repo = ApplicationSettings.GetRepository())
            {
                return repo.Presentations.OrderBy(x => x.CreateDate).Take(count).ToList();
            }
        }
        public Presentation FormNewPresentation(PreCreatedPresentation presentationData, Guid userId)
        {
            if (presentationData == null)
            {
                return null;
            }
            return new Presentation
            {
                CreateDate = DateTime.Now,
                Description = presentationData.Description,
                Name = presentationData.Name,
                PresentationId = Guid.NewGuid(),
                Tags = presentationData.Tags,
                UserId = userId
            };
        }
    }
}
