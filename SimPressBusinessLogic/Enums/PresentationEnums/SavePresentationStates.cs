using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimPressBusinessLogic.Enums.PresentationEnums
{
    public enum SavePresentationStates
    {
        Ok,
        UserNotExist,
        UserCantRules,
        WrongName,
        WrongDescription,
        WrongTags,
        DataBaseError
    }
}
