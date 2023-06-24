
using System.Text.RegularExpressions;
using hangman.Helper;

public class HangmanGame
{
    GameWords CurrentWord { get; set; }
    public string HiddenCurrentWord { get; private set; }
    public List<char> IncorrectLetters { get; private set; }
    public int RemainingTries { get; private set; }

    public HangmanGame()
    {
        HiddenCurrentWord = String.Empty;
        IncorrectLetters = new();
        RemainingTries = 5;
    }

    public void Init()
    {
        CurrentWord = GetRandomWord();
        HiddenCurrentWord = ReplaceAllWithUnderline(CurrentWord);

        DisplayGameIntro();
        DisplayScore();

        while (ShouldGameContinue())
        {
            char charInput = Helper.AskForChar("Guess: ");
            Console.Clear();
            VerifyGuess(charInput);
            DisplayScore();
        }
    }

    void DisplayGameIntro()
    {
        Console.WriteLine($"This is the Hangman game, you have {RemainingTries} tries to guess the hidden word.");
        Console.WriteLine($"The word has {HiddenCurrentWord.Length} letters. Good Luck!");
    }

    void VerifyGuess(char character)
    {
        if (IncorrectLetters.Contains(character))
        {
            Console.WriteLine("This character already has been send");
            return;
        }

        bool ContainsInCurrentWord = CurrentWord.ToString().ToLower().Contains(character);

        if (ContainsInCurrentWord)
        {
            Console.WriteLine("Nice guess!");
            HiddenCurrentWord = RevealCharacterIn(HiddenCurrentWord, character);
            VerifyGame();
            return;
        }
        else
        {
            Console.WriteLine("Wrong guess!");
            IncorrectLetters.Add(character);
            RemainingTries--;
            VerifyGame();
            return;
        }

    }

    void VerifyGame()
    {
        if (!HiddenCurrentWord.Contains("_"))
        {
            Console.WriteLine($"Congratulations, you guessed the word!");
            Console.WriteLine($"The word was {CurrentWord.ToString()}.");
        }
        else if (RemainingTries == 0)
        {
            Console.WriteLine("Failed. Your attempts are over.");
            Console.WriteLine($"The word was {CurrentWord.ToString()}.");
        }

    }

    string RevealCharacterIn(string hiddenWord, char character)
    {
        string newHiddenWorld = HiddenCurrentWord;
        for (int i = 0; i < newHiddenWorld.Length; i++)
        {
            if (CurrentWord.ToString().ToLower()[i] == character)
                newHiddenWorld = newHiddenWorld.Remove(i, 1).Insert(i, character.ToString());

        }
        return newHiddenWorld;
    }

    string CharListToString(List<char> list) =>
        new String(list.ToArray());

    string ReplaceAllWithUnderline(GameWords word) =>
        Regex.Replace(word.ToString(), ".", "_");

    bool ShouldGameContinue() =>
       RemainingTries > 0 && HiddenCurrentWord.Contains("_");

    public void DisplayScore() =>
        Console.WriteLine($"Word: {HiddenCurrentWord} | Remaining tries: {RemainingTries} | Incorrect: {CharListToString(IncorrectLetters)}");

    GameWords GetRandomWord()
    {
        var gameWords = Enum.GetValues(typeof(GameWords)).Cast<GameWords>().ToList();
        var random = new Random();
        return (GameWords)random.Next(gameWords.Count);
    }
}
