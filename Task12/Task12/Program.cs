using ApplicationLib;
using MyPriorityQueueLib;

int n;
int Number = 1;
Console.WriteLine("Введите количество n");
n =Convert.ToInt32(Console.ReadLine());
MyPriorityQueue Queue = new MyPriorityQueue();
string filePath = "output.txt";
using (StreamWriter writer = new StreamWriter(filePath, false))
{
}
Application MaxApp = new Application();
int MaxStep = -1;
int NumberOfSteps=0;
for (int NumberOfStep=0; NumberOfStep<n; NumberOfStep++)
{
    Random rnd = new Random();
    int NumberOfApplications = rnd.Next(1, 11);
    for (int i = 0; i < NumberOfApplications; i++)
    {
        Application app = new Application();
        app.NumberOfPriority = rnd.Next(1, 6);
        app.NumberOfStep = NumberOfStep;
        app.NumberOfApplication = Number;
        Number++;
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"ADD {app.NumberOfApplication} {app.NumberOfPriority} {app.NumberOfStep}");
        }
            Queue.Add(app);
    }
    Application DeletedApp= new Application();
    DeletedApp = Queue.Poll();
    using (StreamWriter writer = new StreamWriter(filePath, true))
    {
        writer.WriteLine($"REMOVE {DeletedApp.NumberOfApplication} {DeletedApp.NumberOfPriority} {DeletedApp.NumberOfStep}");
    }
    if (MaxStep ==-1 || (NumberOfStep - DeletedApp.NumberOfStep)>MaxStep)
    {
        MaxStep = NumberOfStep;
        MaxApp = DeletedApp;
    }
    NumberOfSteps++;
}
while (Queue.IsEmpty() == false)
{
    Application DeletedApp = new Application();
    DeletedApp = Queue.Poll();
    using (StreamWriter writer = new StreamWriter(filePath, true))
    {
        writer.WriteLine($"REMOVE {DeletedApp.NumberOfApplication} {DeletedApp.NumberOfPriority} {DeletedApp.NumberOfStep}");
    }
    if (MaxStep == -1 || (NumberOfSteps - DeletedApp.NumberOfStep) > MaxStep)
    {
        MaxStep = NumberOfSteps;
        MaxApp = DeletedApp;
    }
}

using (StreamWriter writer = new StreamWriter(filePath, true))
{
    writer.WriteLine();
    writer.WriteLine($"{MaxApp.NumberOfApplication} {MaxApp.NumberOfPriority} {MaxApp.NumberOfStep}");
}