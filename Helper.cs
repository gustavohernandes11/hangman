namespace hangman.Helper;

public class Helper
{
    public static char AskForChar(string text)
    {
        while (true)
        {
            Console.WriteLine(text);
            ConsoleKeyInfo input = Console.ReadKey();
            char inputChar = input.KeyChar;

            if (Char.IsLetter(inputChar)) return inputChar;
            else Console.WriteLine("Only letters are allowed");

        }
    }
}

