using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimPressDomainModel.Entities
{
    public class SlideInfo
    {
        public int SlideNumber { get; set; }
        public SlideTextParameters HeaderText { get; set; }
        public SlideTextParameters ContentText { get; set; }
        public string FonColor { get; set; }
        public SlideImageParameters Image { get; set; }
    }
}
