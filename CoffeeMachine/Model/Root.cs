using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Outlets
    {
        public int count_n { get; set; }
    }

    public class TotalItemsQuantity
    {
        public int hot_water { get; set; }
        public int hot_milk { get; set; }
        public int ginger_syrup { get; set; }
        public int sugar_syrup { get; set; }
        public int tea_leaves_syrup { get; set; }
    }

    public class HotTea
    {
        public int hot_water { get; set; }
        public int hot_milk { get; set; }
        public int ginger_syrup { get; set; }
        public int sugar_syrup { get; set; }
        public int tea_leaves_syrup { get; set; }
    }

    public class HotCoffee
    {
        public int hot_water { get; set; }
        public int ginger_syrup { get; set; }
        public int hot_milk { get; set; }
        public int sugar_syrup { get; set; }
        public int tea_leaves_syrup { get; set; }
    }

    public class BlackTea
    {
        public int hot_water { get; set; }
        public int ginger_syrup { get; set; }
        public int sugar_syrup { get; set; }
        public int tea_leaves_syrup { get; set; }
    }

    public class GreenTea
    {
        public int hot_water { get; set; }
        public int ginger_syrup { get; set; }
        public int sugar_syrup { get; set; }
        public int green_mixture { get; set; }
    }

    public class Beverages
    {
        public HotTea hot_tea { get; set; }
        public HotCoffee hot_coffee { get; set; }
        public BlackTea black_tea { get; set; }
        public GreenTea green_tea { get; set; }
    }

    public class Machine
    {
        public Outlets outlets { get; set; }
        public TotalItemsQuantity total_items_quantity { get; set; }
        public Beverages beverages { get; set; }
    }

    public class Root
    {
        public Machine machine { get; set; }
    }


}
