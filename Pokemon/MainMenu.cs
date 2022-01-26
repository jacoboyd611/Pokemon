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
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        #region Play Button
        private void playButton_Enter(object sender, EventArgs e)
        {
            playButton.BackColor = Color.Yellow;
        }
        private void playButton_Leave(object sender, EventArgs e)
        {
            playButton.BackColor = Color.Khaki;
        }


        #endregion

        #region Exit Button
        private void exitButton_Enter(object sender, EventArgs e)
        {
            exitButton.BackColor = Color.Red;
        }
        private void exitButton_Leave(object sender, EventArgs e)
        {
            exitButton.BackColor = Color.IndianRed;
        }

        #endregion

        private void playButton_Click(object sender, EventArgs e)
        {
            Form f = this.FindForm();
            f.Controls.Remove(this);
            MainScreen screen = new MainScreen();
            f.Controls.Add(screen);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
