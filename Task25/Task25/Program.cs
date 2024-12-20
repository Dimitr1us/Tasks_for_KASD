using MyHashSetLib;
MyHashSet<string> set = new MyHashSet<string>();
string filePath = "input.txt"; // Укажите путь к вашему файлу

using (StreamReader reader = new StreamReader(filePath))
{
    string line;

    
    while ((line = reader.ReadLine()) != null)
    {
        set.add(line);
    }
}
Console.WriteLine(set.first());
