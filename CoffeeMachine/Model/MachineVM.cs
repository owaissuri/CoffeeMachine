using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Model
{
    public class MachineVM
    {
        public int outlet { get; set; }
        public List<Ingredient> availableItems { get; set; }

        public List<Beverage> beverages { get; set; }
    }

}
