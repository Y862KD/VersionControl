using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using FejlesztesiMintak_Y862KD.Abstractions;

namespace FejlesztesiMintak_Y862KD.Entities
{
    public class Ball : Toy
    {

        protected override void DrawImage(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height);
        }



    }
}
