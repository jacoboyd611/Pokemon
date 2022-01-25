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
using System.Diagnostics;

namespace Pokemon
{
    public partial class Form1 : Form
    {
        public static Pokemon[] party = new Pokemon[6];
        public static List<Pokemon> possibleEnemies = new List<Pokemon>();
        public Form1()
        {
            InitializeComponent();
            ReadParty();
            ReadEnemy();
            MainMenu screen = new MainMenu();
            this.Controls.Add(screen);
        }
        private void ReadParty()
        {
            XmlReader reader = XmlReader.Create("Resources/Pokemon.xml");
            while (reader.Read())
            {
                for (int i = 0; i < 6; i++)
                {
                    reader.ReadToFollowing("name");
                    string name = (reader.ReadString());
                    if (name != "")
                    {
                        reader.ReadToNextSibling("level");
                        int lvl = Convert.ToInt32(reader.ReadString());

                        reader.ReadToNextSibling("health");
                        int health = Convert.ToInt32(reader.ReadString());

                        reader.ReadToNextSibling("move1");
                        string movename = reader.GetAttribute("name");
                        int damage = Convert.ToInt32(reader.GetAttribute("damage"));
                        string type = reader.GetAttribute("type");
                        Move move1 = new Move(movename, type, damage);

                        reader.ReadToNextSibling("move2");
                        movename = reader.GetAttribute("name");
                        if (movename != "")
                        {
                            damage = Convert.ToInt32(reader.GetAttribute("damage"));
                            type = reader.GetAttribute("type");
                            Move move2 = new Move(movename, type, damage);

                            reader.ReadToNextSibling("move3");
                            movename = reader.GetAttribute("name");
                            if (movename != "")
                            {
                                damage = Convert.ToInt32(reader.GetAttribute("damage"));
                                type = reader.GetAttribute("type");
                                Move move3 = new Move(movename, type, damage);

                                reader.ReadToNextSibling("move4");
                                movename = reader.GetAttribute("name");
                                if (movename != "")
                                {
                                    damage = Convert.ToInt32(reader.GetAttribute("damage"));
                                    type = reader.GetAttribute("type");
                                    Move move4 = new Move(movename, type, damage);
                                    Pokemon p = new Pokemon(name, health, lvl, move1, move2, move3, move4);
                                    party[i] = p;
                                }
                                else
                                {
                                    Pokemon p = new Pokemon(name, health, lvl, move1, move2, move3);
                                    party[i] = p;
                                }
                            }
                            else
                            {
                                Pokemon p = new Pokemon(name, health, lvl, move1, move2);
                                party[i] = p;
                            }
                        }
                        else
                        {
                            Pokemon p = new Pokemon(name, health, lvl, move1);
                            party[i] = p;
                        }
                    }
                }
            }
            reader.Close();
        }
        private void ReadEnemy()
        {
            XmlReader reader = XmlReader.Create("Resources/Enemy.xml");
            while (reader.Read())
            {
                reader.ReadToFollowing("name");
                string name = (reader.ReadString());
                if (name != "")
                {
                    reader.ReadToNextSibling("level");
                    int lvl = Convert.ToInt32(reader.ReadString());

                    reader.ReadToNextSibling("health");
                    int health = Convert.ToInt32(reader.ReadString());

                    reader.ReadToNextSibling("move1");
                    string movename = reader.GetAttribute("name");
                    int damage = Convert.ToInt32(reader.GetAttribute("damage"));
                    string type = reader.GetAttribute("type");
                    Move move1 = new Move(movename, type, damage);

                    reader.ReadToNextSibling("move2");
                    movename = reader.GetAttribute("name");
                    if (movename != "")
                    {
                        damage = Convert.ToInt32(reader.GetAttribute("damage"));
                        type = reader.GetAttribute("type");
                        Move move2 = new Move(movename, type, damage);

                        reader.ReadToNextSibling("move3");
                        movename = reader.GetAttribute("name");
                        if (movename != "")
                        {
                            damage = Convert.ToInt32(reader.GetAttribute("damage"));
                            type = reader.GetAttribute("type");
                            Move move3 = new Move(movename, type, damage);

                            reader.ReadToNextSibling("move4");
                            movename = reader.GetAttribute("name");
                            if (movename != "")
                            {
                                damage = Convert.ToInt32(reader.GetAttribute("damage"));
                                type = reader.GetAttribute("type");
                                Move move4 = new Move(movename, type, damage);
                                Pokemon p = new Pokemon(name, health, lvl, move1, move2, move3, move4);
                                possibleEnemies.Add(p);
                            }
                            else
                            {
                                Pokemon p = new Pokemon(name, health, lvl, move1, move2, move3);
                                possibleEnemies.Add(p);
                            }
                        }
                        else
                        {
                            Pokemon p = new Pokemon(name, health, lvl, move1, move2);
                            possibleEnemies.Add(p);
                        }
                    }
                    else
                    {
                        Pokemon p = new Pokemon(name, health, lvl, move1);
                        possibleEnemies.Add(p);
                    }
                }

            }
            reader.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
