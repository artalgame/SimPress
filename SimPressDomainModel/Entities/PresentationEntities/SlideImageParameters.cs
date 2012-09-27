using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimPressDomainModel.Entities
{
    public class SlideImageParameters
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Url { get; set; }
    }
}
