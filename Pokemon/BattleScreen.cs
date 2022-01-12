using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class BattleScreen : UserControl
    {
        int healthWd = 261;
        int healthHt = 77;
        Brush brush = new SolidBrush(Color.Beige);
        Pen blackPen = new Pen(Color.Black);

        public BattleScreen()
        {
            InitializeComponent();
            Refresh();
        }

        private void BattleScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(brush, 20, 23, healthWd, healthHt);
            e.Graphics.DrawRectangle(blackPen, 20, 23, healthWd, healthHt);

            e.Graphics.FillRectangle(brush, 425, 325, healthWd, healthHt);
            e.Graphics.DrawRectangle(blackPen, 425, 325, healthWd, healthHt);

            e.Graphics.FillRectangle(brush, 20, 430, 500, 150);
            e.Graphics.DrawRectangle(blackPen, 20, 430, 500, 150);

            e.Graphics.FillRectangle(brush, 530, 430, 155, 45);
            e.Graphics.DrawRectangle(blackPen, 530, 430, 155, 45);

            e.Graphics.FillRectangle(brush, 530, 482, 155, 45);
            e.Graphics.DrawRectangle(blackPen, 530, 482, 155, 45);

            e.Graphics.FillRectangle(brush, 530, 535, 155, 45);
            e.Graphics.DrawRectangle(blackPen, 530, 535, 155, 45);


        }

        public void change() { }
        
    }
}
