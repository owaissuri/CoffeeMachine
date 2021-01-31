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
        public Dictionary<string,int> availableItems { get; set; }

        public List<Beverage> beverages { get; set; }
    }

    public class Beverage
    {
        public string name { get; set; }
        public Dictionary<string,int> ingredients { get; set; }
    }
}
