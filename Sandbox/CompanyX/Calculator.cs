using Bridge.Html5;
using Bridge.CLR;
using System;

namespace Demo
{
    [FileName("Calculator")]
    public static class Calculator
    {
        public static void OnReady()
        {
            Document.AddEventListener("DOMContentLoaded", Init, false);
        }

        public static void Init()
        {
            // http://thecodeplayer.com/walkthrough/javascript-css3-calculator

            // Get all the keys from document
            var keys = Document.QuerySelectorAll("#calculator button");

            // TODO: issue with array emitting
            //var operators = new string[4] { "+", "-", "x", "÷" };
            var operators = Script.Write<string>("['+', '-', 'x', '÷']");
            
            var decimalAdded = false;

            // Add onclick event to all the keys and perform operations
            for (var i = 0; i < keys.Length; i++)
            {
                // TODO: Emit issue with .As<T>() Generic Extension Method
                //keys[i].As<SpanElement>().OnClick = (Event e) =>

                keys[i].OnClick = (Event e) =>
                {
                    // Get the input and button values
                    var input = Document.QuerySelector(".panel-body h3");
                    var inputVal = input.InnerHTML;
                    
                    // TODO: How to use 'this' keyword?
                    var btnVal = Script.Write<string>("this.innerHTML");

                    // Now, just append the key values (btnValue) to the input string and finally use javascript's eval function to get the result
                    // If clear key is pressed, erase everything
                    if (btnVal == "C")
                    {
                        input.InnerHTML = "";
                        decimalAdded = false;
                    }

                    // If eval key is pressed, calculate and display the result
                    else if (btnVal == "=") 
                    {
                        var equation = inputVal;
                        var lastChar = equation.Substr(equation.Length - 1, 1);

                        // Replace all instances of x and ÷ with * and / respectively. 
                        // This can be done easily using regex and the 'g' tag which will replace all instances of the matched character/substring
                        equation = equation.Replace("/x/g", "*").Replace("/÷/g", "/");

                        // Final thing left to do is checking the last character of the equation. If it's an operator or a decimal, remove it
                        if (operators.IndexOf(lastChar) > -1 || lastChar == ".")
                        {
                            equation = equation.Replace("/.$/", "");
                        }

                        if (equation != null)
                        {
                            input.InnerHTML = Window.Eval<string>(equation);
                        }

                        decimalAdded = false;
                    }

                    // Basic functionality of the calculator is complete. But there are some problems like 
                    // 1. No two operators should be added consecutively.
                    // 2. The equation shouldn't start from an operator except minus
                    // 3. not more than 1 decimal should be there in a number

                    // We'll fix these issues using some simple checks
                    else if (operators.IndexOf(btnVal) > -1)
                    {
                        // Operator is clicked
                        // Get the last character from the equation
                        var lastChar = inputVal.Substr(inputVal.Length - 1);

                        // Only add operator if input is not empty and there is no operator at the last
                        if (inputVal != "" && operators.IndexOf(lastChar) == -1)
                        {
                            input.InnerHTML += btnVal;
                        }
                        // Allow minus if the string is empty
                        else if (inputVal == "" && btnVal == "-")
                        {
                            input.InnerHTML += btnVal;
                        }

                        // Replace the last operator (if exists) with the newly pressed operator
                        if (operators.IndexOf(lastChar) > -1 && inputVal.Length > 1)
                        {
                            // Here, '.' matches any character while $ denotes the end of string, so anything 
                            // (will be an operator in this case) at the end of string will get replaced by new operator
                            input.InnerHTML = inputVal.Replace("/.$/", btnVal);
                        }

                        decimalAdded = false;
                    }

                    // Now only the decimal problem is left. We can solve it easily using a flag 'decimalAdded' 
                    // which we'll set once the decimal is added and prevent more decimals to be added once it's set. 
                    // It will be reset when an operator, eval or clear key is pressed.
                    else if (btnVal == ".")
                    {
                        if (!decimalAdded)
                        {
                            input.InnerHTML += btnVal;
                            decimalAdded = true;
                        }
                    }

                    // if any other key is pressed, just append it
                    else
                    {
                        input.InnerHTML += btnVal;
                    }

                    // prevent page jumps
                    e.PreventDefault();
                };
            }
        }
    }
}