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
            while (player.NumberOfIncorrectGuesses() < player.maxNumberOfGuesses)
            {                                
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
                        board += '-';
                        charactersLeftToGuess++;
                    }
                }
                
                Console.WriteLine(string.Empty);

                if (!game.TheWord.Contains(playersGuess))
                {
                    player.IncorrectGuess();                    
                }

                Console.WriteLine($"{game.ShowHangman(player.NumberOfIncorrectGuesses())}");

                Console.WriteLine($"Word: {board}");
                               
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

    public int maxNumberOfGuesses { get; }

    private int _incorrectGuess;
    public int NumberOfIncorrectGuesses() => _incorrectGuess;

    public Player(string name, int maxNumberOfGuesses = 6)
    {
        this.name = name;

        this._incorrectGuess = 0;
            
        this.maxNumberOfGuesses = maxNumberOfGuesses;
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
        _incorrectGuess++;           
    }        
}

public class Game
{
    List<string> availableWords = new List<string>() { "beantobar", "coconut", "tufftoffee", "peppermint", "orangecreme", "caramel" };    
    public string TheWord { get; }
    public Game()
    {            
        Random r = new Random();
        int rInt = r.Next(0, 6);            
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

    private List<string> hangManGraphic = new List<string>() { "+---+\r\n  |   |\r\n      |\r\n      |\r\n      |\r\n      |\r\n=========",
                                                               "+---+\r\n  |   |\r\n  O   |\r\n      |\r\n      |\r\n      |\r\n=========",
                                                               "+---+\r\n  |   |\r\n  O   |\r\n  |   |\r\n      |\r\n      |\r\n=========",
                                                               "+---+\r\n  |   |\r\n  O   |\r\n /|   |\r\n      |\r\n      |\r\n=========",
                                                               "+---+\r\n  |   |\r\n  O   |\r\n /|\\  |\r\n      |\r\n      |\r\n=========",
                                                               "+---+\r\n  |   |\r\n  O   |\r\n /|\\  |\r\n /    |\r\n      |\r\n=========",
                                                               "+---+\r\n  |   |\r\n  O   |\r\n /|\\  |\r\n / \\  |\r\n      |\r\n=========",
    };
        
    public string ShowHangman (int pictureNumber)
    {
        int i = pictureNumber;

        if (i < 0)
        {
            i = 0;
        }

        return hangManGraphic[i];
    }

}
