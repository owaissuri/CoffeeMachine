using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine.Repository;

namespace CoffeeMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            CoffeeRepo _service = new CoffeeRepo();
            FileRepo _file = new FileRepo();

            //Make the path dynamic
            var data1 = _file.Read("C:\\Users\\I19132\\source\\repos\\CoffeeMachine\\testcase\\T1.JSON");
            var data2 = _file.Read("C:\\Users\\I19132\\source\\repos\\CoffeeMachine\\testcase\\T2.JSON");
            var data3 = _file.Read("C:\\Users\\I19132\\source\\repos\\CoffeeMachine\\testcase\\T3.JSON");

            var output1 = _service.InitializeInput(data1);
            _service.ProcessBeverage(output1);
            var output2 = _service.InitializeInput(data2);
            _service.ProcessBeverage(output2);
            var output3 = _service.InitializeInput(data3);
            _service.ProcessBeverage(output3);

        }

        
    }
}
