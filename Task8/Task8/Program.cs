using MyStackLibrary;
MyStack<int> stack = new MyStack<int>();
Console.WriteLine(stack.empty());
stack.push(1);
stack.push(10);
stack.push(20);
stack.push(5);
Console.WriteLine(stack.search(1));
Console.WriteLine(stack.empty());


