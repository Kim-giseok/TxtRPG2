using System;

static class ConsoleUtility
{
    public static void ClearCurrentConsoleLine()
    {
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }
    public static int GetInput(int min, int max)
    {
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        bool alert = false;

        while (true) //return이 되기 전까지 반복
        {

            //int.TryParse는 int로 변환이 가능한지 bool값을 반환, 가능(true)할 경우 out int input으로 숫자도 반환
            if (int.TryParse(Console.ReadLine(), out int input) && (input >= min) && (input <= max))
                return input;

            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            if (alert == false)
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                alert = true;
            }
        }
    }

    //로딩 화면
    public static void Loading()
    {
        Console.Clear();
        Console.Write("Loading");
        string str = ".";

        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(75); //시간을 지연시킴
            Console.Write(str);
        }
    }

    public static void Write(string str, ConsoleColor color = ConsoleColor.White)
    {
        if (color == ConsoleColor.White)
        {
            color = Console.ForegroundColor;
        }
        Console.ForegroundColor = color;
        Console.Write(str);
        Console.ResetColor();
    }

    public static void WritLine(string str, ConsoleColor color = ConsoleColor.White)
    {
        Write(str, color);
        Console.WriteLine();
    }
}