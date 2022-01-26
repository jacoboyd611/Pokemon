using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pokemon
{
    public class Pokemon
    {
        public int moveNum;
        public float health, level, totalHealth;
        public string name;
        public Move move1, move2, move3, move4;
        public Image front, back;
        public Pokemon(string _name, int _health, int _level, Move _move1)
        {
            name = _name;
            totalHealth = health = _health;
            level = _level;
            move1 = _move1;
            move2 = null;
            move3 = null;
            move4 = null;

            moveNum = 1;
            Sprites();
        }
        public Pokemon(string _name, int _health, int _level, Move _move1, Move _move2)
        {
            name = _name;
            health = _health;
            level = _level;
            move1 = _move1;
            move2 = _move2;
            move3 = null;
            move4 = null;

            moveNum = 2;
            Sprites();
        }
        public Pokemon(string _name, int _health, int _level, Move _move1, Move _move2, Move _move3)
        {
            name = _name;
            health = _health;
            level = _level;
            move1 = _move1;
            move2 = _move2;
            move3 = _move3;
            move4 = null;

            moveNum = 3;
            Sprites();
        }
        public Pokemon(string _name, int _health, int _level, Move _move1, Move _move2, Move _move3, Move _move4)
        {
            name = _name;
            health = _health;
            level = _level;
            move1 = _move1;
            move2 = _move2;
            move3 = _move3;
            move4 = _move4;

            moveNum = 4;
            Sprites();
        }

        public void Sprites()
        {
            switch (name)
            {
                case ("Pikachu"):
                    front = Properties.Resources.pikachuFront;
                    back = Properties.Resources.pikachuBehind;
                    break;
                case ("Charizard"):
                    front = Properties.Resources.charizardFront;
                    back = Properties.Resources.charizardBack;
                    break;
                case ("Squirtle"):
                    front = Properties.Resources.squirtleFront;
                    back = Properties.Resources.squirtleBack;
                    break;
                case ("Rattata"):
                    front = Properties.Resources.rattataFront;
                    back = Properties.Resources.rattataBack;
                    break;
                case ("Infernape"):
                    front = Properties.Resources.infernapeFront;
                    back = Properties.Resources.infernapeBack;
                    break;
                case ("Pigeotto"):
                    front = Properties.Resources.pigeottoFront;
                    back = Properties.Resources.pigeottoBack;
                    break;
            }
        }
    }
}
