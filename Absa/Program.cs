using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("error, vole.");
                return;
            }

            (int seats, List<List<int>> listOfLists) = ParseString(input);

            //var inputs = input.Split(new char[] { ']', ' ', ',', ':', ';', '-', '|' }, StringSplitOptions.RemoveEmptyEntries);

            //if (!int.TryParse(inputs[0], out int number))
            //{
            //    Console.WriteLine("error, vole.");
            //    return;
            //}

            //List<List<int>> lists = new List<List<int>>();
            //List<int> numbers = new();
            //for (int i = 0; i < inputs.Length; i++)
            //{
            //    if (inputs[i].StartsWith("["))
            //    {
            //        lists.Add(numbers);
            //        numbers = new List<int>
            //    {
            //        Convert.ToInt32(inputs[i].Substring(1))
            //    };
            //    }
            //    else if (int.TryParse(inputs[i], out int requestSeat))
            //    { numbers.Add(requestSeat); }
            //}
            //lists.Add(numbers);
            //lists.RemoveAt(0);

            if (listOfLists.Count > seats)
            {
                Console.WriteLine("No");
                continue;
            }

            if (CanPickUniqueNumbers(listOfLists, 0, listOfLists.Count, new List<int>()))
            { Console.WriteLine("Yes"); }
            else
            { Console.WriteLine("No"); }
            
            Console.ReadLine();
        }
    }

    static (int, List<List<int>>) ParseString(string input)
    {
        int number = Convert.ToInt32(Regex.Match(input, @"\d+").Value);
        var matches = Regex.Matches(input, @"\[(.*?)\]");
        var listOfLists = new List<List<int>>();
        foreach (Match match in matches)
        {
            listOfLists.Add(ParseList(match.Groups[1].Value).Select(m => Convert.ToInt32(m)).ToList());//.Split(',').Select(m => Convert.ToInt32(m)).ToHashSet();            
        }
        return (number, listOfLists);
    }

    static List<string> ParseList(string input)
    {
        List<string> list = new List<string>();
        foreach (Match match in Regex.Matches(input, @"\d+"))
        {
            list.Add(match.Value);
        }
        return list;
    }

    static bool CanPickUniqueNumbers(List<List<int>> lists, int index, int max, List<int> pickedNumbers)
    {
        if (index > max - 1)
        { return true; ; }

        foreach (var num in lists[index])
        {
            if (!pickedNumbers.Contains(num))
            {
                pickedNumbers.Add(num);
                if (CanPickUniqueNumbers(lists, index + 1, max, pickedNumbers))
                {
                    return true;
                }
                pickedNumbers.Remove(num);
            }
        }
        return false;
    }
}