using MyHashMapLib;
using System;
using System.Text.RegularExpressions;

static string ProcessString(string input)
    {
        // Регулярное выражение для проверки условий
        string pattern = @"^<(/?)(.+?)>$"; // Проверка на открывающую и закрывающую скобки

        Match match = Regex.Match(input, pattern);
        if (match.Success)
        {
            // Проверяем, что второй символ равен слэшу, если он есть
            if (match.Groups[1].Value == "/" || match.Groups[1].Value == "")
            {
                // Возвращаем значение без скобок и слэша
                return match.Groups[2].Value.Trim(); // Убираем пробелы по краям
            }
        }
        return null; // Возвращаем null, если не подходит под условия
    }
MyHashMap<string,string> map = new MyHashMap<string,string>();
string filePath = "input.txt"; // Укажите путь к вашему файлу

// Используем блок using для автоматического закрытия StreamReader
using (StreamReader reader = new StreamReader(filePath))
{
    string line;

    // Читаем файл построчно
    while ((line = reader.ReadLine()) != null)
    {
        if (ProcessString(line) != null)
        {
            line = ProcessString(line);
            line = line.ToLower();
            if (map.containsValue(line) == false)
            {
                map.Put(line, line);
            }
        }
    }
}
Console.WriteLine(map.keySet().Count);