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
        public SolidBrush RibonColor { get; private set; }
        public SolidBrush BoxColor { get; private set; }

        public Present(Color color)
        {
            BoxColor = new SolidBrush(color);
            RibonColor = new SolidBrush(color);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(BoxColor, 0, 0, Width, Height);
            
     
        }
    }
}
