using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* В задаче не использовать циклы for, while. Все действия по обработке данных выполнять с использованием LINQ
 * 
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * Необходимо отфильтровать полученные коллекцию, оставив только отрицательные или четные числа.
 * Дважды вывести коллекцию, разделив элементы специальным символом.
 * Остальные указания см. непосредственно в коде!
 * 
 * Пример входных данных:
 * 1 2 3 4 5
 * 
 * Пример выходных:
 * 2:4
 * 2*4
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
 * 
 */

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {
            // тут я исправил опечаточку
            RunTask01();
        }

        public static void RunTask01()
        {
            int[] arr = null;
            try
            {
                arr = (from e in Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                       select int.Parse(e)).ToArray();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("ArgumentNullException");
                //тут надо закрывать программу, сначала я так думал, а потом решил, что это не нужно
                //ну короче нужно выходить, иначе программа продолжт работу с некорректным массивом
                return;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
                return;
            }
            catch (FormatException)
            {
                Console.WriteLine("FormatException");
                return;
            }
            catch (OverflowException)
            {
                Console.WriteLine("OverflowException");
                return;
            }

            // использовать синтаксис запросов!
            IEnumerable<int> arrQuery = from t in arr
                                        where t < 0 || t % 2 == 0
                                        select t;

            // использовать синтаксис методов!
            IEnumerable<int> arrMethod = arr.Where(t => t < 0 || t % 2 == 0);

            try
            {
                Print(arrQuery, ':');
                Print(arrMethod, '*');
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("ArgumentNullException");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("InvalidOperationException");
            }
        }

        // Попробуйте осуществить вывод элементов коллекции с учетом разделителя, записав это ОДНИМ ВЫРАЖЕНИЕМ.
        // P.S. Есть два способа, оставьте тот, в котором применяется LINQ...
        // я сократил имя, потому что зачем...
        public static void Print<T>(IEnumerable<T> col, char sep) =>
            Console.WriteLine(col.Select(x => x.ToString()).Aggregate((x, y) => x + sep + y));
    }
}
