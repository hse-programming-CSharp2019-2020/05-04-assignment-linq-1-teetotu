using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*Все действия по обработке данных выполнять с использованием LINQ
 * 
 * Объявите перечисление Manufacturer, состоящее из элементов
 * Dell (код производителя - 0), Asus (1), Apple (2), Microsoft (3).
 * 
 * Обратите внимание на класс ComputerInfo, он содержит поле типа Manufacturer
 * 
 * На вход подается число N.
 * На следующих N строках через пробел записана информация о компьютере: 
 * фамилия владельца, код производителя (от 0 до 3) и год выпуска (в диапазоне 1970-2020).
 * Затем с помощью средств LINQ двумя разными способами (как запрос или через методы)
 * отсортируйте коллекцию следующим образом:
 * 1. Первоочередно объекты ComputerInfo сортируются по фамилии владельца в убывающем порядке
 * 2. Для объектов, у которых фамилии владельцев сопадают, 
 * сортировка идет по названию компании производителя (НЕ по коду) в возрастающем порядке.
 * 3. Если совпадают и фамилия, и имя производителя, то сортировать по году выпуска в порядке убывания.
 * 
 * Выведите элементы каждой коллекции на экран в формате:
 * <Фамилия_владельца>: <Имя_производителя> [<Год_производства>]
 * 
 * Пример ввода:
 * 3
 * Ivanov 1970 0
 * Ivanov 1971 0
 * Ivanov 1970 1
 * 
 * Пример вывода:
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * 
 *  * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * При некорректных входных данных (не связанных с созданием объекта) выбрасывайте FormatException
 * При невозможности создать объект класса ComputerInfo выбрасывайте ArgumentException!
 */
namespace Task03
{
    class Program
    {
        static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            int N = 0;
            List<ComputerInfo> computerInfoList = new List<ComputerInfo>();
            try
            {
                N = int.Parse(Console.ReadLine());
                char[] chr = { ' ' };
                for (int i = 0; i < N; i++)
                {
                    string[] str = Console.ReadLine().Split(chr, StringSplitOptions.RemoveEmptyEntries);
                    computerInfoList.Add(new ComputerInfo(str[0], int.Parse(str[1]), (Manufacturer)int.Parse(str[2])));
                }
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


            // выполните сортировку одним выражением
            var computerInfoQuery = from x in computerInfoList
                                    orderby x.Owner descending
                                    orderby x.ComputerManufacturer.ToString()
                                    orderby x.yearOfManufacture descending
                                    select x;

            PrintCollectionInOneLine(computerInfoQuery);

            Console.WriteLine();

            // выполните сортировку одним выражением
            var computerInfoMethods = computerInfoList.
                OrderByDescending(x => x.Owner).ThenBy(x => x.ComputerManufacturer.ToString()).ThenByDescending(x => x.yearOfManufacture);

            PrintCollectionInOneLine(computerInfoMethods);

            Console.ReadLine();
        }

        // выведите элементы коллекции на экран с помощью кода, состоящего из одной линии (должна быть одна точка с запятой)
        //public static void PrintCollectionInOneLine<T>(IEnumerable<T> col) => тут просто метод из прошлых заданий
        //    Console.WriteLine(col.Select(x => x.ToString()).Aggregate((x, y) => x + Environment.NewLine + y));
        public static void PrintCollectionInOneLine(IEnumerable<ComputerInfo> collection) =>
            collection.ToList().ForEach(x => Console.WriteLine(x));
    }


    class ComputerInfo
    {
        public string Owner { get; set; }
        public int yearOfManufacture { get; set; }
        public Manufacturer ComputerManufacturer { get; set; }

        public ComputerInfo(string owner, int year, Manufacturer manufacturer)
        {
            if (year < 1970 || year > 2020 || manufacturer > Manufacturer.Microsoft || manufacturer < Manufacturer.Dell)
                throw new ArgumentException();
            Owner = owner;
            yearOfManufacture = year;
            ComputerManufacturer = manufacturer;
        }

        public override string ToString() =>
            $"{Owner}: {ComputerManufacturer} [{yearOfManufacture}]";
    }

    enum Manufacturer
    {
        Dell = 0,
        Asus,
        Apple,
        Microsoft
    }
}
