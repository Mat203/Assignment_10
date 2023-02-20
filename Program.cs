string Input()
{
    Console.Write("Write what you want to calculate:");
    return Console.ReadLine()!;
}

bool IsNumberToken(string token)
{
    return int.TryParse(token, out _);
}

bool IsOperatorToken(string token)
{
    return token is "+" or "-" or "*" or "/" or "^";
}

bool IsLeftBracketToken(string token)
{
    return token == "(";
}

bool IsRightBracketToken(string token)
{
    return token == ")";
}

bool IsLeftAssociative(string token)
{
    return IsOperatorToken(token) && token != "^";
}

int Precedance(string token)
{
    switch (token)
    {
        case "+":
        case "-":
            return 2;
        case "*":
        case "/":
            return 3;
        case "^":
            return 4;
        default: // all other
            return 0;
    }

}

List<string> ToReverse(List<string> tokens)
{
    Queue<string> output = new Queue<string>();
    Stack<string> operators = new Stack<string>();
    foreach (string token in tokens)
    {
        if (IsNumberToken(token))
        {
            output.Enqueue(token);
        }
        else if (IsOperatorToken(token))
        {
            while (
                operators.Count != 0 &&
                !IsLeftBracketToken(operators.Peek()) &&
                (
                    Precedance(operators.Peek()) > Precedance(token) ||
                    (
                        Precedance(operators.Peek()) == Precedance(token) &&
                        IsLeftAssociative(token)
                    )
                )
                )
            {
                output.Enqueue(operators.Pop());
            }

            operators.Push(token);
        }
        else if (IsLeftBracketToken(token))
        {
            operators.Push(token);
        }
        else if (IsRightBracketToken(token))
        {
            while (!IsLeftBracketToken(operators.Peek()))
            {
                if (operators.Count == 0)
                {
                    throw new Exception("Error: Mismatched parentheses");
                }

                output.Enqueue(operators.Pop());
            }

            if (!IsLeftBracketToken(operators.Peek()))
            {
                throw new Exception("No left bracket at he operators stack");
            }

            operators.Pop();
        }
    }

    while (operators.Count > 0)
    {
        if (IsLeftBracketToken(operators.Peek()))
        {
            throw new Exception("Error: Mismatched parentheses");
        }

        output.Enqueue(operators.Pop());
    }

    return output.ToList();
}


List<string> Tokenize(string r)
{
    string buffer = "";
    List<string> result = new List<string>();

    foreach (Char s in r)
    { 
        if (Char.IsNumber(s))
        {
            buffer += s;
        }
        else if (s is '+' or '-' or '*' or '/' or '(' or ')' or '^')
        {
            if (buffer.Length > 0)
            {
                result.Add(buffer);
                buffer = "";
            }

            result.Add(item: s.ToString());
        }
    }

    if (buffer.Length > 0)
    {
        result.Add(buffer);
    }

    return result;
}

Stack<double> Calculate(List<string> tokens)
{
    Stack<double> s = new Stack<double>();


    foreach (string token in tokens)
    {
        if (double.TryParse(token, out double number))
        {
            s.Push(number);
        }
        else
        {
            double num2 = s.Pop();
            double num1 = s.Pop();
            double result = 0;
            switch (token)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    result = num1 / num2;
                    break;
                case "^":
                    result = Math.Pow(num1, num2);
                    break;
            }
            s.Push(result);
        }
    }

    if (s.Count == 1) 
    { 
        Console.WriteLine("Результат: " + s.Pop());
    }

    return s;
}
    
string input = Input();
List <string> tokens = Tokenize(input);
List<string> tpnTokens = ToReverse(tokens);
Stack<double> result = Calculate(tpnTokens);
//Console.WriteLine(String.Join("",tpnTokens));

