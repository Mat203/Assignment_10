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


string input = Input();
List <string> tokens = Tokenize(input);
Console.WriteLine(String.Join("0",tokens));