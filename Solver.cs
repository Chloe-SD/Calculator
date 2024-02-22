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
        public static string? Calculatable {  get; set; }
        public static void AddEntryToCalculatable(string entry)
        {
            if (Calculatable != null && entry != null)
            {
                int lastIndex = Calculatable.Length-1 ;
                List <char> mathOperators = new List <char>(['+','-','*','/']);
                if (entry[0] == '(' || Calculatable[lastIndex] == ')')
                {
                    if (!mathOperators.Contains(Calculatable[lastIndex]))
                    {
                        Calculatable += "*";
                    }
                    
                }
            }
            Calculatable += entry;
        }
        public static string Solve()
        {
            if (Calculatable != null)
            {
                try
                {
                    DataTable table = new DataTable();
                    var result = table.Compute(Calculatable, "");
                    Calculatable = null;
                    if (result.ToString() == "∞")
                    {
                        return "Error";
                    }
                    string FormattedResult = FormatResult(Convert.ToDecimal(result));
                    return FormattedResult; 
                }
                catch (Exception)
                {
                    // Handle any errors that may occur during evaluation
                    Calculatable = null;
                    return "Error";
                }
            }
            else { return "Error"; }
        }
        private static string FormatResult(decimal result)
        {
            string formattedResult;
            string resultString = result.ToString("#0.##########");
            int decimalIndex = resultString.IndexOf('.');
            int preDecimalDigits = ((long)result).ToString().Length;
            if (decimalIndex > 13 || preDecimalDigits > 13)
            {
                // Error if int part of result is too long for the screen
                return "Error";
            }
            else
            {
                int maxDigits = 13 - decimalIndex;
                decimal roundedResult = Math.Round(result, maxDigits);
                formattedResult = roundedResult.ToString();
            }
            return formattedResult;
        }  
        public static void AddCalculatableOperator(string mathOperator)
        {

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
