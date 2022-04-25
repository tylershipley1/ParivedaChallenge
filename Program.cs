using System;
using System.IO;

namespace ParivedaChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            int wins=0;
            int losses=0;
            Console.Clear();
            string input = Menu();
            Console.ForegroundColor = ConsoleColor.White;
            while(input!="4")
            {
            if(input=="1")
            {
            Console.Clear();
            string word = GetWord().ToLower();
            char[] chosenWord = MakeChar(word);
            CheckCorrect(chosenWord, word, ref wins, ref losses);
            }
            else if(input=="2")
            {
            Rules();
            }
            else if(input=="3")
            {
                Console.Clear();
                Console.WriteLine($"You have {wins} win(s) and {losses} loss(es).");
                Console.WriteLine("Press any Key to return to Menu...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid Input. Please enter 1, 2, 3, or 4 for your choice. Press any Key to go back to Menu...");
                Console.ReadKey();
                Console.Clear();
            }
            input = Menu();
            }
            Console.Clear();
            Console.WriteLine("Thank you for playing!");
            Console.WriteLine($"You left with {wins} win(s) and {losses} loss(es).");


        }

        static void Rules()
        {
        Console.Clear();
        Console.WriteLine($"Welcome to CODLE!\nIn this game, a random 5 letter word is chosen out of the top 210 most common 5 letter nouns.\nThe player has 6 attemtps to discover the word.\nIf the letter is correct, the letter turns green.\nIf the letter is in the word, but not in the same spot, it turns yellow.\nIf a letter does not appear, it is not in the word.\nGood Luck!\nPress any Key to return to menu...");
        Console.ReadKey();
        Console.Clear();
        }

        static string Menu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("C");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("O");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("D");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("E");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("L");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("E");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"\n\n1) Play\n2) Rules\n3) Score\n4) Quit\n\nEnter your Choice:");
            return Console.ReadLine();

        }

        static int Randomize(int min, int max)
        {
            Random rnd = new Random();
            int number=rnd.Next(min,max+1);
            return number;
        }

        static string GetWord()
        {
            int count=0;
            string[] Words = new string[1000];
            StreamReader inFile = new StreamReader("wordpool.txt");
            string word = inFile.ReadLine();
            while(word!=null & word!="used")
            {
            Words[count]= word;
            count++;
            word = inFile.ReadLine();
            }
            return ChooseWord(Words, count);

        }

        static string ChooseWord(string[] Words, int count)
        {
            int choiceNum = Randomize(0,count);
            return Words[choiceNum];
        }

        
        static char[] MakeChar(string word)
        {
            char[] chosen = new char[5];
            for(int i=0;i<5;i++)
            {
                chosen[i] = word[i];
            }
            return chosen;
        }

        static void CheckCorrect(char[] chosen, string word, ref int wins, ref int losses)
        {
            char[] yellow = new char[5];
            for(int o=0;o<yellow.Length; o++)
            {
                yellow[o]='#';
            }
            int counter=0;
            int numCorrect=0;
            while(counter<6&&numCorrect<5)
            {
            string guess = ShowCurrent(chosen, word, counter, yellow);
            for(int m=0;m<yellow.Length; m++)
            {
                yellow[m]='#';
            }
            char[] guessChar = MakeChar(guess);
            for(int x= 0; x<5; x++)
            {
                for(int j= 0; j<5; j++)
                {
                    if(guessChar[x]==chosen[j])
                    {
                        if(x==j)
                        {
                            chosen[x]='!';
                            numCorrect++;
                        }
                        else
                        {
                            yellow[x]=guessChar[x];
                        }
                    }
                }
            }
            counter++;
            }
            if(numCorrect==5)
            {
                Console.WriteLine();
                Console.Write($"Congrats, you guessed ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(word);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" correctly!");
                wins++;
            }
            else
            {
                Console.WriteLine();
                Console.Write($"Sorry, you didn't guess it. The word was ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(word);
                Console.ForegroundColor = ConsoleColor.White;
                losses++;
            }
            Console.WriteLine();
            Console.WriteLine($"You now have {wins} win(s) and {losses} loss(es).");
            Console.WriteLine($"\nPress any key to return to Menu...");
            Console.ReadKey();
            Console.Clear();
            
        }

        static string ShowCurrent(char[] chosen, string word, int q, char[] yellow)
        {
            Console.WriteLine($"\t\t\t\t\t\t\t\t\tNumber of Attempts: {q}");
            for(int i=0; i<5;i++)
            {
                if(chosen[i]=='!')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{word[i]} ");
                    Console.ForegroundColor = ConsoleColor.White;

                }
                else if(yellow[i]!='#')
                {

                   Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{yellow[i]} ");
                    Console.ForegroundColor = ConsoleColor.White;   
                }
                else
                {
                    Console.Write("_ ");
                }
            }
        Console.WriteLine();
        Console.Write("Enter your guess: ");
        string guess = Console.ReadLine().ToLower();
        for(int i=0;i<6;i++)
        {
            if(i<5)
            {
                if(guess.Length!=5)
                {
                    while(guess.Length!=5)
                    {
                    Console.WriteLine("Invalid Guess, Please enter a 5 letter word:");
                    guess = Console.ReadLine();
                    }
                    
                    
                }
            }
        }
        return guess;
        }

    }
}
