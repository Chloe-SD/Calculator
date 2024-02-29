using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace Calculator
{
    static class Solver
    {
        // This class is for building, manipulating, and solving the logical version of the equation
        // (The human readable equation for the calculator display is handles in the Input class)
        public static string? Calculatable {  get; set; }
        // The calculatable string, built at the same time as the equation string
        public static void AddEntryToCalculatable(string entry)
        {
            // This is called whenever a Matho Operator is pressed
            if (Calculatable != null && entry != null) // check that both calculatable and entry are not null
            {
                int lastIndex = Calculatable.Length-1 ; // get the current length of the calculatable string
                List <char> mathOperators = new List <char>(['+','-','*','/']); // a list of math operators (for building logic)
                if (entry[0] == '(' || Calculatable[lastIndex] == ')') // determine if calculatable ends with, or entry starts with bracket
                {
                    if (!mathOperators.Contains(Calculatable[lastIndex])) // determine if the last character of the calculatable is a math operator
                    {
                        // if the last char of calculatable is ), and the first char of entry is (, this worn work when we try to solve
                        // so we need to insert a multiplication sign.
                        // for example, we understand that (2)(3) is actually (2)*(3), but the solver needs the sign to be sure. 
                        // Note that this is only added to Calculatable, the human readable display will not show this sign.
                        Calculatable += "*";
                    }
                    
                }
            }
            Calculatable += entry; // add current entry string to calculatable string
        }
        public static string Solve()
        {
            if (Calculatable != null) // ensure calculatable is not null
            {
                try // try to solve the calculatable statement
                {
                    DataTable table = new DataTable(); // using DataTable
                    var result = table.Compute(Calculatable, ""); // pass calculatable to DataTable, set var for result
                    Calculatable = null; // erase calculatable string
                    if (result.ToString() == "∞") // check if result is infinity
                    {
                        // Error message, Deviding by zero was returning infinity, this was not right so I added an error
                        return "Error";
                    }
                    string FormattedResult = FormatResult(Convert.ToDecimal(result)); // format the result for the display
                    return FormattedResult; // send result back to the caller
                }
                catch (Exception) // IF NOT SOLVABLE
                {
                    Calculatable = null; // erase the calculatable
                    return "Error"; // return error message to caller
                }
            }
            else { return "Error"; } // if none of that worked, send an Error to the caller
        }
        private static string FormatResult(decimal result)
        {
            string formattedResult;
            string resultString = result.ToString("#0.##########"); // Rounds doubles to 13 digits (to fit display)
            int decimalIndex = resultString.IndexOf('.'); // gets index of decimal (if there is one)
            int preDecimalDigits = ((long)result).ToString().Length; // makes a long of the result (Cuts off decimal)
            if (decimalIndex > 13 || preDecimalDigits > 13) // determine if the int part of the result is more than 13 digits
            {
                // Error if int part of result is too long for the screen
                return "Error";
            }
            else
            {
                int maxDigits = 13 - decimalIndex; // set max result length (13 digits to fit on screen)
                decimal roundedResult = Math.Round(result, maxDigits); // round anything longer than this
                formattedResult = roundedResult.ToString(); // set at result (as a string)
            }
            return formattedResult; // return result
        }  
        public static void AddCalculatableOperator(string mathOperator)
        {
            // this method takes the human readable math operator, and converts it to what the computer
            // needs to perforn the calculations.
            if (mathOperator == "÷")
            {
                Calculatable += "/";
                return;
            }
            else if (mathOperator == "×")
            {
                Calculatable += "*";
                return;
            }
            else // must be either + or -
            {
                Calculatable += mathOperator;
            }
        }
    }


}
