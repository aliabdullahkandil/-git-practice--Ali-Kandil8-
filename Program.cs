namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            string choice;

            do
            {
                Console.WriteLine("\nMain Menu");
                Console.WriteLine("P - Print numbers");
                Console.WriteLine("A - Add a number");
                Console.WriteLine("M - Display mean of the numbers");
                Console.WriteLine("S - Display the smallest number");
                Console.WriteLine("L - Display the largest number");
                Console.WriteLine("F - Find a number");
                Console.WriteLine("C - Clear the whole list");
                Console.WriteLine("X - Ascending sort");
                Console.WriteLine("D - Descending sort");
                Console.WriteLine("Q - Quit");
                Console.Write("Enter your choice: ");
                choice = Console.ReadLine().ToUpper();

                if (choice == "P")
                {
                    if (numbers.Count == 0)
                        Console.WriteLine("[] - the list is empty");
                    else
                    {
                        Console.Write("[ ");
                        int i = 0;
                        do
                        {
                            Console.Write(numbers[i] + " ");
                            i++;
                        } while (i < numbers.Count);
                        Console.WriteLine("]");
                    }
                }
                else if (choice == "A")
                {
                    Console.Write("Enter a number to add: ");
                    string input = Console.ReadLine();
                    if (input != "")
                    {
                        int num = Convert.ToInt32(input);
                        numbers.Add(num);
                        Console.WriteLine(num + " added.");
                    }
                    else
                        Console.WriteLine("No number entered.");
                }
                else if (choice == "M")
                {
                    if (numbers.Count == 0)
                        Console.WriteLine("No numbers to calculate mean.");
                    else
                    {
                        int i = 0, sum = 0;
                        do
                        {
                            sum += numbers[i];
                            i++;
                        } while (i < numbers.Count);

                        double mean = (double)sum / numbers.Count;
                        Console.WriteLine("Mean: " + mean.ToString("F2"));
                    }
                }
                else if (choice == "S")
                {
                    if (numbers.Count == 0)
                        Console.WriteLine("No numbers to check.");
                    else
                    {
                        int i = 1;
                        int smallest = numbers[0];
                        do
                        {
                            if (numbers[i] < smallest)
                                smallest = numbers[i];
                            i++;
                        } while (i < numbers.Count);
                        Console.WriteLine("Smallest number: " + smallest);
                    }
                }
                else if (choice == "L")
                {
                    if (numbers.Count == 0)
                        Console.WriteLine("No numbers to check.");
                    else
                    {
                        int i = 1;
                        int largest = numbers[0];
                        do
                        {
                            if (numbers[i] > largest)
                                largest = numbers[i];
                            i++;
                        } while (i < numbers.Count);
                        Console.WriteLine("Largest number: " + largest);
                    }
                }
                else if (choice == "F")
                {
                    if (numbers.Count == 0)
                        Console.WriteLine("The list is empty.");
                    else
                    {
                        Console.Write("Enter a number to find: ");
                        int find = Convert.ToInt32(Console.ReadLine());
                        bool found = false;
                        int i = 0;

                        do
                        {
                            if (numbers[i] == find)
                            {
                                Console.WriteLine(find + " found at position " + (i + 1));
                                found = true;
                                break;
                            }
                            i++;
                        } while (i < numbers.Count);

                        if (!found)
                            Console.WriteLine(find + " not found in the list.");
                    }
                }
                else if (choice == "C")
                {
                    numbers.Clear();
                    Console.WriteLine("List cleared.");
                }
                else if (choice == "X")
                {
                    int i = 0;
                    do
                    {
                        int j = i + 1;
                        do
                        {
                            if (j < numbers.Count && numbers[j] < numbers[i])
                            {
                                int temp = numbers[i];
                                numbers[i] = numbers[j];
                                numbers[j] = temp;
                            }
                            j++;
                        } while (j < numbers.Count);
                        i++;
                    } while (i < numbers.Count - 1);
                    Console.WriteLine("List sorted in ascending order.");
                }
                else if (choice == "D")
                {
                    int i = 0;
                    do
                    {
                        int j = i + 1;
                        do
                        {
                            if (j < numbers.Count && numbers[j] > numbers[i])
                            {
                                int temp = numbers[i];
                                numbers[i] = numbers[j];
                                numbers[j] = temp;
                            }
                            j++;
                        } while (j < numbers.Count);
                        i++;
                    } while (i < numbers.Count - 1);
                    Console.WriteLine("List sorted in descending order.");
                }
                else if (choice == "Q")
                {
                    Console.WriteLine("Goodbye!");
                }
                else
                {
                    Console.WriteLine("Invalid option. Try again.");
                }

            } while (choice != "Q");
        }
    }
}


