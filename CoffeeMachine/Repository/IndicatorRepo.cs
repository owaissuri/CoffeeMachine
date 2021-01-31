using CoffeeMachine.Model;
using CoffeeMachine.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Repository
{
    internal class IndicatorRepo : IIndicator
    {
        private List<Ingredient> _lowIngredientIndicator;

        public IndicatorRepo()
        {
            this._lowIngredientIndicator = new List<Ingredient>();
        }

        public void UpdateLowIngredientIndicator(Ingredient ingredient)
        {
            if(_lowIngredientIndicator.Any(os => os.name  == ingredient.name))
            {
                var item = _lowIngredientIndicator.FirstOrDefault(os => os.name == ingredient.name);
                item.quantity = ingredient.quantity;
            }
            else
            {
                _lowIngredientIndicator.Add(new Ingredient
                {
                    name = ingredient.name,
                    quantity = ingredient.quantity,
                });
            }
            
        }

        public void ShowLowIngredientIndicator()
        {
            if (_lowIngredientIndicator.Count > 0)
            {
                Console.WriteLine("---Low Ingredients Indicator---");
                foreach (var item in _lowIngredientIndicator)
                {
                    Console.WriteLine("Item -> " + item.name + "   Current quantity available -> " + item.quantity);
                }
                Console.WriteLine();
            }
        }

        public void RefillIngredient(Ingredient item)
        {
            //Code to refill ingredients
        }
    }
}
