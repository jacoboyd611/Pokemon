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
    public partial class BagScreen : UserControl
    {
        int buffer = 5;
        int highlighted = 10;
        (int, int) selected = (100, 100);
        bool firstPick = true;

        //Graphics Objects
        Brush brush = new SolidBrush(Color.Beige);
        Brush blackBrush = new SolidBrush(Color.Black);
        Brush greyBrush = new SolidBrush(Color.Gray);
        Brush yellowBrush = new SolidBrush(Color.Yellow);
        Brush greenBrush = new SolidBrush(Color.Green);
        Pen blackPen = new Pen(Color.Black);
        Pen yellowPen = new Pen(Color.Yellow);
        Pen orangePen = new Pen(Color.Orange, 5);
        Font drawFont = new Font("Lucida Console", 16);
        Font boxFont = new Font("Lucida Console", 10);

        int width = 400;
        int height = 400;

        int pokemonW = 100;
        int pokemonH = 100;

        Rectangle healBox;
        Rectangle swapBox;
        Rectangle bigBox;

        string currentButton = "swap";
        string menuState = "";

        PointF[] triangle;
        List<Rectangle> pokemon = new List<Rectangle>();

        public BagScreen()
        {
            InitializeComponent();

            bigBox = new Rectangle(this.Width / 2 - width / 2, this.Height / 2 - height / 2, width, height);
            healBox = new Rectangle(width * 2 / 3 + bigBox.X + buffer, bigBox.Y + height / 2 - 47, 115, 45);
            swapBox = new Rectangle(width * 2 / 3 + bigBox.X + buffer, height / 2 + bigBox.Y + 22, 115, 45);

            Rectangle rec = new Rectangle(bigBox.X + 25, bigBox.Y + 25, pokemonW, pokemonH);
            Rectangle newRec = rec;

            triangle = Triangle(new Point { X = healBox.X + healBox.Width * 3 / 4 + buffer, Y = healBox.Y + healBox.Height / 2 });

            for (int i = 0; i < 6; i++)
            {
                if (i >= 0 && i < 2)
                {
                    if (i == 0) { pokemon.Add(newRec); }
                    newRec = new Rectangle(newRec.X, newRec.Y + newRec.Height + 25, newRec.Width, newRec.Height);
                    pokemon.Add(newRec);
                }
                else if (i > 2)
                {
                    if (i == 3)
                    {
                        newRec = new Rectangle(rec.X + rec.Width + 25, rec.Y, rec.Width, rec.Height);
                        pokemon.Add(newRec);
                    }
                    else
                    {
                        newRec = new Rectangle(newRec.X, newRec.Y + newRec.Height + 25, newRec.Width, newRec.Height);
                        pokemon.Add(newRec);
                    }
                }
            }

            Refresh();
        }
        private void BagScreen_Paint(object sender, PaintEventArgs e)
        {
            //square
            e.Graphics.FillRectangle(greyBrush, bigBox);
            e.Graphics.DrawRectangle(blackPen, bigBox);

            e.Graphics.FillRectangle(brush, healBox);
            e.Graphics.DrawRectangle(blackPen, healBox);
            e.Graphics.DrawString("HEAL", drawFont, blackBrush, healBox.X + healBox.Width / 2 - 30, healBox.Y + healBox.Height / 2 - drawFont.Height / 2);

            e.Graphics.FillRectangle(brush, swapBox);
            e.Graphics.DrawRectangle(blackPen, swapBox);
            e.Graphics.DrawString("SWAP", drawFont, blackBrush, swapBox.X + swapBox.Width / 2 - 30, swapBox.Y + swapBox.Height / 2 - drawFont.Height / 2);

            for (int i = 0; i < pokemon.Count; i++)
            {

                

                e.Graphics.FillRectangle(brush, pokemon[i]);
                if (i == highlighted) { e.Graphics.DrawRectangle(yellowPen, pokemon[i]); }
                else { e.Graphics.DrawRectangle(blackPen, pokemon[i]); }
                if (i == selected.Item1 || i == selected.Item2) { e.Graphics.DrawRectangle(orangePen, pokemon[i]); }
                e.Graphics.DrawString($"{i + 1}", boxFont, blackBrush, pokemon[i].X + buffer, pokemon[i].Y + buffer);

                if (Form1.party[i].name == "Pikachu" || Form1.party[i].name == "Charizard"
               || Form1.party[i].name == "Squirtle" || Form1.party[i].name == "Rattata") { e.Graphics.DrawImage(Form1.party[i].front, pokemon[i].X + buffer, pokemon[i].Y + buffer, pokemon[i].Width - 2 * buffer, pokemon[i].Height - 2 * buffer); }

                if (Form1.party[i] != null)
                {
                    e.Graphics.DrawString($"{Form1.party[i].name}", boxFont, blackBrush, pokemon[i].X + buffer, pokemon[i].Y + pokemon[i].Height - boxFont.Height * 2);
                }

                float length = Form1.party[i].health / Form1.party[i].totalHealth * (pokemon[i].Width - 2 * buffer);
                e.Graphics.FillRectangle(greenBrush, pokemon[i].X + buffer, pokemon[i].Y + pokemon[i].Height - buffer * 2, length, 5);
                e.Graphics.DrawRectangle(blackPen, pokemon[i].X + buffer, pokemon[i].Y + pokemon[i].Height - buffer * 2, pokemon[i].Width - 2 * buffer, 5);
            }
            e.Graphics.FillPolygon(yellowBrush, triangle);
            e.Graphics.DrawPolygon(blackPen, triangle);

        }
        public void Change(string position)
        {
            switch (position)
            {
                case ("swap"):
                    currentButton = position;
                    triangle = Triangle(new Point { X = swapBox.X + swapBox.Width * 3 / 4 + buffer, Y = swapBox.Y + swapBox.Height / 2 });
                    break;
                case ("heal"):
                    currentButton = position;
                    triangle = Triangle(new Point { X = healBox.X + healBox.Width * 3 / 4 + buffer, Y = healBox.Y + healBox.Height / 2 });
                    break;
                case ("p0"):
                    currentButton = position;
                    highlighted = 0;
                    break;
                case ("p1"):
                    currentButton = position;
                    highlighted = 1;
                    break;
                case ("p2"):
                    currentButton = position;
                    highlighted = 2;
                    break;
                case ("p3"):
                    currentButton = position;
                    highlighted = 3;
                    break;
                case ("p4"):
                    currentButton = position;
                    highlighted = 4;
                    break;
                case ("p5"):
                    currentButton = position;
                    highlighted = 5;
                    break;
            }
            Refresh();
        }
        private void Swap((int, int) chosen)
        {
            Pokemon temp = Form1.party[chosen.Item1];
            Form1.party[chosen.Item1] = Form1.party[chosen.Item2];
            Form1.party[chosen.Item2] = temp;
            firstPick = true;
            selected = (100, 100);
            Refresh();
        }
        private void BagScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        Form f = FindForm();
                        f.Controls.Remove(this);
                        MainScreen screen = new MainScreen();
                        f.Controls.Add(screen);
                    }
                    break;
                case Keys.A:
                    {
                        if (menuState == "swap")
                        {
                            if (firstPick)
                            {
                                selected.Item1 = highlighted;
                                firstPick = false;
                            }
                            else
                            {
                                selected.Item2 = highlighted;
                                Swap(selected);
                            }
                        }
                        else if (menuState == "heal") { Form1.party[highlighted].health = Form1.party[highlighted].totalHealth; Refresh(); }
                        else if (currentButton == "swap")
                        {
                            menuState = "swap";
                            Change("p0");
                        }
                        else
                        {
                            menuState = "heal";
                            Change("p0");
                        }
                        break;
                    }
                case Keys.S:
                    {
                        if (menuState != "")
                        {
                            menuState = "";
                            highlighted = 10;
                            selected = (10, 10);
                            firstPick = false;
                            Refresh();
                        }
                        break;
                    }
                case Keys.Down:
                    {
                        if (menuState == "")
                        {
                            if (currentButton == "swap") { Change("heal"); }
                            else { Change("swap"); }
                        }
                        else
                        {
                            switch (currentButton)
                            {
                                case ("p0"): { Change("p1"); break; }
                                case ("p1"): { Change("p2"); break; }
                                case ("p2"): { Change("p0"); break; }
                                case ("p3"): { Change("p4"); break; }
                                case ("p4"): { Change("p5"); break; }
                                case ("p5"): { Change("p3"); break; }
                            }
                        }
                    }
                    break;
                case Keys.Up:
                    {
                        if (menuState == "")
                        {
                            if (currentButton == "swap") { Change("heal"); }
                            else { Change("swap"); }
                        }
                        else
                        {
                            switch (currentButton)
                            {
                                case ("p0"): { Change("p2"); break; }
                                case ("p1"): { Change("p0"); break; }
                                case ("p2"): { Change("p1"); break; }
                                case ("p3"): { Change("p5"); break; }
                                case ("p4"): { Change("p3"); break; }
                                case ("p5"): { Change("p4"); break; }
                            }
                        }
                        break;
                    }
                case Keys.Left:
                    {
                        switch (currentButton)
                        {
                            case ("p0"): { Change("p3"); break; }
                            case ("p1"): { Change("p4"); break; }
                            case ("p2"): { Change("p5"); break; }
                            case ("p3"): { Change("p0"); break; }
                            case ("p4"): { Change("p1"); break; }
                            case ("p5"): { Change("p2"); break; }
                        }
                    }
                    break;
                case Keys.Right:
                    {
                        switch (currentButton)
                        {
                            case ("p0"): { Change("p3"); break; }
                            case ("p1"): { Change("p4"); break; }
                            case ("p2"): { Change("p5"); break; }
                            case ("p3"): { Change("p0"); break; }
                            case ("p4"): { Change("p1"); break; }
                            case ("p5"): { Change("p2"); break; }
                        }
                    }
                    break;
            }
        }
        public PointF[] Triangle(Point p)
        {
            //triangle formula
            //x1, x2 = x3 = x1+15; y1, y2 = y1+10, y3 = y1-10;
            PointF[] temp = new PointF[] { p, new Point { X = p.X + 15, Y = p.Y + 10 }, new Point { X = p.X + 15, Y = p.Y - 10 } };
            return temp;
        }
        private void BagScreen_Load(object sender, EventArgs e)
        {
            Focus();
        }
    }
}
