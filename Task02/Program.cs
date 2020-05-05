using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

/* В задаче не использовать циклы for, while. Все действия по обработке данных выполнять с использованием LINQ
 * 
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * Необходимо оставить только те элементы коллекции, которые предшествуют нулю, или все, если нуля нет.
 * Дважды вывести среднее арифметическое квадратов элементов новой последовательности.
 * Вывести элементы коллекции через пробел.
 * Остальные указания см. непосредственно в коде.
 * 
 * Пример входных данных:
 * 1 2 0 4 5
 * 
 * Пример выходных:
 * 2,500
 * 2,500
 * 1 2
 * 
 * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * В случае возникновения иных нештатных ситуаций (например, в случае попытки итерирования по пустой коллекции) 
 * выбрасывайте InvalidOperationException!
 */
namespace Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            RunTask02();
        }

        public static void RunTask02()
        {
            int[] arr = null;
            try
            {
                // Попробуйте осуществить считывание целочисленного массива, записав это ОДНИМ ВЫРАЖЕНИЕМ.
                arr = (from x in Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                       select int.Parse(x)).ToArray();
                checked
                {
                    int[] filteredCollection = arr.TakeWhile(x => x != 0).ToArray();

                    //зачем я дважды чекд сделал...подсказка была принята криво...
                    double averageUsingStaticForm = Enumerable.Average(filteredCollection.Select(val => val * val));
                    double averageUsingInstanceForm = filteredCollection.Select(val => val * val).Average();

                    Console.WriteLine($"{averageUsingInstanceForm:f3}");
                    Console.WriteLine($"{averageUsingStaticForm:f3}");

                    // вывести элементы коллекции в одну строку
                    Print(filteredCollection, ' ');
                }
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("ArgumentNullException");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
            }
            catch (FormatException)
            {
                Console.WriteLine("FormatException");

            }
            catch (OverflowException)
            {
                Console.WriteLine("OverflowException");
            }
        }

        public static void Print<T>(IEnumerable<T> col, char sep) =>
            Console.WriteLine(col.Select(x => x.ToString()).Aggregate((x, y) => x + sep + y));
    }
}
