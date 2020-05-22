using System;
using ApplicationTestsCouches.model;

namespace ApplicationTestsCouches
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            dbal monDbal = new dbal();

            monDbal.Select("personnel");

        }
    }
}
