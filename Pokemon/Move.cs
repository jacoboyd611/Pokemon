using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Move
    {
        public string name, type;
        public int damage;

        public Move(string _name, string _type, int _damage)
        {
            name = _name;
            type = _type;
            damage = _damage;
        }
    }
}
