using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Pokemon
/*TODO
 * Turn system
 * Enemy values
 * End fight
 */
{
    public partial class BattleScreen : UserControl
    {
        int buffer = 5;
        int y1 = 446;
        int y2 = 522;
        int x1 = 240;
        int x2 = 490;

        int turn = 0;
        string message;
        string tempMessage = "";
        int charCounter = 0;
        string menuState = "";
        string currentbutton = "fight";
        int currentPokemon = 0;
        bool playerTurn = true;
        bool textDone = false;

        Pokemon enemy;
        Random randGen = new Random();

        //UI boxes
        Rectangle enemyHealthBox = new Rectangle(20, 23, 261, 77);
        Rectangle playerHealthBox = new Rectangle(425, 325, 261, 77);
        Rectangle moveBox = new Rectangle(20, 430, 500, 150);
        Rectangle fightBox = new Rectangle(530, 430, 155, 45);
        Rectangle bagBox = new Rectangle(530, 482, 155, 45);
        Rectangle runBox = new Rectangle(530, 535, 155, 45);
        //array for drawing
        Rectangle[] boxes = new Rectangle[5];
        //draw triangle indicator
        PointF[] triangle;

        //Graphics Objects
        Brush brush = new SolidBrush(Color.Beige);
        Brush blackBrush = new SolidBrush(Color.Black);
        Brush greyBrush = new SolidBrush(Color.Gray);
        Brush yellowBrush = new SolidBrush(Color.Yellow);
        Brush greenBrush = new SolidBrush(Color.Green);

        Pen blackPen = new Pen(Color.Black);
        Font drawFont = new Font("Lucida Console", 16);

        public BattleScreen()
        {
            InitializeComponent();
            enemy = Form1.possibleEnemies[randGen.Next(Form1.possibleEnemies.Count)];
            message = $"What will {Form1.party[currentPokemon].name} do?";

            triangle = Triangle(new Point { X = 660, Y = 452 });

            boxes[0] = enemyHealthBox;
            boxes[1] = playerHealthBox;
            boxes[2] = fightBox;
            boxes[3] = bagBox;
            boxes[4] = runBox;
            Refresh();
        }
        private void BattleScreen_Paint(object sender, PaintEventArgs e)
        {
            if (Form1.party[currentPokemon].name == "Pikachu" || Form1.party[currentPokemon].name == "Charizard"
                || Form1.party[currentPokemon].name == "Squirtle") { e.Graphics.DrawImage(Form1.party[currentPokemon].back, moveBox.X + 75, moveBox.Y - 200, 250, 200); }
            if (enemy.name == "Pikachu" || enemy.name == "Charizard" || enemy.name == "Squirtle") { e.Graphics.DrawImage(enemy.front, playerHealthBox.X+125, playerHealthBox.Y- 200, 100, 100); }

            foreach (Rectangle box in boxes)
            {
                e.Graphics.FillRectangle(brush, box);
                e.Graphics.DrawRectangle(blackPen, box);
            }

            ////Enemy Info
            e.Graphics.DrawString(enemy.name, drawFont, blackBrush, enemyHealthBox.X + buffer, enemyHealthBox.Y + buffer);
            float length = enemy.health / enemy.totalHealth * (enemyHealthBox.Width - 2 * buffer);
            e.Graphics.FillRectangle(greenBrush, enemyHealthBox.X + buffer, enemyHealthBox.Y + drawFont.Height + buffer, length, 5);
            e.Graphics.DrawRectangle(blackPen, enemyHealthBox.X + buffer, enemyHealthBox.Y + drawFont.Height + buffer, enemyHealthBox.Width - 2 * buffer, 5);

            //Player Info
            e.Graphics.DrawString(Form1.party[currentPokemon].name, drawFont, blackBrush, playerHealthBox.X + buffer, playerHealthBox.Y + buffer);
            length = Form1.party[currentPokemon].health / Form1.party[currentPokemon].totalHealth * (playerHealthBox.Width - 2 * buffer);
            e.Graphics.FillRectangle(greenBrush, playerHealthBox.X + buffer, playerHealthBox.Y + drawFont.Height + buffer, length, 5);
            e.Graphics.DrawRectangle(blackPen, playerHealthBox.X + buffer, playerHealthBox.Y + drawFont.Height + buffer, playerHealthBox.Width - 2 * buffer, 5);

            #region move avaiblitly stuff : Pokemon can have less than 4 moves sooooooooooo
            e.Graphics.FillRectangle(greyBrush, moveBox);
            if (menuState == "fight")
            {
                //attacks
                e.Graphics.FillRectangle(brush, moveBox.X, moveBox.Y, moveBox.Width / 2, moveBox.Height / 2);
                e.Graphics.DrawString(Form1.party[currentPokemon].move1.name, drawFont, blackBrush, moveBox.X + buffer, moveBox.Y + buffer);

                if (Form1.party[0].move2 != null)
                {
                    e.Graphics.FillRectangle(brush, moveBox.X + moveBox.Width / 2, moveBox.Y, moveBox.Width / 2, moveBox.Height / 2);
                    e.Graphics.DrawString(Form1.party[currentPokemon].move2.name, drawFont, blackBrush, moveBox.X + moveBox.Width / 2 + buffer, moveBox.Y + buffer);
                    if (Form1.party[0].move3 != null)
                    {
                        e.Graphics.FillRectangle(brush, moveBox.X, moveBox.Y + moveBox.Height / 2, moveBox.Width / 2, moveBox.Height / 2);
                        e.Graphics.DrawString(Form1.party[currentPokemon].move3.name, drawFont, blackBrush, moveBox.X + buffer, moveBox.Y + moveBox.Height / 2 + buffer);
                        if (Form1.party[0].move4 != null)
                        {
                            e.Graphics.FillRectangle(brush, moveBox.X + moveBox.Width / 2, moveBox.Y + moveBox.Height / 2, moveBox.Width / 2, moveBox.Height / 2);
                            e.Graphics.DrawString(Form1.party[currentPokemon].move3.name, drawFont, blackBrush, moveBox.X + moveBox.Width / 2 + buffer, moveBox.Y + moveBox.Height / 2 + buffer);
                        }
                    }
                }
            }
            #endregion

            e.Graphics.DrawRectangle(blackPen, moveBox);

            //lines
            e.Graphics.DrawLine(blackPen, moveBox.X + moveBox.Width / 2, moveBox.Y, moveBox.X + moveBox.Width / 2, moveBox.Y + moveBox.Height);
            e.Graphics.DrawLine(blackPen, moveBox.X, moveBox.Y + moveBox.Height / 2, moveBox.X + moveBox.Width, moveBox.Y + moveBox.Height / 2);
            //buttons
            e.Graphics.DrawString("FIGHT", drawFont, blackBrush, fightBox.X + 42, fightBox.Y + fightBox.Height / 2 - drawFont.Height / 2);
            e.Graphics.DrawString("BAG", drawFont, blackBrush, bagBox.X + 50, bagBox.Y + bagBox.Height / 2 - drawFont.Height / 2);
            e.Graphics.DrawString("RUN", drawFont, blackBrush, runBox.X + 50, runBox.Y + runBox.Height / 2 - drawFont.Height / 2);

            //Write message
            e.Graphics.DrawString(tempMessage, drawFont, blackBrush, enemyHealthBox.X, moveBox.Y - drawFont.Height);
            if (tempMessage != message)
            {
                playerTurn = false;
                if (timer1.Enabled == false) { timer1.Enabled = true; }
            }
            else
            {
                WriteStop();
                if (textDone == false)
                {
                    //draw cursor
                    e.Graphics.FillPolygon(yellowBrush, triangle);
                    e.Graphics.DrawPolygon(blackPen, triangle);
                }
            }
        }
        public void PlayerTurn(string move)
        {
            switch (move)
            {
                case ("attack1"):
                    tempMessage = "";
                    if (menuState == "fight")
                    {
                        enemy.health -= Form1.party[currentPokemon].move1.damage;
                        Change("fight");
                        menuState = "";
                        message = $"{Form1.party[currentPokemon].name} uses {Form1.party[currentPokemon].move1.name}.";
                        if (enemy.health <= 0)
                        {
                            turn = 4;
                            message = $"{enemy.name} was defeated. ";
                        }
                        else { turn = 1; }
                    }
                    break;
                case ("attack2"):
                    break;
            }
            Refresh();
        }
        public void EnemyTurn()
        {
            switch (randGen.Next(enemy.moveNum))
            {
                case (0):
                    Form1.party[currentPokemon].health -= enemy.move1.damage;
                    tempMessage = "";
                    message = $"{enemy.name} uses {enemy.move1.name}.";
                    break;
                case (1):
                    Form1.party[currentPokemon].health -= enemy.move2.damage;
                    break;
                case (2):
                    Form1.party[currentPokemon].health -= enemy.move3.damage;
                    break;
                case (3):
                    Form1.party[currentPokemon].health -= enemy.move4.damage;
                    break;
            }
            if (Form1.party[currentPokemon].health <= 0)
            {
                turn = 4;
                message = $"{Form1.party[currentPokemon].name} was defeated. ";
            }
            else { turn = 2; }
            Refresh();
        }
        public void Change(string position)
        {
            switch (position)
            {
                case ("bag"):
                    currentbutton = position;
                    triangle = Triangle(new Point { X = 660, Y = 504 });
                    break;
                case ("run"):
                    currentbutton = position;
                    triangle = Triangle(new Point { X = 660, Y = 556 });
                    break;
                case ("fight"):
                    currentbutton = position;
                    triangle = Triangle(new Point { X = 660, Y = 452 });
                    break;

                //attacks
                case ("attack1"):
                    currentbutton = position;
                    triangle = Triangle(new Point { X = x1, Y = y1 });
                    break;
                case ("attack2"):
                    currentbutton = position;
                    triangle = Triangle(new Point { X = x2, Y = y1 });
                    break;
                case ("attack3"):
                    currentbutton = position;
                    triangle = Triangle(new Point { X = x1, Y = y2 });
                    break;
                case ("attack4"):
                    currentbutton = position;
                    triangle = Triangle(new Point { X = x2, Y = y2 });
                    break;
            }
            Refresh();
        }
        public PointF[] Triangle(Point p)
        {
            //triangle formula
            //x1, x2 = x3 = x1+15; y1, y2 = y1+10, y3 = y1-10;
            PointF[] temp = new PointF[] { p, new Point { X = p.X + 15, Y = p.Y + 10 }, new Point { X = p.X + 15, Y = p.Y - 10 } };
            return temp;
        }
        private void BattleScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (textDone)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        textDone = false;
                        if (turn == 0)
                        {
                            playerTurn = true;
                            Refresh();
                        }
                        else if (turn == 1)
                        {
                            EnemyTurn();
                        }
                        else if (turn == 2)
                        {
                            message = $"What will {Form1.party[currentPokemon].name} do?";
                            tempMessage = "";
                            turn = 0;
                            Refresh();
                        }
                        else if (turn == 3)
                        {
                            tempMessage = "";
                            if (randGen.Next(2) == 1)
                            {
                                message = $"{Form1.party[currentPokemon].name} got away safely.";
                                turn = 4;
                            }
                            else
                            {
                                message = $"{Form1.party[currentPokemon].name} couldn't get away.";
                                turn = 1;
                            }
                            Refresh();
                        }
                        else if (turn == 4) { EndGame(); }
                        break;
                }
            }
            else if (playerTurn)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        if (currentbutton == "fight")
                        {
                            menuState = "fight";
                            Change("attack1");
                        }
                        else if (currentbutton == "bag") { }
                        else if (currentbutton == "run")
                        {
                            message = $"{Form1.party[currentPokemon].name} tries to run away.";
                            tempMessage = "";
                            turn = 3;
                            Refresh();
                        }
                        else
                        {
                            if (menuState == "fight") { PlayerTurn(currentbutton); }
                        }
                        break;
                    case Keys.S:
                        menuState = "";
                        Change("fight");
                        break;
                    case Keys.Down:
                        {
                            if (menuState == "")
                            {
                                if (currentbutton == "fight") { Change("bag"); }
                                else if (currentbutton == "bag") { Change("run"); }
                                else if (currentbutton == "run") { Change("fight"); }
                            }
                            else if (menuState == "fight")
                            {
                                if (currentbutton == "attack1") { Change("attack3"); }
                                else if (currentbutton == "attack3") { Change("attack1"); }

                                if (currentbutton == "attack2") { Change("attack4"); }
                                else if (currentbutton == "attack4") { Change("attack2"); }
                            }
                        }
                        break;
                    case Keys.Up:
                        {
                            if (menuState == "")
                            {
                                if (currentbutton == "fight") { Change("run"); }
                                else if (currentbutton == "bag") { Change("fight"); }
                                else if (currentbutton == "run") { Change("bag"); }
                            }
                            else if (menuState == "fight")
                            {
                                if (currentbutton == "attack1") { Change("attack3"); }
                                else if (currentbutton == "attack3") { Change("attack1"); }

                                if (currentbutton == "attack2") { Change("attack4"); }
                                else if (currentbutton == "attack4") { Change("attack2"); }
                            }
                        }
                        break;
                    case Keys.Left:
                        {
                            if (menuState == "fight")
                            {
                                if (currentbutton == "attack1") { Change("attack2"); }
                                else if (currentbutton == "attack2") { Change("attack1"); }

                                if (currentbutton == "attack3") { Change("attack4"); }
                                else if (currentbutton == "attack4") { Change("attack3"); }
                            }
                        }
                        break;
                    case Keys.Right:
                        {
                            if (menuState == "fight")
                            {
                                if (currentbutton == "attack1") { Change("attack2"); }
                                else if (currentbutton == "attack2") { Change("attack1"); }

                                if (currentbutton == "attack3") { Change("attack4"); }
                                else if (currentbutton == "attack4") { Change("attack3"); }
                            }
                        }
                        break;
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            tempMessage += message[charCounter];
            charCounter++;
            if (tempMessage == message) { textDone = true; }
            Refresh();
        }
        private void WriteStop()
        {
            timer1.Enabled = false;
            charCounter = 0;
        }
        private void EndGame()
        {
            Form f = this.FindForm();
            f.Controls.Remove(this);
            MainScreen screen = new MainScreen();
            f.Controls.Add(screen);
        }
    }
}
