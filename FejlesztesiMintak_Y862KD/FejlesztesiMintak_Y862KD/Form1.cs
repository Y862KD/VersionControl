using FejlesztesiMintak_Y862KD.Abstractions;
using FejlesztesiMintak_Y862KD.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FejlesztesiMintak_Y862KD
{
    public partial class Form1 : Form
    {
        List<Toy> _toys = new List<Toy>();
        private Toy _nextToy;

        private IToyFactory _factory;
        public IToyFactory Factory
        {
            get { return _factory; }
            set 
            { _factory = value;
                DisplayNext();
            }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
            buttonColorSelect.BackColor = Color.Khaki;
            buttonBox.BackColor = Color.Green;
            buttonRibbon.BackColor = Color.Yellow;
        }

        private void DisplayNext()
        {
            if (_nextToy != null)
                Controls.Remove(_nextToy);
            _nextToy = Factory.CreateNew();
            _nextToy.Top = labelNext.Top + labelNext.Height + 20;
            _nextToy.Left = labelNext.Left;
            Controls.Add(_nextToy);

        }


        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);            

        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;

            foreach (var toy in _toys)
            {
                toy.MoveToy();
                if (toy.Left > maxPosition)
                    maxPosition = toy.Left;    
            }   

            if (maxPosition > 1000) 
            {
                var toy = _toys[0];
                _toys.Remove(toy);
                mainPanel.Controls.Remove(toy);

            }

        }

        private void buttonCar_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void buttonBall_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory
            {
                BallColor = buttonColorSelect.BackColor
            };
            
        }

        private void buttonPresent_Click(object sender, EventArgs e)
        {
            Factory = new PresentFactory
            { BoxColor = buttonBox.BackColor };
        }

        private void buttonColorSelect_Click(object sender, EventArgs e)
        {
            
            var button = (Button)sender;
            var colorSelect = new ColorDialog();

            colorSelect.Color = button.BackColor;
            if (colorSelect.ShowDialog() != DialogResult.OK) return;
            button.BackColor = colorSelect.Color;
        }


    }
}
