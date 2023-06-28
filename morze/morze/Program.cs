

internal class Program
{
    static void Main(string[] args)
    {
        Dictionary<char, string> morseAlphabet = new Dictionary<char, string>()
        {
            {'А', ".-"},
            {'Б', "-..."},
            {'В', ".--"},
            {'Г', "--."},
            {'Д', "-.."},
            {'Е', "."},
            {'Ё', "."},
            {'Ж', "...-"},
            {'З', "--.."},
            {'И', ".."},
            {'Й', ".---"},
            {'К', "-.-"},
            {'Л', ".-.."},
            {'М', "--"},
            {'Н', "-."},
            {'О', "---"},
            {'П', ".--."},
            {'Р', ".-."},
            {'С', "..."},
            {'Т', "-"},
            {'У', "..-"},
            {'Ф', "..-."},
            {'Х', "...."},
            {'Ц', "-.-."},
            {'Ч', "---."},
            {'Ш', "----"},
            {'Щ', "--.-"},
            {'Ъ', ".--.-."},
            {'Ы', "-.--"},
            {'Ь', "-..-"},
            {'Э', "..-.."},
            {'Ю', "..--"},
            {'Я', ".-.-"}
        };

        Console.WriteLine("Выберите режим работы\n");
        Console.WriteLine("1-шифровка\n");
        Console.WriteLine("2-дешифровка\n");

        string choice = Console.ReadLine();

        if (choice == "1"){
            Console.WriteLine("шифровка >>> ");
            string input = Console.ReadLine().ToUpper(); // приводим к верхнему регистру


            foreach (char c in input)
            {
                if (morseAlphabet.ContainsKey(c))
                {
                    Console.Write(morseAlphabet[c] + "|");
                }
                else if (c == ' ')
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(c);
                    Console.ResetColor();

                }
            }

        }
        else if (choice == "2"){
            Console.WriteLine("2-дешифровка\n");
        }
                      


    }

}
