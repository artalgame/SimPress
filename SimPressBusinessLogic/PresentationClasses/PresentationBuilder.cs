using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimPressBusinessLogic.Enums.PresentationEnums;
using SimPressDomainModel.Entities;
using SimPressDomainModel.Entities.PresentationEntities;
using SimPressBusinessLogic.UsefulClasses;
using SimPressBusinessLogic.StaticClasses;
using SimPressDomainModel.Interfaces;

namespace SimPressBusinessLogic.PresentationClasses
{
    public class PresentationBuilder
    {
        public SavePresentationStates SavePresentationState { get; set; }
        public string DataBaseErrorText;
        public void SaveNewPresentation(PreCreatedPresentation presentation, User user)
        {
            if (user == null)
            {
                SavePresentationState = SavePresentationStates.UserCantRules;
                return;
            }
            if (new UserActions().IsUserExist(user))
            {
                SavePresentationState = SavePresentationStates.UserNotExist;
                return;
            }
            if (presentation == null)
            {
                SavePresentationState = SavePresentationStates.NullData;
                return;
            }
            if ((presentation.Name == null) || (presentation.Name == ""))
            {
                SavePresentationState = SavePresentationStates.WrongName;
                return;
            }
            try
            {
                var createdPresentation = new PresentationActions().FormNewPresentation(presentation, user.UserId);
                TrySavePresentation(createdPresentation);
                SavePresentationState = SavePresentationStates.Ok;
            }
            catch (Exception ex)
            {
                SavePresentationState = SavePresentationStates.DataBaseError;
                DataBaseErrorText = ex.Message;
            }
        }

        private void TrySavePresentation(Presentation createdPresentation)
        {
            using(IRepository repo = ApplicationSettings.GetRepository())
            {
                repo.CreatePresentation(createdPresentation);
                repo.SaveChanges();
            }
        }
    }
}
