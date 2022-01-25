using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pokemon
{
    public class Player
    {
        public int x, y, width, height, speed, counter = 0;
        string lastDirection = "up";
        public Image currentImage = Properties.Resources.tile036;
        public Rectangle feetRec;

        #region Image arrays
        Image[] upFrames = { Properties.Resources.tile036, Properties.Resources.tile037, Properties.Resources.tile038,
        Properties.Resources.tile039, Properties.Resources.tile040, Properties.Resources.tile041, Properties.Resources.tile042,
        Properties.Resources.tile043};

        Image[] downFrames = { Properties.Resources.tile000, Properties.Resources.tile001, Properties.Resources.tile002,
        Properties.Resources.tile003, Properties.Resources.tile004, Properties.Resources.tile005, Properties.Resources.tile006,
        Properties.Resources.tile007};

        Image[] leftFrames = { Properties.Resources.tile012, Properties.Resources.tile013, Properties.Resources.tile014,
        Properties.Resources.tile015, Properties.Resources.tile016, Properties.Resources.tile017, Properties.Resources.tile018,
        Properties.Resources.tile019};

        Image[] rightFrames = { Properties.Resources.tile024, Properties.Resources.tile025, Properties.Resources.tile026,
        Properties.Resources.tile027, Properties.Resources.tile028, Properties.Resources.tile029, Properties.Resources.tile030,
        Properties.Resources.tile031};
        #endregion

        public Player(int _x, int _y, int _width, int _height)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            speed = 2;
            feetRec = new Rectangle(x, y + height, width, height);

        }

        public void Move(bool left, bool right, bool up, bool down, List<Rectangle> walls)
        {
            if (left && collision(walls, x, y + height / 2))
            {
                x -= speed;
                currentImage = leftFrames[counter];
                lastDirection = "left";
            }
            else if (right && collision(walls, x + width, y + height / 2))
            {
                x += speed;
                currentImage = rightFrames[counter];
                lastDirection = "right";
            }
            else if (up && collision(walls, x + width / 2, y))
            {
                y -= speed;
                currentImage = upFrames[counter];
                lastDirection = "up";
            }
            else if (down && collision(walls, x + width / 2, y + height))
            {
                y += speed;
                currentImage = downFrames[counter];
                lastDirection = "down";
            }
            else
            {
                switch (lastDirection)
                {
                    case "left":
                        currentImage = leftFrames[0];
                        break;
                    case "right":
                        currentImage = rightFrames[0];
                        break;
                    case "up":
                        currentImage = upFrames[0];
                        break;
                    case "down":
                        currentImage = downFrames[0];
                        break;
                }
            }
            feetRec = new Rectangle(x, y +  height*2/3, width, height/5);
        }

        public bool Encounter(List<Rectangle> grass)
        {
            foreach (Rectangle rec in grass)
            {
                if (feetRec.IntersectsWith(rec)) 
                { 
                    return true; 
                }
            }
            return false;
        }

        public bool collision(List<Rectangle> walls, int x, int y)
        {
            foreach (Rectangle rec in walls)
            {
                if (rec.Contains(x, y)) { return false; }
            }
            return true;
        }
    }
}
