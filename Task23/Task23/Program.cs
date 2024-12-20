using MyHashSetLib;
MyHashSet<int> myHashSet = new MyHashSet<int>();
myHashSet.add(1, 2);
myHashSet.add(3, 4);
myHashSet.add(5, 6);
myHashSet.add(6, 7);
myHashSet.remove(1);
Console.WriteLine(myHashSet.first());
