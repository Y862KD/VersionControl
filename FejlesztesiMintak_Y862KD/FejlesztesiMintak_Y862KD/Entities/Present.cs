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
        public Rectangle BoxColor { get; private set; }
        public Rectangle RibbonColor { get; private set; }

        public Present(Color color)
        {
            BoxColor = new (color);
            RibbonColor = new Rectangle(color);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(BoxColor, 0, 0, Height, Width);
        }
    }
}
