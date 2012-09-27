using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimPressDomainModel.Entities
{
    public class SlideTextParameters
    {
        public string Text { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public string FontType { get; set; }
        public string FontFamily { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
    }
}
