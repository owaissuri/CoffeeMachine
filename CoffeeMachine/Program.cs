﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoffeeMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            CoffeeController _controller = new CoffeeController();
            _controller.RunMachine();

        }

    }
}
