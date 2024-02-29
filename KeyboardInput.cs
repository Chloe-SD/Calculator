using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    static class KeyboardInput
    {
        // Logic for handling keyboard input
        public static void HandleKeystroke(string input)
        {
            // called when any key (besides ENTER) is pressed
            if (IsDigit(input)) // determine os key is a digit
            {
                Input.Entry(input); // if so, enter the digit
            }
            else if (IsOperator(input)) // determine is key is a Math Operator
            {
                Input.MathOperator(input); // if so, begin the logic for math operators
            }
            else if (IsBracket(input)) // determine if key is ( or )
            {
                Input.HandleBracket(); // call logic for brackets
            }
            else if (input == "%")
            {
                Input.HandlePercent(); // call % logic
            }
            else if (input == "=")
            {
                Input.SendToSolver(); // send equation to solver
            }
            // if the key was any other type that is no defined here, nothing happens
        }
        static bool IsDigit(string input)
        {
            // determins if key is a digit by trying to pase the key text to an int
            if (int.TryParse(input, out _))
            {
                return true;
            }
            return false;
        }
        static bool IsOperator(string input)
        {
            // compares key to list of operators to determin if its an operator
            List<string> operators = new List<string>(["+","-","÷","×"]);
            if (operators.Contains(input))
            {
                return true;
            }
            return false;
        }
        static bool IsBracket(string input)
        {
            // determine if key is a bracket
            if (input == "(" || input == ")")
            {
                return true;
            }
            return false;
        }
    }
}
