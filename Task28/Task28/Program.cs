using MyInterFaceLib;
using MyArrayListLibrary;
using MyVectorLibrary;

MyArrayList<int> list = new MyArrayList<int>();
list.add(0);
list.add(1);
list.add(2);
list.add(3);
MyVector<int> vector = new MyVector<int>();
vector.addAll(list);
Console.WriteLine(vector.firstElement());
