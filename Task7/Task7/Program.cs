using MyVectorLibrary;

static bool IsValidIPAddress(string ipAddress)
{
    if (string.IsNullOrEmpty(ipAddress))
        return false;

    string[] segments = ipAddress.Split('.');
    if (segments.Length != 4)
        return false;

    foreach (string segment in segments)
    {
        if (!int.TryParse(segment, out int value) || value < 0 || value > 255 || segment.Length != value.ToString().Length)
            return false;
    }

    return true;
}

bool Are_IP_Different(string ipAddress1, string ipAddress2)
{
    if (ipAddress1 != null && ipAddress2 != null && ipAddress1.Length == ipAddress2.Length)
    {
        for (int i = 0; i < ipAddress1.Length; i++)
        {
            if (ipAddress1[i] == ipAddress2[i] && ipAddress1[i]!='.') return false;
        }
        return true;
    }
    else return true;
}

bool check(MyVector<string> vector, string ipAddress)
{
    int num = vector.Size();
    if (num == 0) return true;
    for (int i = 0; i < num; i++)
    {
        string str=vector.get(i);
        if (Are_IP_Different(str,ipAddress)==false) return false;
    }
    return true;
}

MyVector <string> vector_of_strings = new MyVector<string> (10,5);
MyVector<string> vector_of_IP = new MyVector<string>(10, 5);
using (StreamReader sr = new StreamReader("input.txt")) {
    string line;
    while ((line = sr.ReadLine()) != null) {
        vector_of_strings.add(line);
    }
}
int num = vector_of_strings.Size();
string str;
for (int i = 0; i < num; i++)
{
    str = vector_of_strings.get(i)+" ";
    int flag = -1;
    string Ip;
    string[] segments = new string[4];
    for (int j = 0; j < str.Length; j++)
    {
        if (Char.IsDigit(str[j]))
        {
            if (flag == -1)
            {
                flag = 0;
                segments[flag] = segments[flag] + str[j];
            }
            else segments[flag] = segments[flag] + str[j];
        }
        else if (str[j] == '.')
        {
            segments[flag] = segments[flag] + '.';
            flag++;
            if (flag == 4) 
            {
                flag = 3;
                Ip = segments[0] + segments[1] + segments[2] + segments[3];
                if (IsValidIPAddress(Ip) && check(vector_of_IP, Ip))
                {
                    vector_of_IP.add(Ip);
                }
                segments[0] = segments[1];
                segments[1] = segments[2];
                segments[2] = segments[3];
                segments[3] = "";
            }
        }
        else
        {
            Ip = segments[0] + segments[1] + segments[2] + segments[3];
            if (IsValidIPAddress(Ip) && check(vector_of_IP, Ip))
            {
                vector_of_IP.add(Ip);
            }
            segments = new string[4];
            flag = -1;
        }
        Console.WriteLine(segments[0] + segments[1] + segments[2] + segments[3]);
    }
}
num=vector_of_IP.Size();
for (int i = 0; i < num;i++) Console.WriteLine(vector_of_IP.get(i));