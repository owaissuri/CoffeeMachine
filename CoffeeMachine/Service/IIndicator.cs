using CoffeeMachine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Service
{
    interface IIndicator
    {
        void UpdateLowIngredientIndicator(Ingredient ingredient);

        void ShowLowIngredientIndicator();

        void RefillIngredient(Ingredient item);
    }
}
