using System.Text.RegularExpressions;

namespace BBHangman
{
    internal class Program
    {
        static void Main(string[] args)
        {                        
            Player player = new Player("Player1");
            Game game = new Game();

            bool playerWon = false;
            bool validInput = false;

            Console.WriteLine($"The word is {game.TheWord}");

            // play continues until the player has run out of lives
            while (player.NumberOfIncorrectGuesses() > 0)
            {                
                bool letterUsed = false;
                string? playersGuess = "";
            
                do
                {
                    Console.WriteLine("Choose a letter");

                    playersGuess = Console.ReadLine().ToLower();


                    validInput = IsUserInputValid(player, playersGuess);
                }
                while (!validInput);
                
                int charactersLeftToGuess = 0;

                string board = "";

                foreach (var character in game.TheWord)
                {
                    var letter = character.ToString();

                    if (player.Guesses.Contains(letter))
                    {
                        board += letter;
                    }
                    else
                    {
                        board += '_';
                        charactersLeftToGuess++;
                    }
                }
                
                Console.WriteLine(string.Empty);

                if (!game.TheWord.Contains(playersGuess))
                {
                    player.IncorrectGuess();
                }

                Console.WriteLine($"Word: {board} | Remaining: {player.NumberOfIncorrectGuesses()}  | Incorrect: | Guess: {playersGuess} ");
                               
                if (charactersLeftToGuess == 0)
                {
                    playerWon = true;
                    break;
                }                                
            }

            game.Results(playerWon, player.name);
        }
        /// <summary>
        /// Series of checks to ensure what the user has entered for a guess
        /// is valid
        /// </summary>
        /// <param name="player"></param>
        /// <param name="input"></param>
        /// <returns>True is all the checks pass otherwise returns false</returns>
        private static bool IsUserInputValid(Player player, string input)
        {
            // each guess can only be one character
            if (input.Length != 1)
            {
                Console.WriteLine("One character at a time please!");
                return false;
            }
            // only letters a - z
            else if (!Regex.IsMatch(input, "[a-z]"))
            {
                Console.WriteLine($"Please enter a letter (a - z)");
                return false;
            }
            // has this letter already been used
            else if (player.LetterAlreadyUsed(input))
            {
                Console.WriteLine("That letter has been used already");
                return false;
            }
            return true;
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

        public void Results(bool playerWon, string playersName)
        {
           if (playerWon)
           {
              Console.WriteLine($"Congratulations {playersName}, you have won");
           }
           else
           {
              Console.WriteLine($"Sorry {playersName} you have lost");
           }
        }
    }
