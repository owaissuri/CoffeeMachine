using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CoffeeMachine.Repository
{
    public class CoffeeRepo
    {
        public MachineVM InitializeInput(dynamic data)
        {
            MachineVM model = new MachineVM();

            try
            {
                //Set total outlet count
                model.outlet = Convert.ToInt32(data.machine.outlets.count_n);

                //set total available items
                Dictionary<string, int> totalItems = new Dictionary<string, int>();
                foreach (var item in data.machine.total_items_quantity)
                {
                    totalItems.Add(item.Name, Convert.ToInt32(item.Value));
                }
                model.availableItems = totalItems;

                //set given beverages
                List<Beverage> beverages = new List<Beverage>();
                foreach (var item in data.machine.beverages)
                {
                    Beverage beverage = new Beverage();
                    Dictionary<string, int> recipes = new Dictionary<string, int>();
                    beverage.name = item.Name;
                    foreach (var i in item.Value)
                    {
                        recipes.Add(i.Name, Convert.ToInt32(i.Value));
                    }
                    beverage.ingredients = recipes;
                    beverages.Add(beverage);
                }
                model.beverages = beverages;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


            return model;

        }

        public void ProcessBeverage(MachineVM model)
        {
            if (model != null)
            {
                Dictionary<string, int> itemCount = model.availableItems;
                int freeOutlets = model.outlet;

                //Check if it is possible to make each given item through the coffee machine
                foreach (var beverage in model.beverages)
                {
                    if (freeOutlets > 0)
                    {
                        bool canPrepare = true;
                        foreach (var item in beverage.ingredients)
                        {
                            if (itemCount.ContainsKey(item.Key))
                            {
                                if (itemCount[item.Key] >= item.Value)
                                {
                                    itemCount[item.Key] = itemCount[item.Key] - item.Value;

                                }
                                else
                                {
                                    canPrepare = false;
                                    Console.WriteLine(beverage.name + " cannot be prepared because " + item.Key + " is not sufficient");
                                    break;
                                }
                            }
                            else
                            {
                                canPrepare = false;
                                Console.WriteLine(beverage.name + " cannot be prepared because " + item.Key + " is not available");
                                break;
                            }

                        }
                        if (canPrepare)
                        {
                            freeOutlets--;
                            Console.WriteLine(beverage.name + " is prepared");
                        }
                    }
                    else
                    {
                        Console.WriteLine(beverage.name + " cannot be prepared as free outlet is not available");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input data");
                Console.ReadLine();
                return;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press Enter for next test case...");
            Console.ReadLine();

        }
    }
}
