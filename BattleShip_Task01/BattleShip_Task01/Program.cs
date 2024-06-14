namespace BattleShip_Task01
{
    internal class Program
    {
        //Felső sor
        public static char[] letter_Space = { ' ', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        //Játékos mezői
        public static char[,] battlefield_Player = new char[,] {{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },
                {'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },
                {'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', } };
        //Ellenfél mezői (Rejtve/nem látható)
        public static char[,] battlefield_CPU = new char[,] {{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },
                {'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },
                {'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', } };
        //Ellenfél mezői (Látható)
        public static char[,] battlefield_CPU_Show = new char[,] {{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },
                {'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },
                {'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', },{'~','~','~','~','~','~','~','~','~','~', } };

        
        //Hajók constansokban (Még nem használt)
        public const int harmas = 3;
        public const int negyes = 4;
        public const int otos = 5;
        //Hajók száma (Fél megoldás)
        public static int ship_Nums = 17;
        static Random rnd = new Random();

        public static int counter = 0;
        static int counter2 = 0;
        public static string user_Input;
        public static int player_Num = 1;

        public static bool end_Game = false;

        //Választott koordináta átalakítása
        static Dictionary<char, int> letter_Space_ToIndex = new Dictionary<char, int>()
        {
            {'A', 0},
            {'B', 1},
            {'C', 2},
            {'D', 3},
            {'E', 4},
            {'F', 5},
            {'G', 6},
            {'H', 7},
            {'I', 8},
            {'J', 9}
        };

        static void Main(string[] args)
        {
            Welcome();
            Battle_Start_Counter();

            do {
                Console.WriteLine("Player Fields");
                BattleField(letter_Space, battlefield_Player);
                Console.WriteLine();
                Console.WriteLine("CPU/Player 2 Fields");
                BattleField(letter_Space, battlefield_CPU_Show);
                if (player_Num == 1)
                {
                    Console.WriteLine("Player 1 Turn");
                    Input_PlayerOne();
                    player_Num = 2;
                }
                else if (player_Num == 2)
                {
                    Console.WriteLine("Player 2 Turn");
                    Input_PlayerTwo();
                    player_Num = 1;
                }
                Thread.Sleep(1000);
                Console.Clear();
            }
            while(!end_Game);
            

            Console.WriteLine("That was a good game");



            Console.ReadLine();
        }

        //Pálya kirajzolásért felelős metódus.
        public static void BattleField(char[] letter_Space, char[,] battlefield_Player)
        {
            
                for (int i = 0; i < letter_Space.Length; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(letter_Space[i].ToString() + ' ');
                }
                Console.WriteLine();

                for (int i = 0; i < battlefield_Player.GetLength(0); i++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(i);
                    for (int j = 0; j < battlefield_Player.GetLength(1); j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        if (battlefield_Player[i, j] == 'X')
                        {
                            counter++;
                        }
                        Console.Write(' ' + battlefield_Player[i, j].ToString());
                    }
                    Console.WriteLine();

                }
                if (counter == 5)
                {
                    Console.WriteLine("Battle End");
                    end_Game = true;

                }
                else
                {
                    counter = 0;
                }
                Console.ForegroundColor= ConsoleColor.White;
                
            
        }
        //Pálya megtervezéséért felelős metódus
        public static void Random_Battlefield(char[,] battlefield_Player)
        {
            List<int> random_PlacesX = new List<int>();
            List<int> random_PlacesY = new List<int>();

            for (int i = 0; i < ship_Nums + 1; i++)
            {
                random_PlacesX.Add(rnd.Next(0, 10));
                random_PlacesY.Add(rnd.Next(0, 10));
                counter2++;
            }

            for (int i = 0; i < ship_Nums; i++)
            {
                battlefield_Player[random_PlacesX[i], random_PlacesY[i]] = 'S';

            }
        }
        //Inputok kezeléséért felelős metódus
        public static void Input_PlayerOne()
        {
        back:
            Console.WriteLine("Please give a coordinate");
            user_Input = Console.ReadLine().ToUpper();
            //Bevitt karaktrehossz elemzése
            while (user_Input.Length != 2)
            {
                Console.WriteLine("Not knowning coordinate try again");
                user_Input = Console.ReadLine().ToUpper();
                
            }
            Console.WriteLine("Coordinate lenght is good");
            Console.WriteLine("Input is: " + user_Input);
            
            char[] inputs = user_Input.ToCharArray();
            //koordináta elemzése, hogy egy betű és egy szám legyen
            if (!char.IsNumber(inputs[1]) || !char.IsAsciiLetter(inputs[0]))
            {
                Console.WriteLine("Wrong coordinate");
                goto back;
            }
            //koordináták átalakítása int azaz tömb[0,0]-ként használhatóvá
            int row = inputs[1] - '0';
            int col = letter_Space_ToIndex[inputs[0]];
            //tábla átnézése, hogy a megadott ée leellenőrzött koordináta, találat vagy üres mezőre mutat
            Console.WriteLine($"Row: {row} col: {col}");
            if(battlefield_CPU[row, col] == '~')
            {
                battlefield_CPU[row, col] = 'O';
                battlefield_CPU_Show[row, col] = 'O';
            }
            else if(battlefield_CPU[row, col] == 'S')
            {
                battlefield_CPU[row, col] = 'X';
                battlefield_CPU_Show[row, col] = 'X';
            }
            

        }
        public static void Welcome()
        {
            //Játék köszöntő + név megadása
            Console.Title = "Torpedo";
            
            Console.WriteLine(" HELLO IN TORPEDO BATTLE");
            Thread.Sleep(2000);
            Console.Clear();
        }

        public static void Battle_Start_Counter()
        {
            int start_Battle_Counter = 3;
            //Játék előtti számolás móka kijelzés
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Battle Start in: " + start_Battle_Counter);
                start_Battle_Counter--;
                Thread.Sleep(1000);
                Console.Clear();
            }
            //Pályák megtervezése/felosztása
            Random_Battlefield(battlefield_Player);
            Random_Battlefield(battlefield_CPU);
        }
        public static void Input_PlayerTwo()
        {
        back:
            //Console.WriteLine("Please give a coordinate");
            //user_Input = Console.ReadLine().ToUpper();
            //while (user_Input.Length != 2)
            //{
            //    Console.WriteLine("Not knowning coordinate try again");
            //    user_Input = Console.ReadLine().ToUpper();

            //}
            //Console.WriteLine("Coordinate lenght is good");
            //Console.WriteLine("Input is: " + user_Input);
            //char[] inputs = user_Input.ToCharArray();

            //if (!char.IsNumber(inputs[1]) || !char.IsAsciiLetter(inputs[0]))
            //{
            //    Console.WriteLine("Wrong coordinate");
            //    goto back;
            //}

            //int row = inputs[1] - '0';
            //int col = letter_Space_ToIndex[inputs[0]];
            //random koordináták kezelése CPU által
            int row_CPU = rnd.Next(0, 10);
            int col_CPU = rnd.Next(0, 10);

            Console.WriteLine($"Row: {row_CPU} col: {col_CPU}");
            
            if (battlefield_Player[row_CPU, col_CPU] == '~')
            {
                battlefield_Player[row_CPU, col_CPU] = 'O';
            }
            else if (battlefield_Player[row_CPU, col_CPU] == 'S')
            {
                battlefield_Player[row_CPU, col_CPU] = 'X';
            }

        }

        //user_IINput = Console.ReadLine();
        int mezo = 2;
        //if(mezo == 2)
        //{
        //    Console.WriteLine(battlefield.GetLength(1));
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if(mezo + 4 > 10)
        //        {
        //            if (battlefield.GetLength(1) < (i + 1) + 9)
        //            {
        //                Console.WriteLine("Ez fut1: {0}", i + 9);
        //                Console.WriteLine("I: " + i);
        //                battlefield[1, (i + 9) - 4] = 'S';
        //                BattleField();

        //            }
        //            else
        //            {
        //                Console.WriteLine("Ez fut2: {0}", i + 9);
        //                battlefield[1, (i + 9)] = 'S';
        //                Console.WriteLine("I: " + i);
        //                BattleField();
        //            }
        //        }
        //        else if(mezo - 4 < 0)
        //        {
        //            if (0 > (i - 1) + 2)
        //            {
        //                Console.WriteLine("Ez fut2-1: {0}", i - 2);
        //                Console.WriteLine("I: " + i);
        //                battlefield[1, (i - 2) + 4] = 'S';
        //                BattleField();
        //            }
        //            else
        //            {
        //                Console.WriteLine("Ez fut2-2: {0}", i - 2);
        //                Console.WriteLine("I: " + i);
        //                battlefield[1, (i - 2)] = 'S';
        //                BattleField();
        //            }
        //        }



        //    }
        //}
    }
}
