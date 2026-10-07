namespace MenuDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool exitProgram = false;
            while (!exitProgram)
            {
                Console.WriteLine("HUVUDMENY:");
                Console.WriteLine("----------");
                Console.WriteLine("1. Kontrollera pris för 1 person");
                Console.WriteLine("2. Kontrollera pris för ett sällskap");
                Console.WriteLine("3. Repetera ord eller mening 10 gånger");
                Console.WriteLine("4. Hitta tredje ordet i en mening");
                Console.WriteLine("0. Avsluta");

                string? input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    switch (result)
                    {
                        case 0:
                            Console.WriteLine("Välkommen åter!");
                            exitProgram = true;
                            break;
                        case 1:
                            Console.Clear();
                            CheckPrice(printToConsole: true);
                            PressAnyKey();
                            break;
                        case 2:
                            GetPartyPrice();
                            break;
                        case 3:
                            Console.Clear();
                            WordLoop();
                            break;
                        case 4:
                            FindThirdWord();
                            PressAnyKey();
                            break;
                        default:
                            MalformedInput();
                            break;
                    }
                }
                else
                {
                    MalformedInput();
                }
            }
        }

        static void MalformedInput()
        {
            Console.Clear();
            Console.WriteLine("Felaktig inmatning, försök igen\n");
        }

        static void PressAnyKey()
        {
            Console.WriteLine("\nTryck valfri tangent för att fortsätta...\n");
            Console.ReadKey();
            Console.Clear();
        }

        static void WordLoop()
        {
            string? input;
            do
            {
                Console.WriteLine("Skriv ett ord eller mening");
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    MalformedInput();

            } while (string.IsNullOrEmpty(input));

            for (int i = 0; i < 10; i++)
            {
                Console.Write(input);
            }
            Console.WriteLine();
            PressAnyKey();
        }

        static void FindThirdWord()
        {
            Console.Clear();

            string[] words;
            while (true) 
            {
                Console.WriteLine("Skriv en mening med minst tre ord");
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    MalformedInput();
                    continue;
                }

                words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (words.Length > 2)
                {
                    Console.WriteLine($"Det tredje ordet är: \"{words[2]}\"");
                    break;
                }
                else
                {
                    MalformedInput();
                }

            } 
        }

        static void GetPartyPrice()
        {
            int totalPrice = 0;
            int peopleInParty = 0;
            int peopleCounted = 0;
            bool canceled = false;
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Hur många personer ingår i sällskapet? (0 för att avbryta)");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out peopleInParty))
                {
                    if (peopleInParty <= 0)
                    {
                        canceled = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ange x som ålder om du inte vill ange fler personer");

                        for (int i = 0; i < peopleInParty; i++)
                        {
                            int? price = CheckPrice(i + 1);
                            if (price == null)
                            {
                                if (i == 0) //Om vi inte har fått några priser avbryt helt annars rapporterar vi tillbaka det vi har,
                                    canceled = true;
                                break;
                            }
                            totalPrice += price.Value;
                            peopleCounted++;
                        }
                        break;
                    }
                }
                else
                {
                    MalformedInput();
                }
            }
            if (!canceled)
            {
                string noOfPeople = $"{"Antal personer i sällskapet:",-32} {peopleCounted}st";
                string cost = $"{"Priset för sällskapet är:",-32} {totalPrice}kr";
                Console.Write("\n\n");
                Console.WriteLine(noOfPeople);
                Console.WriteLine(cost);
                Console.WriteLine(new string('-', Math.Max(noOfPeople.Length, cost.Length)));
                PressAnyKey();
            }
            else Console.Clear();
        }
        static int? CheckPrice(int person = 0, bool printToConsole = false)
        {

            const int youthPrice = 80;
            const int seniorPrice = 90;
            const int defaultPrice = 120;

            while (true)
            {
                if (person > 0)
                {
                    Console.WriteLine($"Vilken ålder har person nr: {person}");
                }
                else
                {
                    Console.WriteLine("Vilken ålder har personen?");
                }
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int age))
                {
                    if (age < 0)
                    {
                        Console.WriteLine("Ålder kan inte vara mindre än 0");
                        continue;
                    }
                    if (age < 5)
                    {
                        if (printToConsole)
                            Console.WriteLine($"\nGratis för barn under 5 år!");
                        return 0;
                    }
                    else if (age < 20)
                    {
                        if (printToConsole)
                            Console.WriteLine($"\nUngdomspris: {youthPrice}kr");
                        return youthPrice;
                    }
                    else if (age > 100)
                    {
                        if (printToConsole)
                            Console.WriteLine($"\nGratis för personer över 100 år!");
                        return 0;
                    }
                    else if (age > 64)
                    {
                        if (printToConsole)
                            Console.WriteLine($"\nPensionärspris: {seniorPrice}kr");
                        return seniorPrice;
                    }
                    else
                    {
                        if (printToConsole)
                            Console.WriteLine($"\nStandardpris: {defaultPrice}kr");
                        return defaultPrice;
                    }
                }

                else if (input == "x")
                {
                    return null;
                }
                else
                {
                    MalformedInput();
                }

            }
        }
    }
}
