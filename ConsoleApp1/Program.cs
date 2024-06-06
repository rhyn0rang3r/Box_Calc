using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "Length";
            string? numInput2 = "Width";
            string? numInput3 = "Height";
            double result = 0;
            double c = 0;
            double bc = 0;
            double cleanNum1 = 0;
            double cleanNum2 = 0;
            double cleanNum3 = 0;



            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Pad");
            Console.WriteLine("\ts - HSC");
            Console.WriteLine("\tm - RSC");
            Console.WriteLine("\td - Lid");
            Console.Write("Your Option?:");

            string? op = Console.ReadLine();



            if (op == "a")
            {

                // Ask the user to type the Length.
                Console.Write("Type Length, and then press Enter: ");
                numInput1 = Console.ReadLine();


                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the Width.
                Console.Write("Type Width, and then press Enter: ");
                numInput2 = Console.ReadLine();


                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }

            }

            else if (op == "d" || op == "m" || op == "s")
            {

                // Ask the user to type the Length.
                Console.Write("Type Length, and then press Enter: ");
                numInput1 = Console.ReadLine();

                
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the Width.
                Console.Write("Type Width, and then press Enter: ");
                numInput2 = Console.ReadLine();

                
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to type the Height.
                Console.Write("Type Height, and then press Enter: ");
                numInput3 = Console.ReadLine();

                while (!double.TryParse(numInput3, out cleanNum3))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput3 = Console.ReadLine();
                }


            }

            // Convert from inches to feet
            double ConvertFromInchestToFeet(double numberToConvert)
            {
                var poop = numberToConvert / 12;
                return poop;
            }
            double convertedNum1 = ConvertFromInchestToFeet(cleanNum1);
            double convertedNum2 = ConvertFromInchestToFeet(cleanNum2);
            double convertedNum3 = ConvertFromInchestToFeet(cleanNum3);


            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    result = Calculator.CalculateSqFt(cleanNum1, cleanNum2, cleanNum3, op);
                    var rscValue = Calculator.CalculateRscLength(cleanNum1, cleanNum2);
                    c = Calculator.GetMostEfficientFanFold(rscValue, "Single");
                    bc = Calculator.GetMostEfficientFanFold(rscValue, "Double");
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##} sq.ft\n", result);
                        Console.WriteLine("Most efficent FanFold: C {0:0.###}", c);
                        Console.WriteLine("Most efficent Fanfold: BC {0:0.###}", bc);

                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        return;
    }
}

class Calculator
{ 
    public static double CalculateSqFt(double num1, double num2, double num3, string op)
    {
        double glueflap = 2.2;
        double flap = 3;
        double rsclength = ((num1 * 2) + (num2 * 2) + glueflap) / 12;
        double rscwidth = (num3 + (flap * 2)) / 12;
        double hscwidth = (num3 + flap) / 12;
        double lidlength = (num1 + (num3 * 2)) / 12;
        double lidheight = (num2 + (num3 * 2)) / 12;
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

        // Use a switch statement to do the math.
        switch (op)
        {
            //Pad
            case "a":
                result = (num1 / 12) * (num2 / 12);
                break;
            //HSC
            case "s":
                result = (rsclength * hscwidth);
                break;
            //RSC
            case "m":
                result = (rsclength * rscwidth);
                break;
            //Lid
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = (lidheight * lidlength);
                }
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        return result;
    }

    public static double GetMostEfficientFanFold(double rscValue, string typeOfWall)
    {
        double  mostEfficientFanFold = 0;
        List<double> _singleWallFanFoldWidths = new List<double>();
        List<double> _doubleWallFanFoldWidths = new List<double>();
        _singleWallFanFoldWidths.Add(70.625);
        _singleWallFanFoldWidths.Add(51.625);
        _singleWallFanFoldWidths.Add(79.125);
        _singleWallFanFoldWidths.Add(31.125);

        _doubleWallFanFoldWidths.Add(75.25);
        _doubleWallFanFoldWidths.Add(61.375);
        _doubleWallFanFoldWidths.Add(52.625);
        _doubleWallFanFoldWidths.Add(40.625);

        if (string.Equals(typeOfWall, "Single", StringComparison.OrdinalIgnoreCase))
        {
            _singleWallFanFoldWidths.ForEach(x => x = rscValue / x);
            mostEfficientFanFold = _singleWallFanFoldWidths.Min();
        }
        else
        {
            _doubleWallFanFoldWidths.ForEach(x => x = rscValue / x);
            mostEfficientFanFold = _doubleWallFanFoldWidths.Min();
        }
    
       return mostEfficientFanFold;
    }

    public static double CalculateRscLength(double num1, double num2)
    {
        double glueflap = 2.2;
        var rsclength = ((num1 * 2) + (num2 * 2) + glueflap) / 12;

        return rsclength;
    }
   
}

