using MyIteratorLib;
using MyArrayListLibrary;
using MyVectorLibrary;
using MyLinkedListLib;
using MyPriorityQueueLib;
using MyArrayDequeLib;
using MyHashSetLib;
MyVector<string> queue = new MyVector<string>();

// Добавление элементов
queue.add("Первый");
queue.add("Второй");
queue.add("Третий");

iMyIterator<string> iterator = queue.ListIterator();
iterator.Set("Нулевой");
iterator.Add("Первый");
iterator.Next();
iterator.Next();
iterator.Remove();
iterator.Previous();
iterator.Previous();
// Проход по элементам с использованием итератора
Console.WriteLine("Элементы в списке:");
while (iterator.HasNext())
{
    string str=iterator.Next();
    if (str != null)
    {
        Console.WriteLine(str);
    }
}
