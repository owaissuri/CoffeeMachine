using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Model
{
    public class MissingIngredients
    {
        public string beverageName { get; set; }

        public List<string> ingredients { get; set; }
    }
}
