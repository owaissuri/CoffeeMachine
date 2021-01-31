using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine.Model;
using CoffeeMachine.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CoffeeMachine.Repository
{
    public class CoffeeRepo : ICoffee
    {
        private IIndicator _repo;

        internal CoffeeRepo()
        {
            _repo = new IndicatorRepo();
        }

        //Method to process json file data into C# objects
        public MachineVM InitializeInput(dynamic data)
        {
            MachineVM model = new MachineVM();

            try
            {
                //Set total outlet count
                model.outlet = Convert.ToInt32(data.machine.outlets.count_n);

                //set total available items
                IList<Ingredient> totalItems = new List<Ingredient>();
                foreach (var item in data.machine.total_items_quantity)
                {
                    totalItems.Add(new Ingredient { name = item.Name, quantity = Convert.ToInt32(item.Value) });
                }
                model.availableItems = totalItems.ToList();

                AvailableIngredient.Ingredients = model.availableItems;

                //set given beverages
                List<Beverage> beverages = new List<Beverage>();
                foreach (var item in data.machine.beverages)
                {
                    Beverage beverage = new Beverage();
                    List<Ingredient> recipes = new List<Ingredient>();
                    beverage.name = item.Name;
                    foreach (var i in item.Value)
                    {
                        recipes.Add( new Ingredient {name = i.Name, quantity = Convert.ToInt32(i.Value) });
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


        //Main method to process the inputs of current test case and return result accordingly
        public void ProcessBeverage(MachineVM model)
        {
            if (model != null)
            {
                List<Ingredient> itemCount = AvailableIngredient.Ingredients;
                int freeOutlets = model.outlet;

                //Check if All ingredients are present for the given beverages; 
                IEnumerable<MissingIngredients> impossibleBeverages = CheckMissingIngredients(model);

                //Check if it is possible to make each given item through the coffee machine
                //Print result accordingly
                foreach (var beverage in model.beverages)
                {
                    if (impossibleBeverages != null && impossibleBeverages.Any(b => b.beverageName == beverage.name))
                    {
                        MissingIngredients item = impossibleBeverages.Where(b => b.beverageName == beverage.name).First();
                        Console.WriteLine(beverage.name + " cannot be prepared because " + item.ingredients.First() + " is not available");
                    }
                    else
                    {
                        //Check if coffee machine outlets are available
                        if (freeOutlets > 0)
                        {
                            bool canPrepare = true;
                            bool printResult = false;
                            foreach (var item in beverage.ingredients)
                            {
                                if (itemCount.Any(o=> o.name ==  item.name))
                                {
                                    var currentItem = itemCount.FirstOrDefault(o => o.name == item.name);
                                    
                                    if (currentItem.quantity >= item.quantity)
                                    {
                                        currentItem.quantity -= item.quantity;

                                    }
                                    else
                                    {
                                        canPrepare = false;
                                        if (!printResult)
                                        {
                                            Console.WriteLine(beverage.name + " cannot be prepared because " + item.name + " is not sufficient");
                                            printResult = true;
                                        }
                                        
                                        _repo.UpdateLowIngredientIndicator(currentItem);
                                    }
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
            }
            else
            {
                Console.WriteLine("Invalid input data");
                Console.ReadLine();
                return;
            }
            Console.WriteLine();

            _repo.ShowLowIngredientIndicator();
            Console.WriteLine();
            Console.WriteLine("Press Enter for next test case...\n\n\n"); 
            Console.ReadLine();
        }


        //Helper method to verify if all the ingredients are available for a particular beverage
        private IEnumerable<MissingIngredients> CheckMissingIngredients(MachineVM model)
        {
            List<Ingredient> itemCount = model.availableItems;
            IList<MissingIngredients> result = new List<MissingIngredients>();
            try
            {
                foreach (var beverage in model.beverages)
                {
                    MissingIngredients product = new MissingIngredients();
                    List<string> prodIngredients = new List<string>();

                    foreach (var item in beverage.ingredients)
                    {
                        if (!itemCount.Any(o => o.name == item.name))
                        {
                            if (string.IsNullOrEmpty(product.beverageName))
                            {
                                product.beverageName = beverage.name;
                                prodIngredients.Add(item.name);
                            }
                            else
                            {
                                prodIngredients.Add(item.name);
                            }

                        }

                    }
                    if (product.beverageName != null)
                    {
                        product.ingredients = prodIngredients;
                        result.Add(product);
                    }

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return result;
        }
        
    }
}
