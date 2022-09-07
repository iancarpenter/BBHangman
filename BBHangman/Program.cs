﻿namespace BBHangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
                        
            Player player = new Player("Player1");

            Game game = new Game();
            
            Console.WriteLine($"The word is {game.TheWord}");

            bool playerWon = false;

            // whilst user still has remaining
            while (player.NumberOfIncorrectGuesses() > 0)
            {                
                bool letterUsed = false;
                string? playersGuess = "";
                
                // user has a guess, check to make sure the user hasn't already used this letter
                // if letter has been used ask the user to choose a different one
                do
                {
                    Console.WriteLine("Choose a letter");
                    
                    if (letterUsed)
                    {
                        Console.WriteLine("That letter has been used already");
                    }
                    
                    playersGuess = Console.ReadLine();
                    
                    letterUsed = player.LetterAlreadyUsed(playersGuess);                    
                }
                while (letterUsed);
                
                int charactersLeftToGuess = 0;
            
                foreach (var character in game.TheWord)
                {
                    var letter = character.ToString();
                    
                    if (player.Guesses.Contains(letter))
                    {
                        Console.Write($"{letter}");
                    }
                    else
                    {
                        Console.Write("_");
                        charactersLeftToGuess++;
                    }

                }
                Console.WriteLine(string.Empty);

                if (!game.TheWord.Contains(playersGuess))
                {
                    player.IncorrectGuess();
                }

                // display the board
                //Console.WriteLine($"Word: {game.ShowTheGame(game.TheWord)} | Remaining: {player.NumberOfIncorrectGuesses()}  | Incorrect: | Guess: ");

                Console.WriteLine($"Characters left to guess {charactersLeftToGuess}");
                Console.WriteLine($"The player has {player.NumberOfIncorrectGuesses()} lives remaining");
                if (charactersLeftToGuess == 0)
                {
                    playerWon = true;
                    break;
                }                
                
            }
            if (playerWon)
            {
                Console.WriteLine("Congratulations Player, you have won");
            }
            else
            {
                Console.WriteLine("you have lost!");
            }
        }                        
     }
}

    public class Player
    {
        private List<string> _guesses = new List<string>();
        public List<string> Guesses { get => _guesses; }
        public string name { get; set; }

        private int _numberOfIncorrectGuesses;
        public int NumberOfIncorrectGuesses() => _numberOfIncorrectGuesses;

        public Player(string name, int defaultNumberOfGuesses = 5)
        {
            this.name = name;

            this._numberOfIncorrectGuesses = defaultNumberOfGuesses;
        }

        public bool LetterAlreadyUsed(string letter)
        {
            if (_guesses.Contains(letter) )
            {
                return true;
            }
            else
            {
                _guesses.Add(letter);
                return false;
            }
        }

        public void IncorrectGuess()
        {
            _numberOfIncorrectGuesses--;           
        }
    }

    public class Game
    {
        List<string> availableWords = new List<string>() { "plain", "nut", "milk", "peppermint", "orange", "caramel" };

        public string TheWord { get; }

        public Game()
        {
            
            Random r = new Random();

            int rInt = r.Next(0, 5);
            
            this.TheWord = availableWords[rInt];
        }

        public string ShowTheGame(string word)
        {
            string formatWord = "";

            foreach(int letter in word)
            {
                formatWord += "_ ";
            }

            return formatWord;
        }
                   
    }