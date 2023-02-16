string Input()
{
    Console.Write(">");
    return Console.ReadLine();
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