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

ArrayList Tokenize(string r)
{
    string buffer = "";
    ArrayList result = new ArrayList();

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

            result.Add(s.ToString());
        }
    }

    if (buffer.Length > 0)
    {
        result.Add(buffer);
    }

    return result;
}

Queue ToReverse(ArrayList tokens)
{
    Queue output = new Queue();
    Stack operators = new Stack();
    for (int i = 0; i < tokens.Count(); i++)
    {
        string token = tokens.GetAt(i);
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

    return output;
}


void Calculate(Queue tokens)
{
    Stack s = new Stack();

    for (int i = 0; i < tokens.Count(); i++)
    {
        string token = tokens.GetAt(i);
    
        if (double.TryParse(token, out double number))
        {
            s.Push(number.ToString());
        }
        else
        {
            double num2 = Convert.ToDouble(s.Pop());
            double num1 = Convert.ToDouble(s.Pop());
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
            s.Push(result.ToString());
        }
    }
    Console.Write("Result: ");
    Console.Write(s.Pop());
}

string input = Input();
ArrayList tokens = Tokenize(input);
Queue tpnTokens = ToReverse(tokens);
Calculate(tpnTokens);


public class Stack
    {
        private const int Capacity = 50;

        private string[] _array = new string[Capacity];

        private int _pointer;

        public void Push(string value)
        {
            if (_pointer == _array.Length)
            {
                throw new Exception("Stack overflowed");
            }

            _array[_pointer] = value;
            _pointer++;
        }
        public string Pop()
        {
            if (_pointer == 0)
            {
                throw new InvalidOperationException("Стек порожній");
            }

            _pointer--; 
            string item = _array[_pointer]; 

            return item;
        }
        public string Peek()
        {
            if (_pointer == 0)
            {
                throw new InvalidOperationException("Стек порожній");
            }

        return _array[_pointer - 1]; 
        }
        public int Count
        {
            get { return _pointer; }
        }
    }
public class ArrayList
    {
        private string[] _array = new string[10];

        private int _pointer = 0;

        public void Add(string element)
        {
            _array[_pointer] = element;
            _pointer += 1;

            if (_pointer == _array.Length)
            {
                var extendedArray = new string[_array.Length * 2];
                for (var i = 0; i < _array.Length; i++)
                {
                    extendedArray[i] = _array[i];
                }

                _array = extendedArray;
            }
        }
        public string GetAt(int index)
        {
            return _array[index];
        }

        public void Print()
        {
                for (var i = 0; i < _array.Length; i++)
                {
                    Console.WriteLine(_array[i]);
                }
        }

        public void Remove(string element)
        {
            for (var i = 0; i < _pointer; i++)
            {
                if (_array[i] == element)
                {
                    for (var j = i; j < _pointer - 1; j++)
                    {
                        _array[j] = _array[j + 1];
                    }

                    _pointer -= 1;
                    return;
                }
            }
        }

        public int Count()
        {
            return _pointer;
        }
    }

public class Queue
{
    private ArrayList _items = new ArrayList();

    public void Enqueue(string item)
    {
        _items.Add(item);
    }

    public string GetAt(int index)
    {
        return _items.GetAt(index);
    }
    public int Count ()
    {
        return _items.Count();
    }
    public string Peek()
    {
        if (_items.Count() == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        return _items.GetAt(0);
    }
}
