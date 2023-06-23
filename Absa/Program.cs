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

            (int seats, List<List<int>> listOfLists)  = ParseString(input);

            if (listOfLists.Count > seats)
            {
                Console.WriteLine("No");
                continue;
            }

            if (CanPickUniqueNumbers(listOfLists, 0, listOfLists.Count, new HashSet<int>()))
            { Console.WriteLine("Yes"); }
            else
            { Console.WriteLine("No"); }
            
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Parses input like *number*[*number*,*number*] to one int and list of lists of ints
    /// </summary>
    /// <param name="input">input string to be parsed</param>
    /// <returns>tupple of int and List of Lists of ints</returns>
    static (int, List<List<int>>) ParseString(string input)
    {
        int number = Convert.ToInt32(Regex.Match(input, @"\d+").Value);
        var matches = Regex.Matches(input, @"\[(.*?)\]");
        var listOfLists = new List<List<int>>();
        foreach (Match match in matches)
        {
            listOfLists.Add(ParseList(match.Groups[1].Value).Select(m => Convert.ToInt32(m)).ToList());     
        }
        return (number, listOfLists);
    }

    /// <summary>
    /// Parses input string containing numbers to List of strings that represents those numbers
    /// </summary>
    /// <param name="input">input string to be parsed</param>
    /// <returns>List of strings containing numbers only</returns>
    static List<string> ParseList(string input)
    {
        List<string> list = new List<string>();
        foreach (Match match in Regex.Matches(input, @"\d+"))
        {
            list.Add(match.Value);
        }
        return list;
    }

    /// <summary>
    /// Scan lists if it is possible to pick up exactly one random number can be picked from every one of them
    /// </summary>
    /// <param name="lists">List of Lists of ints</param>
    /// <param name="index">Should be 0 for start. Exists for recursion calling</param>
    /// <param name="max">number of Lists of ints in lists property</param>
    /// <param name="pickedNumbers">Should by new HashSet of ints as input. HashSet of ints for saving testing results for recursion calling</param>
    /// <returns>bool: Is it possible to pick up exactly one random number can be picked from every List of ints?</returns>
    static bool CanPickUniqueNumbers(List<List<int>> lists, int index, int max, HashSet<int> pickedNumbers)
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