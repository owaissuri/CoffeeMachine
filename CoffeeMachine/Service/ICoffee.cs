using CoffeeMachine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Service
{
    public interface ICoffee
    {
        MachineVM InitializeInput(dynamic data);

        void ProcessBeverage(MachineVM model);
    }
}
