string Input()
{
    Console.Write(">");
    return Console.ReadLine();
}

bool IsNumberToken(string token)
{
    return int.TryParse(token, out _);
}

bool IsOperatorToken(string token)
{
    return token is "+" or "-" or "/" or "*" or "^";
}

bool IsLeftBracketToken(string token)
{
    return token == "(";
}

bool IsRightBracketToken(string token)
{
    return token == ")";
}

int Precedence(string token)
{
    if (token == "+" || token == "-")
    {
        return 2;
    }
    if (token == "*" || token == "/")
    {
        return 3;
    }
    if (token == "^")
    {
        return 4;
    }
}
List<string> Tokenize(string r)
    {
        string buffer = "";
        List<string> result = new List<string>();

        foreach (var s in r)
        { 
            if (Char.IsNumber(s))
            {
                buffer += s;
            }
            else if (s == '+' || s == '-' || s == '*' || s == '/' || s == '(' || s == ')' || s == '^')
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

List<string> ToPRN(List<string> tokens)
{
    Queue<string> output = new Queue<string>();
    Stack<string> operators = new Stack<string>();

    foreach (string token in tokens)
    {
        if (IsNumberToken(token))
        {
            output.Enqueue(token);
        }
    }
}

string input = Input();
List <string> tokens = Tokenize(input);
Console.WriteLine(String.Join("0",tokens));