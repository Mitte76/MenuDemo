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
                Console.WriteLine("1. Kontrollera pris för 1 person");
                Console.WriteLine("2. Kontrollera pris för ett sälskap");
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
                            CheckPrice(printToConsole: true);
                            PressAnyKey();
                            break;
                        case 2:
                            GetPartyPrice();
                            PressAnyKey();
                            break;
                        case 3:
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
            Console.Clear();
            Console.WriteLine("Skriv ett ord eller mening");
            string? input = Console.ReadLine();

            for (int i = 0; i < 10; i++)
            {
                Console.Write(input);
            }
            PressAnyKey();
        }

        static void FindThirdWord()
        {
            Console.Clear();
            Console.WriteLine("Skriv en mening med minst tre ord");

            string? input = Console.ReadLine();
            if (input == null) return;
            string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 2)
            {
                Console.WriteLine($"Det tredje ordet är: \"{words[2]}\"");
            }
            else
            {
                Console.WriteLine("Du måste skriva en mening med minst tre ord");
            }

        }

        static void GetPartyPrice()
        {
            int totalPrice = 0;
            int peopleInParty = 0;
            bool canceled = false;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Hur många personer ingår i sällskapet? (0 för att avbryta)");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out peopleInParty))
                {
                    if (peopleInParty == 0)
                    {
                        canceled = true;
                        break;
                    }
                    else
                    {
                        for (int i = 0; i < peopleInParty; i++)
                        {
                            int price = CheckPrice(i + 1);
                            if(price == -1)
                            {
                                canceled = true;
                                break;
                            }
                            totalPrice += price;
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
                string noOfPeople = $"{"Antal personer i sällskapet:",-32} {peopleInParty}st";
                string cost = $"{"Priset för sällskapet är:",-32} {totalPrice}kr";
                Console.Write("\n\n");
                Console.WriteLine(noOfPeople);
                Console.WriteLine(cost);
                Console.WriteLine(new string('-', Math.Max(noOfPeople.Length, cost.Length)));

            }
        }
        static int CheckPrice(int person = 0, bool printToConsole = false)
        {

            int youthPrice = 80;
            int seniorPrice = 90;
            int defaultPrice = 120;

            while (true)
            {
                if (person > 0)
                {
                    Console.WriteLine($"Vilken ålder har person nr: {person} (x för att avbryta)");
                }
                else
                {
                    Console.WriteLine("Vilken ålder har personen?");
                }
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int age))
                {
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
                    return -1;
                }
                else
                {
                    MalformedInput();
                }

            }
        }
    }
}
