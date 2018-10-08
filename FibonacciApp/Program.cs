using System;
using System.Collections;
using System.Threading;

namespace FibonacciApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Fibonacci sequence application!");

            //Getting singleton object - not scalable, but given how simple the app is, then it should be fine.
            var businessLogicObject = BusinessLogic.GetInstance;

            Console.WriteLine("The first 11 Fibonacci numbers from 0 are: ");

            //Finding the first 11 Fibonacci numbers.
            PrintArray(businessLogicObject.GetFibNumbers(11));

            Rerun(11, businessLogicObject);
        }

        private static void Rerun(int level, BusinessLogic businessLogicObject)
        {
            Console.WriteLine("Would you like to find more than "+level+" Fibonacci numbers? Enter 1 for yes and 0 for no");

            //Option to find out more Fibonacci numbers.
            int userInput;

            while (!int.TryParse(Console.ReadLine(), out userInput))
            {
                Console.WriteLine("This is a very simple application, please enter 0 or 1");
            }

            //Base case: if user chooses to exit.
            if (userInput == 0)
            {
                Console.WriteLine("Thanks for using this app!");
                Thread.Sleep(3000);
            }

            //Recursive case: if user wants to find more than 11 levels of Fibonacci numbers.
            else
            {
                Console.WriteLine("How many Fibonacci numbers from 0 would you like to know?");
                while (!int.TryParse(Console.ReadLine(), out userInput))
                {
                    Console.WriteLine("Please input a valid number!");
                }
                PrintArray(businessLogicObject.GetFibNumbers(userInput));
                Rerun(userInput, businessLogicObject);

            }
        }

        /*
         * Function to print array with commas.
         * TODO: potentially implement List interface rather than ArrayList - just can't remember how to do it atm. 
         */
        private static void PrintArray(ArrayList arrayList)
        {

            for (var j = 0; j < arrayList.Count; j++)
            {
                if (j == arrayList.Count - 1)
                {
                    Console.Write(arrayList[j] + "\n");
                }
                else
                {
                    Console.Write(arrayList[j] + ", ");
                }

            }
        }

    }

}

/*
 * Logic to get Fibonacci numbers.
 * TODO Does not consider integer overflow, would need to look into further.
 * TODO Would also like to implement method to know automatically if a number is a Fibonacci number rather than manually calculating from 0.
 */
internal class BusinessLogic
{
    private ArrayList _fibonacciNumArray;
    //Fibonacci number normally is sum of two Fibonacci numbers in sequence [n + (n+1)]: so n = _first and _second is the next Fibonacci number
    private int _first;
    private int _second;

    public ArrayList GetFibNumbers(int level)
    {
        while (true)
        {
            //Level defined is correct
            if (_fibonacciNumArray.Count == level)
            {
                return _fibonacciNumArray;
            }
            //Level is less than array size, then Fibonacci numbers will alreay exist, so will return to that level
            else if (_fibonacciNumArray.Count > level)
            {
                var shorterList = new ArrayList();

                for (var x = 0; x < level; x++)
                {
                    shorterList.Add(_fibonacciNumArray[x]);
                }

                return shorterList;
            }
            //Level is more than array size, will need to find more Fibonacci numbers
            else
            {

                if (_fibonacciNumArray.Count > 1)
                {
                    int count = _fibonacciNumArray.Count;
                    _first = (int)_fibonacciNumArray[count - 1];
                    _second = (int)_fibonacciNumArray[count - 2];
                }

                var fibNum = _first + _second;
                _fibonacciNumArray.Add(fibNum);
            }
        }
    }


    private static BusinessLogic _instance = null;
    public static BusinessLogic GetInstance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BusinessLogic();
            }

            return _instance;
        }
    }

    private BusinessLogic()
    {
        _fibonacciNumArray = new ArrayList();
        //Setting the initial numbers for the Fibonacci sequence to be 0 and 1
        _first = 0;
        _second = _first + 1;
    }
}
