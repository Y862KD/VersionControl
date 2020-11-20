using FejlesztesiMintak_Y862KD.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FejlesztesiMintak_Y862KD.Entities
{
    public class Present : Toy
    {
        public SolidBrush BoxColor { get; private set; }
        public SolidBrush RibbonColor { get; private set; }

        public Present(Color color1, Color color2)
        {
            BoxColor = new SolidBrush(color1);
            RibbonColor = new SolidBrush(color2);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(BoxColor, 0, 0, Height, Width);
            
            //ez biztos rossz
            g.FillRectangle(RibbonColor, 0, 0, Height/5, Width);
        }
    }
}
