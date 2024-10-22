using MyStackLibrary;
using System.Collections.Generic;
using System.Windows.Markup;
using OperationsLibrary;
using System.Linq;
MyStack <string> Stack_of_operations = new MyStack<string> ();
MyStack<double> Stack_of_variables = new MyStack<double>();
int Priority(string str)
{
    switch (str)
    {
        case ("sin"):
        case ("cos"):
        case ("tg"):
        case ("ln"):
        case ("lg"):
        case ("exp"):
        case ("whl"):
        case ("abs"):
        case ("sqrt"):
        case ("^"):
            return 3;
        case ("min"):
        case ("max"):
        case ("/"):
        case ("div"):
        case ("mod"):
        case ("*"):
            return 2;
        case ("+"):
        case ("-"):
            return 1;
        default:
            return 0;
    }
}

void Process(string operation, ref MyStack<string> Stack_of_operations, ref MyStack<double> Stack_of_variables)
{
    switch(operation)
    {
        case ("sin"):
        {
            double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
            double result= UnaricOperations.sin(number1);
            Stack_of_variables.add(result);
            return;
        }
        case ("cos"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double result = UnaricOperations.cos(number1);
                Stack_of_variables.add(result);
                return;
            }
        case ("tg"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double result = UnaricOperations.tg(number1);
                Stack_of_variables.add(result);
                return;
            }
        case ("ln"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double result = UnaricOperations.ln(number1);
                Stack_of_variables.add(result);
                return;
            }
        case ("lg"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double result = UnaricOperations.lg(number1);
                Stack_of_variables.add(result);
                return;
            }
        case ("exp"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double result = UnaricOperations.exp(number1);
                Stack_of_variables.add(result);
                return;
            }
        case ("whl"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double result = UnaricOperations.whole(number1);
                Stack_of_variables.add(result);
                return;
            }
        case ("abs"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double result = UnaricOperations.abs(number1);
                Stack_of_variables.add(result);
                return;
            }
        case ("sqrt"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double result = UnaricOperations.sqrt(number1);
                Stack_of_variables.add(result);
                return;
            }
        case ("^"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double number2 = Stack_of_variables.pop();
                double result = BinaricOperations.pow(number2, number1);
                Stack_of_variables.add(result);
                return;
            }

        case ("min"):
            {
                double number1= Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double number2= Stack_of_variables.pop();
                double result = BinaricOperations.min(number1, number2);
                Stack_of_variables.add(result);
                return;
            }
        case ("max"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double number2 = Stack_of_variables.pop();
                double result = BinaricOperations.max(number1, number2);
                Stack_of_variables.add(result);
                return;
            }
        case ("/"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double number2 = Stack_of_variables.pop();
                if (number1 == 0)
                {
                    Console.WriteLine("На ноль делить нельзя.");
                    Environment.Exit(0);
                    return;
                }
                double result = number2 / number1;
                Stack_of_variables.add(result);
                return;
            }
        case ("div"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double number2 = Stack_of_variables.pop();
                double result = BinaricOperations.div(number2, number1);
                Stack_of_variables.add(result);
                return;
            }
        case ("mod"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double number2 = Stack_of_variables.pop();
                double result = BinaricOperations.mod(number2, number1);
                Stack_of_variables.add(result);
                return;
            }
        case ("*"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double number2 = Stack_of_variables.pop();
                double result = number2 * number1;
                Stack_of_variables.add(result);
                return;
            }
        case ("+"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double number2 = Stack_of_variables.pop();
                double result = number1 + number2;
                Stack_of_variables.add(result);
                return;
            }
        case ("-"):
            {
                double number1 = Stack_of_variables.pop();
                if (Stack_of_operations.empty() == false)
                {
                    if (Priority(Stack_of_operations.peek()) >= Priority(operation))
                    {
                        string operation2 = Stack_of_operations.pop();
                        Process(operation2, ref Stack_of_operations, ref Stack_of_variables);
                    }
                }
                double number2 = Stack_of_variables.pop();
                double result = number2-number1;
                Stack_of_variables.add(result);
                return;
            }
    }
}
/*
double Binaric(Func<double, double, double> func,double a,double b)
{
    try
    {
        return func(a, b);
    }
    catch (Exception e) {
        return default(double);
    }
}

double Unaric(Func<double,double> func,double a)
{
    try
    {
        return func(a);
    }
    catch (Exception e) {
        return default (double);
    }
}*/

var values = new Dictionary<string, double>();
string [] UnaricOperation = new string[9] { "sqrt", "abs", "sin", "cos", "tg", "ln", "lg", "exp","whl"} ;
string[] BinaricOperation = new string[9] { "min", "max", "*", "/", "div", "mod", "+", "-","^" };

Console.WriteLine("Введите математическое выражение.");
string expression = Console.ReadLine();
expression = expression.Replace(" ", "");
Console.WriteLine("Введите по одному значения переменных формата a=10. Каждая переменная должна называться одной буквой. Регистр не имеет значения.");

string str=Console.ReadLine();
if (str != "")
{
    string[] pairs = str.Split(' ');
    foreach (var pair in pairs)
    {
        var parts = pair.Split('=');
        string variable = parts[0][0].ToString();
        variable = variable.ToLower();
        double value = double.Parse(parts[1]);

        values[variable] = value;
    }
}
int i = 0;
while (i < expression.Length)
{
    if (expression[i] == '(')
    {
        Stack_of_operations.add("(");
        i++;
    }
    else if (Char.IsDigit(expression[i]))
    {
        string number = "";
        while (i < expression.Length && Char.IsDigit(expression[i]))
        {
            number = number + expression[i];
            i++;
        }
        Stack_of_variables.add(Convert.ToDouble(number));
    }
    else if (values.ContainsKey(expression[i].ToString()))
    {
        Stack_of_variables.add(values[expression[i].ToString()]);
        i++;
    }
    else if (expression[i] == ')')
    {
        string operation = Stack_of_operations.pop();
        while (operation != "(")
        {
            Process(operation, ref Stack_of_operations, ref Stack_of_variables);
            operation = Stack_of_operations.pop();
        }
        i++;
    }
    else
    {
        if (values.ContainsKey(expression[i].ToString()))
        {
            Stack_of_variables.add(values[expression[i].ToString()]);
            i++;
        }
        else
        {
            string operation = expression[i].ToString();
            bool IsWorked = false;
            while (operation.Length <= 4)
            {
                if (UnaricOperation.Contains(operation) || BinaricOperation.Contains(operation))
                {
                    IsWorked = true;
                    Stack_of_operations.add(operation);
                    break;
                }
                i++;
                operation = operation + expression[i].ToString();
            }
            i++;
            if (IsWorked == false)
            {

                Console.WriteLine("В выражении есть лишние символы.");
                Environment.Exit(0);
            }
        }
    }
}
while (Stack_of_operations.empty() == false)
{
    string operation = Stack_of_operations.pop();
    Process(operation, ref Stack_of_operations, ref Stack_of_variables);
}
double result = Stack_of_variables.pop();
Console.WriteLine(result);


