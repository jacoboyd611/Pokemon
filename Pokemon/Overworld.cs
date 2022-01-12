using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Pokemon
{
    public partial class MainScreen : UserControl
    {
        List<Rectangle> walls = new List<Rectangle>();
        List<Rectangle> grass = new List<Rectangle>();
        Random rand = new Random();

        Brush black = new SolidBrush(Color.Black);
        Player player;
        bool leftArrowDown;
        bool rightArrowDown;
        bool downArrowDown;
        bool upArrowDown;

        public MainScreen()
        {
            InitializeComponent();
            ReadXml();

            player = new Player(186, 518, 20, 30);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            player.Move(leftArrowDown, rightArrowDown, upArrowDown, downArrowDown, walls);
            if (player.Encounter(grass)) { encounterTimer.Enabled = true; }
            else { encounterTimer.Enabled = false;}
            Refresh();
        }

        private void Overworld_Paint(object sender, PaintEventArgs e)
        {
            //test rectangles
            //foreach(Rectangle rec in walls) { e.Graphics.FillRectangle(black, rec); }
            //e.Graphics.FillRectangle(black, player.feetRec);

            e.Graphics.DrawImage(player.currentImage, player.x, player.y, player.width, player.height);       
        }
        private void ReadXml()
        {
            XmlReader reader = XmlReader.Create("Resources/overWorld.xml");
            while (reader.Read())
            {
                reader.ReadToFollowing("tile");
                string type = (reader.GetAttribute("type"));
                int x = Convert.ToInt32(reader.GetAttribute("x"));
                int y = Convert.ToInt32(reader.GetAttribute("y"));
                int width = Convert.ToInt32(reader.GetAttribute("width"));
                int height = Convert.ToInt32(reader.GetAttribute("height"));

                if (type == "Red") { walls.Add(new Rectangle(x, y, width, height)); }
                else if (type == "Green") { grass.Add(new Rectangle(x, y, width, height)); }
            }
            reader.Close();
        }
        private void Overworld_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
            }
        }
        private void Overworld_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false; 
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }
        }
        private void frames_Tick(object sender, EventArgs e)
        {
            if (player.counter < 7) { player.counter++; }
            else { player.counter = 0; }
        }
        private void MainScreen_Load(object sender, EventArgs e)
        {
            Focus();
        }
        private void encounterTimer_Tick(object sender, EventArgs e)
        {
            if(rand.Next(0,11) == 3) 
            {
                encounterTimer.Enabled = false;
                gameTimer.Enabled = false;

                //animation?

                Form f = this.FindForm();
                f.Controls.Remove(this);

                BattleScreen screen = new BattleScreen();
                f.Controls.Add(screen);
            }
        }
    }
}

