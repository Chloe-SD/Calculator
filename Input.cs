using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calculator
{
    static class Input
    {
        // this class handles the human readable equation string, it also sends information to the Solver class to
        // build the Calculatable string at the same time. 

        // class variables
        public static string? EntryString; // current number being inputted, displays on screen
        public static string? EquationString { get; set; } // current equation string, displays on screen
        public static bool BracketOpen = false; // keep track of when brackets are open or closed

        public static string UpdateReadout()
        {

            if (EntryString == null)
            {
                if (EquationString == null)
                {
                    return "0"; // diaplay reads 0 if all strings are null
                }
                return EquationString; // display shows only Entry string if Equation string is null
            }
            return EquationString + EntryString; // Equation string and Entry string both displayed on screen
        }
        public static void Entry(string buttonText)
        {
            // called when a digit is pressed
            if (EntryString == "Error")
            {
                Clear(); // clear if last action resulted in error
            }
            EntryString += buttonText; // add button text (digit) to Entry
        }
        public static void MathOperator(string mathOperator)
        {
            if (EntryString == "Error")
            {
                Clear();
            }
            
            if (EntryString != null)
            {
                EquationString += EntryString;
                EquationString += mathOperator;
                Solver.AddEntryToCalculatable(EntryString);
                Solver.AddCalculatableOperator(mathOperator);
                EntryString = null;
            }
            
        }
        public static void HandlePercent()
        {
            string entry = $"({EntryString}*0.01)";
            Solver.AddEntryToCalculatable(entry);
            EquationString += EntryString;
            EquationString += "%";
            EntryString = null;
        }
        public static void HandleBracket()
        {
            // called if the bracket button is pressed
            if (EntryString == "Error")
            {
                Clear(); // clear if last action resulted in error
            }
            if (EntryString != null && (BracketOpen == false))
            {
                // if there is a value in Entry string, and brackets are not currently open
                EquationString += EntryString; // Add entry string to Equation string
                Solver.AddEntryToCalculatable(EntryString); // add entry string to calculatable
            }
            if (EntryString == "(" && (BracketOpen == true))
            {
                // if bracket is open, and Entry does not contain digits, just remove bracket
                EntryString = null;
                BracketOpen = false;
                return;
            }

            if (BracketOpen == false) // if bracket is not yet open
            {
                EntryString = "("; // add an open bracket to EntryString
                BracketOpen = true; // change bool so we know bracket is open
            }
            else // if bracket IS already open
            {
                EntryString += ")"; // Add a closing bracket to Entry
                BracketOpen = false; // change bool so we know its closed
                //EquationString += EntryString; // add the Entry to the Equation string
                //Solver.AddEntryToCalculatable(EntryString); // add entry to calculatable
                //EntryString = null; // clear Entry string
            }
        }
        public static void HandleNegative()
        {
            // called when negative button is pressed
            if (double.TryParse(EntryString, out double temp))
            {
                // if entry can be parsed into a double
                double negative = temp* -1; // multiply by -1 (This toggles between neg and pos)
                EntryString = negative.ToString(); // return result as string
            }
        }
        public static void Clear()
        {
            // when called, resets all values to starting value (Does not reset memory keys)
            BracketOpen = false;
            EntryString = null;
            EquationString = null;
            Solver.Calculatable = null;
        }
        public static void SendToSolver()
        {
            if (EquationString != null) // check if equation string is null
            {
                if (EntryString != null)  // check if Entry is null
                {
                    Solver.AddEntryToCalculatable(EntryString); // add Entry to calculatable
                }
                string? result = Solver.Solve(); // Solve calculatable and store result
                Input.Clear(); // Clear (Reset all values
                Input.EntryString = result; // set Entry string to result from Solver.
            }
            
        }
    }
}
