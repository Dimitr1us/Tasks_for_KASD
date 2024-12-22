using System.Text.RegularExpressions;
using MyHashSetLib;
static List<string> ExtractWords(string input)
{
    var words = new List<string>();
    var matches = Regex.Matches(input, @"\b[a-zA-Z]+\b");

    foreach (Match match in matches)
    {
        words.Add(match.Value);
    }

    return words;
}

MyHashSet<string> myHashSet = new MyHashSet<string>();

string filePath = "input.txt"; 

// Используем блок using для автоматического закрытия StreamReader
using (StreamReader reader = new StreamReader(filePath))
{
    string line;

    while ((line = reader.ReadLine()) != null)
    {
        var list = ExtractWords(line);
        foreach (string match in list)
        {
            if (myHashSet.contains(match.ToLower()) == false) myHashSet.add(match.ToLower(), null);
        }
    }
}

Console.WriteLine(myHashSet.Size());