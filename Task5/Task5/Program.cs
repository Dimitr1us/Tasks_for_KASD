using MyArrayListLibrary;

bool check(string word)
{
    if (word[0] == '/')
    {
        if (word.Length > 1)
        {
            if (Char.IsDigit(word[1])) return false;
            else return true;
        }
        else return false;
    }
    else
    {
        if (Char.IsDigit(word[0])) return false;
        else return true;
    }
}

bool AreStringsEqual(string str1, string str2)
{
    if (str1 == null || str2 == null)
        return false;

    string normalizedStr1 = str1.Replace("/", "").ToLower();
    string normalizedStr2 = str2.Replace("/", "").ToLower();

    return normalizedStr1 == normalizedStr2;
}


MyArrayList <string> list = new MyArrayList <string> ();

string filePath = "TextFile1.txt";
using (StreamReader reader = new StreamReader(filePath))
{
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        string word="";
        bool IsWord=false;
        for (int i = 0; i < line.Length; i++) {
            if (line[i] == '<' && IsWord == false)
            {
                IsWord = true;
            }
            else if (IsWord == true && line[i]!='>')
            {
                word = word + line[i];
            }
            else if (IsWord==true && line[i] == '>')
            {
                IsWord=false;
                if (check(word) == true)
                {
                    list.add(word);
                }
                word = "";
            }
        }
    }
}
int num = list.Size();
Console.WriteLine(num);
int j = 0;
while (j < num-1)
{
    string word = list.get(j);
    int k = j + 1;
    while (k < num) {
        string word2 = list.get(k);
        if (AreStringsEqual(word2, word))
        {
            list.remove(k);
            num = num - 1;
        }
        else k++;
    }
    j++;
}
Console.WriteLine(num);