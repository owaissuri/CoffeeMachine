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
        private List<Ingredient> _lowIngredientIndicator;
        private IndicatorRepo _repo;


        internal CoffeeRepo(List<Ingredient> _lowIngredientIndicator, IndicatorRepo repo)
        {
            this._lowIngredientIndicator = _lowIngredientIndicator;
            this._repo = repo;
        }
        internal MachineVM InitializeInput(dynamic data)
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

        internal void ProcessBeverage(MachineVM model)
        {
            if (model != null)
            {
                List<Ingredient> itemCount = AvailableIngredient.Ingredients;
                int freeOutlets = model.outlet;

                //Check if ingredients are present for the given beverages; 
                IEnumerable<MissingIngredients> impossibleBeverages = CheckMissingIngredients(model);

                //Check if it is possible to make each given item through the coffee machine
                foreach (var beverage in model.beverages)
                {
                    if (impossibleBeverages != null && impossibleBeverages.Any(b => b.beverageName == beverage.name))
                    {
                        MissingIngredients item = impossibleBeverages.Where(b => b.beverageName == beverage.name).First();
                        Console.WriteLine(beverage.name + " cannot be prepared because " + item.ingredients.First() + " is not available");
                    }
                    else
                    {
                        if (freeOutlets > 0)
                        {
                            bool canPrepare = true;
                            foreach (var item in beverage.ingredients)
                            {
                                if (itemCount.Any(o=> o.name ==  item.name))
                                {
                                    var currentItem = itemCount.FirstOrDefault(o => o.name == item.name);
                                    //var currentItemName = itemCount.Where(o => o.name == item.name).Select(o => o.name).First();
                                    //var currentItemQuantity = itemCount.Where(o => o.name == item.name).Select(o => o.quantity).First();

                                    if (currentItem.quantity >= item.quantity)
                                    {
                                        //currentItemQuantity = currentItemQuantity - item.quantity;
                                        currentItem.quantity -= item.quantity;
                                       //UpdateAvailableIngredient(currentItem);

                                    }
                                    else
                                    {
                                        canPrepare = false;
                                        Console.WriteLine(beverage.name + " cannot be prepared because " + item.name + " is not sufficient");
                                        _repo.UpdateLowIngredientIndicator(currentItem);
                                        break;
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


            //temp
            _repo.ShowLowIngredientIndicator();
            Console.WriteLine();
            Console.WriteLine("Press Enter for next test case..."); 
            Console.WriteLine();
            Console.ReadLine();
        }

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
