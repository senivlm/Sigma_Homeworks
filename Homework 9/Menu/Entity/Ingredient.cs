using System;
using System.Collections.Generic;
using System.Text;

namespace MenuLib.Entity
{
    public struct Ingredient
    {
        public string Name { get; set; }

        //public Ingredient()
        //{

        //}

        public Ingredient(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
