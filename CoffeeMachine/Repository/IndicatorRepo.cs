using CoffeeMachine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Repository
{
    internal class IndicatorRepo
    {
        private List<Ingredient> _lowIngredientIndicator;

        public IndicatorRepo(List<Ingredient> _lowIngredientIndicator)
        {
            this._lowIngredientIndicator = _lowIngredientIndicator;
        }

        internal void UpdateLowIngredientIndicator(Ingredient ingredient)
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

        internal void ShowLowIngredientIndicator()
        {
            if (_lowIngredientIndicator != null)
            {
                Console.WriteLine("---Low Ingredients Indicator---");
                foreach (var item in _lowIngredientIndicator)
                {
                    Console.WriteLine("Item -> " + item.name + "   Current quantity available -> " + item.quantity);
                }
                Console.WriteLine();
            }
        }
    }
}
