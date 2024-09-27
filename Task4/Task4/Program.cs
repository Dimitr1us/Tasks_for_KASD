using MyArrayListLibrary;
Console.WriteLine("авааав");
string[] array = { "a","b","c"};
MyArrayList <string> myArrayList = new MyArrayList<string> (array);
Console.WriteLine($"{myArrayList.contains("a")}");
