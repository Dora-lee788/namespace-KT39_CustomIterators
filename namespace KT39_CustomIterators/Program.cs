using System.Collections;
namespace KT39_CustomIterators
{
    public class CircularBuffer<T> : IEnumerable<T>
    {
        private T[] items;
        private int start;
        private int count;

        public CircularBuffer(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity),
                    "Ёмкость должна быть больше нуля.");
            }

            items = new T[capacity];
            start = 0;
            count = 0;
        }

        public void Add(T item)
        {
            if (count < items.Length)
            {
                int index = (start + count) % items.Length;
                items[index] = item;
                count++;
            }
            else
            {
                items[start] = item;
                start = (start + 1) % items.Length;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                yield return items[(start + i) % items.Length];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> Reverse()
        {
            for (int i = count - 1; i >= 0; i--)
            {
                yield return items[(start + i) % items.Length];
            }
        }
    }

    internal class Program
    {
        static int ReadInt()
        {
            while (true)
            {
                try
                {
                    return int.Parse(Console.ReadLine()!);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Ошибка формата. Введите целое число.");
                    Console.WriteLine(ex);
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine("Число выходит за допустимые границы.");
                    Console.WriteLine(ex);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Ввод не должен быть пустым.");
                    Console.WriteLine(ex);
                }
            }
        }

        static void Main(string[] args)
        {
            CircularBuffer<int> buffer = null;

            while (true)
            {
                try
                {
                    Console.WriteLine("\n===== КОЛЬЦЕВОЙ БУФЕР =====");
                    Console.WriteLine("1. Создать буфер");
                    Console.WriteLine("2. Добавить элемент");
                    Console.WriteLine("3. Обход от старого к новому");
                    Console.WriteLine("4. Обход от нового к старому");
                    Console.WriteLine("0. Выход");
                    Console.Write("Выберите действие: ");

                    int choice = ReadInt();

                    if (choice == 0)
                    {
                        break;
                    }
                    else if (choice == 1)
                    {
                        Console.Write("Введите ёмкость буфера: ");
                        int capacity = ReadInt();

                        buffer = new CircularBuffer<int>(capacity);

                        Console.WriteLine("Буфер успешно создан.");
                    }
                    else if (choice == 2)
                    {
                        if (buffer == null)
                        {
                            Console.WriteLine("Сначала создайте буфер.");
                            continue;
                        }

                        Console.Write("Введите число: ");
                        int number = ReadInt();

                        buffer.Add(number);

                        Console.WriteLine("Элемент добавлен.");
                    }
                    else if (choice == 3)
                    {
                        if (buffer == null)
                        {
                            Console.WriteLine("Сначала создайте буфер.");
                            continue;
                        }

                        Console.WriteLine("От старого к новому:");

                        foreach (int item in buffer)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    else if (choice == 4)
                    {
                        if (buffer == null)
                        {
                            Console.WriteLine("Сначала создайте буфер.");
                            continue;
                        }

                        Console.WriteLine("От нового к старому:");

                        foreach (int item in buffer.Reverse())
                        {
                            Console.WriteLine(item);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Такого пункта меню нет.");
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Ошибка: ёмкость должна быть положительной.");
                    Console.WriteLine(ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка.");
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
