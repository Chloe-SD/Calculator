using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calculator
{
    static class Input
    {
        public static string? EntryString;
        public static string? EquationString { get; set; }
        public static bool BracketOpen = false;


        public static string UpdateReadout()
        {
            if (EntryString == null)
            {
                if (EquationString == null)
                {
                    return "0";
                }
                return EquationString;
            }
            return EquationString + EntryString;
        }
        public static void Entry(string buttonText)
        {
            if (EntryString == "Error")
            {
                Clear();
            }
            EntryString += buttonText;
        }
        public static void MathOperator(string mathOperator)
        {
            if (EntryString == "Error")
            {
                Clear();
            }
            if (EntryString != "0")
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
            if (EntryString == "Error")
            {
                Clear();
            }
            if (EntryString != null && (BracketOpen == false))
            {
                EquationString += EntryString;
                Solver.AddEntryToCalculatable(EntryString);
            }

            if (BracketOpen == false)
            {
                EntryString = "(";
                BracketOpen = true;
            }
            else
            {
                EntryString += ")";
                BracketOpen = false;
                EquationString += EntryString;
                Solver.AddEntryToCalculatable(EntryString);
                EntryString = null;
            }
        }
        public static void HandleNegative()
        {
            if (double.TryParse(EntryString, out double temp))
            {
                double negative = temp* -1;
                EntryString = negative.ToString();
            }
        }
        public static void Clear()
        {
            BracketOpen = false;
            EntryString = null;
            EquationString = null;
            Solver.Calculatable = null;
        }
        public static void SendToSolver()
        {
            if (EquationString != null)
            {
                if (EntryString != null)
                {
                    Solver.AddEntryToCalculatable(EntryString);
                }
                string? result = Solver.Solve();
                Input.Clear();
                Input.EntryString = result;
            }
            
        }
    }
}
