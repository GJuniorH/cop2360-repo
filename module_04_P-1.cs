using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter your first number:");
        string inputA = Console.ReadLine();

        Console.WriteLine("Please enter your second number:");
        string inputB = Console.ReadLine();

        try
        {
            int numberA = Convert.ToInt32(inputA);
            int numberB = Convert.ToInt32(inputB);

            int result = Divide(numberA, numberB);
            Console.WriteLine($"The result of dividing {numberA} by {numberB} is: {result}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine("There was an error with one or both input that are invalid to be converted to intergers.");
            Console.WriteLine($"More info: {ex.Message}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("You are not allowed to divide by zero.");
            Console.WriteLine($"More info: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error had occurred, See below for more details.");
            Console.WriteLine($"More info: {ex.Message}");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static int Divide(int a, int b)
    {
        return a / b;
    }
}