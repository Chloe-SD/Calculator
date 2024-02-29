using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;


namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        // Variables for memory storage
        public string? Memory1 {  get; set; }
        public string? Memory2 { get; set; }
        public string? Memory3 { get; set; }
        // bool for setting memory storage (true when "set" pressed)
        public bool setMemory = false;

        public MainPage()
        {          
            InitializeComponent();
        }
        private void UpdateDisplay()
        {
            // updates the human readable display on the calculator, this is called after basically ever button press.
            display.Text = Input.UpdateReadout();

            // The following was used for debugging, disregard
            //if (Solver.Calculatable != null)
            //{
            //    calc.Text = Solver.Calculatable;
            //    ent.Text = Input.EntryString;
            //    eq.Text = Input.EquationString;
            //}
        }
        private void acBtn_Clicked(object sender, EventArgs e)
        {
            Input.Clear();  // Calls function to clear eveything
            UpdateDisplay();
        }
        private void negativeBtn_Clicked(object sender, EventArgs e)
        {
            // When negative button pressed, the current entry number will toggle between positive and negative.
            Input.HandleNegative();
            UpdateDisplay();
        }
        private void operator_Clicked(object sender, EventArgs e)
        {
            // called when any math operator (+ - etc) is clicked.
            Button button = (Button)sender; 
            string mathOperator = button.Text; // gets the text of whichever button was clicked
            Input.MathOperator(mathOperator); // sends the button text to MathOperator function
            UpdateDisplay();           
        }
        private void equalBtn_Clicked(object sender, EventArgs e)
        {
            Input.SendToSolver(); // sends the current equation to the solver function
            UpdateDisplay();
        }
        private void number_Clicked(object sender, EventArgs e)
        {
            // called when any number button is clicked
            Button button = (Button)sender;
            string buttonText = button.Text; // get the text of the button clicked
            Input.Entry(buttonText); // send the button text to the Entry method
            UpdateDisplay();
        }
        private void PercentBtn_Clicked(object sender, EventArgs e)
        {
            Input.HandlePercent(); // calls the HandlePercent method
            UpdateDisplay();
        }
        private void bracketBtn_Clicked(object sender, EventArgs e)
        {
            Input.HandleBracket(); // calls the HandleBracket method
            UpdateDisplay();
        }
        private void setMemoryBtn_Clicked(object sender, EventArgs e)
        {
            if (Input.EntryString == "Error")
            {
                Input.Clear(); // Clears if last action resulted in error
            }
            setMemory = true; // change bool to true so memory can be set.
        }
        private void memoryBtn_Clicked(object sender, EventArgs e)
        {
            if (Input.EntryString == "Error")
            {
                Input.Clear(); // cleear if last action resulted in error
            }
            Button button = (Button)sender;
            string buttonText = button.Text; // get the text of the button clicked (M1, M2, M3)

            if (setMemory) // if setMemory bool is true, this means we are setting a new value to the memory button clicked
            {
                if (buttonText == "M1") // If button was M1
                {
                    Memory1 = Input.EntryString; // set variable for memory 1 to the current value on screen
                    display.Text = $"M1 set: {Input.EntryString}"; // show confirmation message                 
                }
                else if (buttonText == "M2") // same for M2
                {
                    Memory2 = Input.EntryString;
                    display.Text = $"M2 set: {Input.EntryString}";
                }
                else // same for M3
                {
                    Memory3 = Input.EntryString;
                    display.Text = $"M3 set: {Input.EntryString}";
                }
                setMemory = false; // change setMemory bool to false
                return; // to keep from executing the code below
            }
            // the below code is for recalling stored values when the memory vuttons are clicked.
            if (buttonText == "M1" && Memory1 != null) // button pressed is M1, does not run if no value is stored
            {
                if (Input.EntryString == "0") //check if current entry value is 0
                {
                    Input.EntryString = Memory1; // replace 0 with M1 stored value
                }
                else // if current entry is not 0 (ex: 2+2)
                {
                    Input.EntryString += Memory1; // append the stored value onto the current entry
                    // (Example: Memory stored value is set to 7, screen would now show 2+27)
                }
                
            }
            else if (buttonText == "M2" && Memory2 != null) //same for M2
            {
                if(Input.EntryString == "0")
                {
                    Input.EntryString = Memory2;
                }
                else
                {
                    Input.EntryString += Memory2;
                }
            }
            else if (buttonText == "M3" && Memory3 != null) // same for M3
            {
                if (Input.EntryString == "0")
                {
                    Input.EntryString = Memory3;
                }
                else
                {
                    Input.EntryString += Memory3;
                }
            }
            UpdateDisplay();           
        }

        //Keyboard functionality - for tyrping into the calculator from the keyboard
        // NOTE: an entry was created in the GUI for this to work. The entry is transparent.
        private void KeyboardEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //called anytime a keyboard key is pressed (besides ENTER)
            string input = e.NewTextValue; // get the value of the key pressed
            if (input != null) // ensure its not null
            {
                KeyboardInput.HandleKeystroke(input); // call method to determine what to do based on the key pressed
                UpdateDisplay();
                keyboardEntry.Text = null; // erase the entry (Can only ever be 1 character)
            }
        }

        private void keyboardEntry_Completed(object sender, EventArgs e)
        {
            //called when the ENTER key is pressed on a keyboard
            Input.SendToSolver(); // sends current equation to the solver method
            UpdateDisplay();
        }
    }
    
}
