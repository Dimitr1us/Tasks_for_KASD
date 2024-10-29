using MyArrayDequeLib;

int NumberOfDigits(string str)
{
    int result = 0;
    for (int i=0; i < str.Length; i++)
    {
        if (Char.IsDigit(str[i])==true) result++;
    }
    return result;
}

int NumberOfSpaces(string str)
{
    int result = 0;
    for (int i = 0; i < str.Length; i++)
    {
        if (str[i] == ' ') result++;
    }
    return result;
}

MyArrayDeque<string> myArrayDeque = new MyArrayDeque<string>();

StreamReader reader = new StreamReader("input.txt");
string line;
while ((line = reader.ReadLine()) != null)
{
    if (NumberOfSpaces(line) > 0) {
        if (myArrayDeque.IsEmpty() == true) myArrayDeque.Add(line);
        else
        {
            string first = myArrayDeque.GetFirst();
            if (NumberOfDigits(first) < NumberOfDigits(line)) myArrayDeque.AddLast(line);
            else myArrayDeque.AddFirst(line);
        }
    }
}
reader.Close();
int n = Convert.ToInt32(Console.ReadLine());
StreamWriter writer = new StreamWriter("output.txt");
while (myArrayDeque.IsEmpty() == false)
{
    line = myArrayDeque.RemoveFirst();
    writer.WriteLine(line);
    if (NumberOfDigits(line)>n) Console.WriteLine(line);
}
writer.Close();