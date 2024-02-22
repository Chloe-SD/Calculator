using System.Collections;
using System.Data;


namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        public string? Memory1 {  get; set; }
        public string? Memory2 { get; set; }
        public string? Memory3 { get; set; }
        public bool setMemory = false;
        public MainPage()
        {          
            InitializeComponent();
        }
        private void UpdateDisplay()
        {
            display.Text = Input.UpdateReadout();
            //if (Solver.Calculatable != null)
            //{
            //    calc.Text = Solver.Calculatable;
            //    ent.Text = Input.EntryString;
            //    eq.Text = Input.EquationString;
            //}
        }
        private void acBtn_Clicked(object sender, EventArgs e)
        {
            Input.Clear();  
            UpdateDisplay();
        }
        private void negativeBtn_Clicked(object sender, EventArgs e)
        {
            Input.HandleNegative();
            UpdateDisplay();
        }
        private void operator_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string mathOperator = button.Text;
            Input.MathOperator(mathOperator);
            UpdateDisplay();           
        }
        private void equalBtn_Clicked(object sender, EventArgs e)
        {
            Input.SendToSolver();
            UpdateDisplay();
        }
        private void number_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;
            Input.Entry(buttonText); 
            UpdateDisplay();
        }
        private void PercentBtn_Clicked(object sender, EventArgs e)
        {
            Input.HandlePercent();
            UpdateDisplay();
        }
        private void bracketBtn_Clicked(object sender, EventArgs e)
        {
            Input.HandleBracket();
            UpdateDisplay();
        }
        private void setMemoryBtn_Clicked(object sender, EventArgs e)
        {
            if (Input.EntryString == "Error")
            {
                Input.Clear();
            }
            setMemory = true;
        }
        private void memoryBtn_Clicked(object sender, EventArgs e)
        {
            if (Input.EntryString == "Error")
            {
                Input.Clear();
            }
            Button button = (Button)sender;
            string buttonText = button.Text;
            if (setMemory)
            {
                if (buttonText == "M1")
                {
                    Memory1 = Input.EntryString;
                    display.Text = $"M1 set: {Input.EntryString}";                  
                }
                else if (buttonText == "M2")
                {
                    Memory2 = Input.EntryString;
                    display.Text = $"M2 set: {Input.EntryString}";
                }
                else
                {
                    Memory3 = Input.EntryString;
                    display.Text = $"M3 set: {Input.EntryString}";
                }
                setMemory = false;
                return;
            }
            if (buttonText == "M1" && Memory1 != null)
            {
                if (Input.EntryString == "0") 
                {
                    Input.EntryString = Memory1;
                }
                else
                {
                    Input.EntryString += Memory1;
                }
                
            }
            else if (buttonText == "M2" && Memory2 != null)
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
            else if (buttonText == "M3" && Memory3 != null)
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
        
        
    }
    
}
