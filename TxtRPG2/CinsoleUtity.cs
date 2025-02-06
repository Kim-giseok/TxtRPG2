using System;

static class ConsoleUtility
{
    
    //글자 색 변경
    public static void ColorWrite(string str, ConsoleColor color)
    {
        Console.ForegroundColor = color; //텍스트 컬러 설정
        Console.WriteLine(str);
        Console.ResetColor();
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
}
