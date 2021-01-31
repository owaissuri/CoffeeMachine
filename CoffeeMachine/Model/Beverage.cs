using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Model
{
    public class Beverage
    {
        public string name { get; set; }
        public List<Ingredient> ingredients { get; set; }
    }
}
