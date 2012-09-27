using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimPressBusinessLogic.Enums.PresentationEnums;
using SimPressDomainModel.Entities;
using SimPressDomainModel.Entities.PresentationEntities;

namespace SimPressBusinessLogic.PresentationClasses
{
    public class PresentationBuilder
    {
        public SavePresentationStates SavePresentationState { get; set; }
        public void SaveNewPresentation(PreCreatedPresentation presentation,User user)
        {
            if(user == null)
            {
                SavePresentationState = SavePresentationStates.UserCantRules;
                return;
            }
            
        }
    }
}
