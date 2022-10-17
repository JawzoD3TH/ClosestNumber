using System;

namespace ClosestNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal CoreNumber = 1;
            while (CoreNumber != 0)
            {
                Console.Write("Core Number > ");
                string Input = Console.ReadLine();
                decimal Rounding = 0;
                bool BadInput = true;

                while (BadInput)
                {
                    try
                    {
                        CoreNumber = Convert.ToDecimal(Input);
                        BadInput = false;
                    }
                    catch
                    {
                        BadInput = true;
                        Console.WriteLine("Bad Input: Need A Number! (Type 0 To Exit)");
                        Console.Write("Core Number > ");
                        Input = Console.ReadLine();
                    }
                }

                if (CoreNumber != 0)
                {
                    BadInput = true;
                    Console.Write("Rounding Zeros > ");
                    Input = Console.ReadLine();
                    int Multiple = 1;

                    while (BadInput)
                    {
                        try
                        {
                            Rounding = Convert.ToDecimal(Input);
                            BadInput = false;
                        }
                        catch
                        {
                            BadInput = true;
                            Console.WriteLine("Bad Input: Need A Number! (Format Example: 0.00)");
                            Console.Write("Rounding Zeros > ");
                            Input = Console.ReadLine();
                        }
                    }

                    string Round;
                    if (Input.StartsWith("0.") || Input.StartsWith("."))
                        Round = Rounding.ToString().Substring(1);
                    else if (Input.StartsWith("00"))
                    {
                        if (CoreNumber % 1 != 0)
                            Round = Input + '.';
                        else Round = Input;
                    }
                    else Round = Rounding.ToString();

                    bool? Succeeded = true;
                    if (Round.Contains(".") && !CoreNumber.ToString().Contains("."))
                        Succeeded = null;

                    if (Succeeded != null)
                    {
                        if (CoreNumber.ToString().Contains(".") && Round.Contains("."))
                        {
                            int FractionPlaces = CoreNumber.ToString().Substring(CoreNumber.ToString().IndexOf('.')).Length;
                            int RoundingPlaces = Round.Substring(Round.IndexOf('.')).Length;
                            if (RoundingPlaces > FractionPlaces) //Math.Round could've worked but it's a string.
                                Round = Round.Substring(0, Round.Length - (RoundingPlaces - FractionPlaces));
                        }

                        decimal Seed = CoreNumber;
                        bool MultipleNotReached = true;
                        int Limit = 49999999; //This is the limit of iterations...
                        while (MultipleNotReached)
                        {
                            if (!CoreNumber.ToString().Contains(Round))
                            {
                                CoreNumber += Seed;
                                Multiple++;

                                if (Multiple == Limit)
                                {
                                    MultipleNotReached = false;
                                    Succeeded = false;
                                }
                            }
                            else MultipleNotReached = false;
                        }
                    }

                    switch (Succeeded)
                    {
                        case null:
                            Console.WriteLine("The Sum Of Two Whole Numbers Will Never Be A Fraction!");
                            break;

                        case true:
                            Console.WriteLine(string.Format("Result: {0} , Multiple: {1}" + '\n', CoreNumber, Multiple));
                            break;

                        case false:
                            Console.WriteLine("Number Was Not Reached...");
                            break;
                    }
                }
            }
        }
    }
}
