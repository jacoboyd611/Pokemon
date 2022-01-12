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

        #region Instructions Button
        private void instructionsButton_Enter(object sender, EventArgs e)
        {
            instructionsButton.BackColor = Color.DodgerBlue;
        }
        private void instructionsButton_Leave(object sender, EventArgs e)
        {
            instructionsButton.BackColor = Color.SteelBlue;
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
    }
}
