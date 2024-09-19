using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        struct Complex
        {
            public double real;
            public double imag;
            public Complex(double x, double y) { real = x; imag = y; }
            public Complex sum(Complex num1, Complex num2)
            {
                Complex num3 = new Complex();
                num3.real = num1.real + num2.real;
                num3.imag = num1.imag + num2.imag;
                return num3;
            }
            public Complex sub(Complex num1, Complex num2)
            {
                Complex num3 = new Complex();
                num3.real = num1.real - num2.real;
                num3.imag = num1.imag - num2.imag;
                return num3;
            }
            public Complex multiplication(Complex num1, Complex num2) {
                Complex num3 = new Complex();
                num3.real = num1.real * num2.real - num1.imag * num2.imag;
                num3.imag = num1.real * num2.imag + num2.real * num1.imag;
                return num3;
            }
            public Complex division(Complex num1, Complex num2) {
                Complex num3 = new Complex();
                num3.real = (num1.real * num2.real + num1.imag * num2.imag) / (num2.real * num2.real + num2.imag * num2.imag);
                num3.imag = (num1.imag * num2.real - num1.real * num2.imag) / (num2.real * num2.real + num2.imag * num2.imag);
                return num3;
            }
            public double abs(Complex num)
            {
                return Math.Sqrt(num.real * num.real + num.imag * num.imag);
            }
            public double arg(Complex num) {
                if (num.real == 0 && num.imag > 0) return Math.PI;
                else if (num.real == 0 && num.imag < 0) return -Math.PI;
                else if (num.imag > 0) return Math.Atan(num.imag / num.real);
                else return Math.Atan(num.imag/num.real)+ Math.PI;  
            }
            public double real_part(Complex num)
            {
                return num.real;
            }
            public double imag_part(Complex num)
            {
                return num.real;
            }
            public string output(Complex num)
            {
                return $"\nДействительное число - это {num.real} и мнимое - {num.imag}";
            }
        }
        static void Main(string[] args)
        {
            double Number;
            Complex num1=new Complex();
            Complex num2 = new Complex();
            num1.real = 0;
            num1.imag=0;
            while (true)
            {
                Console.WriteLine("\nВвести коплексное число - a");
                Console.WriteLine("Сумма двух комплексных чисел - b");
                Console.WriteLine("Разность двух комплексных чисел - c");
                Console.WriteLine("Произведение двух комплексных чисел - d");
                Console.WriteLine("Деление двух комплексных чисел - e");
                Console.WriteLine("Модуль числа - f");
                Console.WriteLine("Аргумент числа - h");
                Console.WriteLine("Конец программы - q или Q");
                Console.WriteLine("Вывод всего числа - g");
                char letter = Console.ReadKey().KeyChar;
                switch (letter) {
                    case 'a':
                        {
                            Console.WriteLine("\nВведите 2 числа: действительное и мнимое");
                            num2.real = Convert.ToDouble(Console.ReadLine());
                            num2.imag = Convert.ToDouble(Console.ReadLine());
                            num1.real = num2.real;
                            num1.imag = num2.imag;
                            break;
                        }
                    case 'b':
                        {
                            Console.WriteLine("\nВведите 2 числа: действительное и мнимое");
                            num2.real = Convert.ToDouble(Console.ReadLine());
                            num2.imag = Convert.ToDouble(Console.ReadLine());
                            num1.sum(num1, num2);
                            break;
                        }
                    case 'c':
                        {
                            Console.WriteLine("\nВведите 2 числа: действительное и мнимое");
                            num2.real = Convert.ToDouble(Console.ReadLine());
                            num2.imag = Convert.ToDouble(Console.ReadLine());
                            num1.sub(num1, num2);
                            break;
                        }
                    case 'd':
                        {
                            Console.WriteLine("\nВведите 2 числа: действительное и мнимое");
                            num2.real = Convert.ToDouble(Console.ReadLine());
                            num2.imag = Convert.ToDouble(Console.ReadLine());
                            num1.multiplication(num1, num2);
                            break;
                        }
                    case 'e':
                        {
                            Console.WriteLine("\nВведите 2 числа: действительное и мнимое");
                            num2.real = Convert.ToDouble(Console.ReadLine());
                            num2.imag = Convert.ToDouble(Console.ReadLine());
                            num1.division(num1, num2);
                            break;
                        }
                    case 'f':
                        {
                            Console.WriteLine($"\nМодуль равен {num1.abs(num1)}");
                            break;
                        }
                    case 'h':
                        {
                            Console.WriteLine($"\nАргумент равен {num1.arg(num1)}");
                            break;
                        }
                    case 'q':
                    case 'Q':
                        {
                            return;
                        }
                    case 'g':
                        {
                            Console.WriteLine(num1.output(num1));
                            break;
                        }
                    default:
                        Console.WriteLine("\nНеизвестная команда");
                        break;
                }
            }
        }
    }
}
