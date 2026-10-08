namespace MenuDemo
{
    internal class Program
    {
        static int mainMenuItems = 5;
        static void Main(string[] args)
        {

            bool exitProgram = false;
            int currentItem = 0;
            while (!exitProgram)
            {
                ShowMainMenu(currentItem);

                ConsoleKey input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.DownArrow:
                        currentItem++;
                        if (currentItem >= mainMenuItems)
                            currentItem = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        currentItem--;
                        if (currentItem < 0)
                            currentItem = mainMenuItems - 1;
                        break;
                    case ConsoleKey.Enter:
                        OpenItem((currentItem + 1) % mainMenuItems);
                        if (currentItem == 4)
                            exitProgram = true;
                        break;
                    case ConsoleKey.D0:
                        OpenItem(0);
                        exitProgram = true;
                        break;
                    case ConsoleKey.D1:
                        Console.Clear();
                        OpenItem(1);
                        PressAnyKey();
                        break;
                    case ConsoleKey.D2:
                        OpenItem(2);
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        OpenItem(3);
                        break;
                    case ConsoleKey.D4:
                        OpenItem(4);
                        PressAnyKey();
                        break;
                    default:
                        MalformedInput();
                        break;
                }

                currentItem = currentItem % 5;

            }
        }

        static void OpenItem(int itemNumber)
        {
            switch (itemNumber)
            {
                case 0:
                    Console.WriteLine("Välkommen åter!");
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

            }

        }
        static void ShowMainMenu(int currentItem)
        {
            Console.Clear();
            Console.WriteLine("HUVUDMENY:");
            Console.WriteLine("----------");

            string[] items =
            [
                "Pris för 1 person",
                "Pris för ett sällskap",
                "Repetera ord eller mening",
                "Hitta tredje ordet",
                "Avsluta",
            ];

            for (int i = 0; i < items.Length; i++)
            {
                bool selected = i == currentItem;
                int menuNumber = i == items.Length - 1 ? 0 : i + 1; //Beräkning för att få alternativet "0. avsluta" längst ner
                PrintMenuItem(menuNumber, items[i], selected);
            }
        }

        static void PrintMenuItem(int number, string text, bool selected)
        {
            Console.Write($"{number}.[");

            Console.ForegroundColor = selected
                ? ConsoleColor.Green
                : ConsoleColor.DarkGray;

            Console.Write(selected ? "X" : " ");

            Console.ResetColor();
            Console.WriteLine($"] {text}");
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
                Console.WriteLine("Skriv en mening med minst tre ord (exit för att avsluta)");
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    MalformedInput();
                    continue;
                }

                if (input.ToLower() == "exit") break;

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
                Console.WriteLine("Antal personer i sällskapet?");

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
                        Console.Clear();
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
                string noOfPeople1 = $"{"Antal personer i sällskapet:",-32} ";
                string noOfPeople2 = $"{peopleCounted}st";
                string cost1 = $"{"Priset för sällskapet är:",-32} ";
                string cost2 = $"{totalPrice}kr";

                Console.Write("\n\n");
                Console.Write(noOfPeople1);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(noOfPeople2);
                Console.ResetColor();
                Console.Write(cost1);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(cost2);
                Console.ResetColor();
                Console.WriteLine(new string('-', Math.Max(noOfPeople1.Length + noOfPeople2.Length, cost1.Length + cost2.Length)));
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
                    Console.WriteLine($"Vilken ålder har person nr: {person} (x = avbryt)");
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
