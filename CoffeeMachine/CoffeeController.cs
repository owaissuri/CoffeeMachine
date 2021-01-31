using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine.Model;
using CoffeeMachine.Repository;
using CoffeeMachine.Service;
using System.IO;

namespace CoffeeMachine
{
    public class CoffeeController
    {
        private readonly ICoffee _service1  = new CoffeeRepo();
        private readonly ICoffee _service2 = new CoffeeRepo();
        private readonly ICoffee _service3 = new CoffeeRepo();
        private readonly ICoffee _service4 = new CoffeeRepo();
        private readonly IFile _file = new FileRepo();


        public void RunMachine()
        {
            //Test case file location -- feel free to modify the inputs
            var data1 = _file.Read(@"..\\..\\testcase\\T1.JSON");
            var data2 = _file.Read(@"..\\..\\testcase\\T2.JSON");
            var data3 = _file.Read(@"..\\..\\testcase\\T3.JSON");
            var data4 = _file.Read(@"..\\..\\testcase\\T4.JSON");

            //TEST CASES 
            var input1 = _service1.InitializeInput(data1);
            _service1.ProcessBeverage(input1);

            var input2 = _service2.InitializeInput(data2);
            _service2.ProcessBeverage(input2);

            var input3 = _service3.InitializeInput(data3);
            _service3.ProcessBeverage(input3);

            var input4 = _service4.InitializeInput(data4);
            _service4.ProcessBeverage(input4);
        }

    }
}
