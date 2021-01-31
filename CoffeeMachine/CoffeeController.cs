using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine.Model;
using CoffeeMachine.Repository;

namespace CoffeeMachine
{
    public class CoffeeController
    {
        public static List<Ingredient> _lowIngredientIndicator = new List<Ingredient>();
        static IndicatorRepo _indicatorService = new IndicatorRepo(_lowIngredientIndicator);
        //IndicatorRepo _indicatorRepo = new IndicatorRepo(_lowIngredientIndicator);

        CoffeeRepo _service = new CoffeeRepo(_lowIngredientIndicator, _indicatorService);
        FileRepo _file = new FileRepo();


        public void RunMachine()
        {
            //Make the path dynamic
            var data1 = _file.Read("C:\\Users\\I19132\\source\\repos\\CoffeeMachine\\testcase\\T1.JSON");
            var data2 = _file.Read("C:\\Users\\I19132\\source\\repos\\CoffeeMachine\\testcase\\T2.JSON");
            var data3 = _file.Read("C:\\Users\\I19132\\source\\repos\\CoffeeMachine\\testcase\\T3.JSON");

            //TEST CASES 
            var output1 = _service.InitializeInput(data1);
            _service.ProcessBeverage(output1);

            var output2 = _service.InitializeInput(data2);
            _service.ProcessBeverage(output2);

            var output3 = _service.InitializeInput(data3);
            _service.ProcessBeverage(output3);
        }

    }
}
