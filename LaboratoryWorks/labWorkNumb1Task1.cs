using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryWorks
{
    public static class LabWorkNumb1Task1
    {
        /*
        Пользователь вводит число. Если это целочисленное число (int.TryParse(...)), 
            то вывести на экран символ соответствующий этому числу. 
        Если это число с плавающей запятой, то сравнить это число с предыдущим введенным (float.NaN). 
        Если эти числа равны, прекратить ввод, иначе повторить ввод. 
        Также выполнение программы прекращается, если ввести символ q.
         */

        public static void PrintProcesedInput()
        {
            bool exit = false;

            double lastInput = double.NaN;

            while (!exit)
            {
                string userInput = Console.ReadLine();

                if (userInput == "q")
                    return;

                if (!IsRealNumberInStr(userInput))
                {
                    Console.WriteLine($"Некорректный ввод");
                    continue;
                }

                if (IsDoubleInStr(userInput))
                {
                    if (double.Parse(userInput) == lastInput)
                        exit = true;
                }
                else
                {
                    if (int.Parse(userInput) > char.MaxValue || (char)int.Parse(userInput) < char.MinValue)
                        Console.WriteLine("Данное число не может быть выведенно в виде символа юникода");
                    else
                        Console.WriteLine((char)int.Parse(userInput));

                }
                lastInput = double.Parse(userInput);
            }
        }
        private static bool IsRealNumberInStr(string number)
        {
            int posIndex = 0;
            int pointCounter = 0;
            
            if (number[0] == '-' && number.Length > 1)
            {
                if (!char.IsDigit(number[0 + 1]))
                    return false;

                posIndex += 2;
            }

            for (; posIndex < number.Length; posIndex++)
                if (!char.IsDigit(number[posIndex]))
                    if (number[posIndex] == ',')
                        pointCounter++;
                    else
                        return false;

            if (pointCounter > 1)
                return false;

            return true;
        }
        
        private static bool IsDoubleInStr(string number)
        {
            for (int i = 0; i < number.Length; i++)
                if (number[i] == ',')
                    return true;

            return false;
        }
    }
}
