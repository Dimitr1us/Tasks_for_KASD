using MyTreeMapLib;


/*interface Comparator<G> where G : IComparable<G>
{
    int CompareTo(G x, G y);
} */

compare<int> Compare=new compare<int>();
MyTreeMap<int,int> map = new MyTreeMap<int,int>();
map.put(1, 2);
Console.WriteLine(map.get(1));

   class compare<G> : Comparator<G> where G: IComparable<G>
{
    public int CompareTo(G x, G y)
    {
        return x.CompareTo(y);
    }
}