using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    internal class UnaricOperations
    {
        public static double sqrt(double a) {
            if (a < 0) {
                Console.WriteLine("Подкоренное выражение не может быть отрицательным.");
                Environment.Exit(0);
                return default(double); }
            return Math.Sqrt(a); }

        public static double tg(double a) {
            try { return Math.Tan(a); }
            catch {
                Console.WriteLine("Значение косинуса в тангенсе равно 0.");
                Environment.Exit(0);
                return default(double); }
        }
        public static double abs(double a) {
            return Math.Abs(a);
        }

        public static double sin(double a) {
            return Math.Sin(a);
        }

        public static double cos(double a) {
            return Math.Cos(a);
        }

        public static double ln(double a) {
            if (a <= 0) {
                Console.WriteLine("Аргумент логарифма должен быть больше 0.");
                Environment.Exit(0);
                return default(double); }
            return Math.Log(a);
        }

        public static double lg(double a) {
            if (a <= 0) {
                Console.WriteLine("Аргумент логарифма должен быть больше 0.");
                Environment.Exit(0);
                return default(double); }
            return Math.Log10(a);
        }

        public static double exp(double a)
        {
            return Math.Exp(a);
        }

        public static double whole(double a)
        {
            return Convert.ToDouble(Convert.ToInt32(a));
        }
    }
    internal class BinaricOperations
    {
        public static double pow(double a, double b)
        {
            return (double)Math.Pow(a, b);
        }

        public static double min(double a, double b)
        {
            if (a > b) return b;
            else return a;
        }

        public static double max(double a, double b)
        {
            if (a < b) return b;
            else return a;
        }

        public static double div (double a, double b)
        {
            if (b == 0)
            {
                Console.WriteLine("На ноль делить нельзя.");
                Environment.Exit(0);
                return (default(double));
            }
            return Convert.ToDouble(Convert.ToInt32(a/b));
        }

        public static double mod(double a, double b)
        {
            if (b == 0)
            {
                Console.WriteLine("На ноль делить нельзя.");
                Environment.Exit(0);
                return (default(double));
            }
            return a- Convert.ToDouble(Convert.ToInt32(a / b));
        }
    }
}
