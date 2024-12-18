using MyHashMapLib;
using System;
using System.Collections.Generic;
string[] Work(string str)
{
    string[] array = str.Split(' ');
    array[3]= array[3].Remove(array[3].Length - 1);
    string []array1= { array[0], array[1],array[3]};
    return array1;
}
//array[0] - тип, array[1] - имя, array[2] - значение
MyHashMap<string, (string, int)> map = new MyHashMap<string, (string, int)>();

string filePath = "input.txt";

// Используем блок using для автоматического закрытия StreamReader
using (StreamReader reader = new StreamReader(filePath))
{
    string line;
    string[] array;
    // Читаем файл построчно
    while ((line = reader.ReadLine()) != null)
    {
        array = Work(line);
        if (array[0] =="float" || array[0]=="int"|| array[0]=="double")
        {
            if (map.containsKey(array[1]) == false)
            {
                map.Put(array[1], (array[0], Convert.ToInt32(array[2])));
            }
            else
            {
                Console.WriteLine($"Переменная {array[1]} переопределена.");
            }
        }
        else
        {
            Console.WriteLine("Недопустимый формат данных.");
        }
    }
}

string file = "output.txt";

var everything = map.entrySet();
using (StreamWriter writer = new StreamWriter(file,false))
{
    foreach (var element in everything)
    {
        writer.WriteLine($"{element.Value.Item1} => {element.Key}({element.Value.Item2})");
    }
}