using System;

static class ConsoleUtility
{
    public static bool GetInput(out int input, int min, int max)
    {
        while (true) //return이 되기 전까지 반복
        {
            Console.Write("원하시는 행동을 입력해주세요.");

            //int.TryParse는 int로 변환이 가능한지 bool값을 반환, 가능(true)할 경우 out int input으로 숫자도 반환
            if (int.TryParse(Console.ReadLine(), out input) && (input >= min) && (input <= max))
                return true;

            Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
            return false;
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
}
