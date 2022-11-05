using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryWorks
{
    public static class LabWorkNumb5Task1
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

            float lastInput = float.NaN;

            while (!exit)
            {
                string userInput = Console.ReadLine();

                if (userInput == "q")
                    exit = true;

                if (!IsRealNumberInStr(userInput))
                {
                    Console.WriteLine($"Некорректный ввод");
                    continue;
                }

                if (IsDoubleAsStr(userInput))
                {
                    if (double.Parse(userInput) == lastInput)
                        exit = true;
                }
                else
                {
                    if (int.Parse(userInput) > char.MaxValue || int.Parse(userInput) < char.MinValue)
                        Console.WriteLine("Данное число не может быть выведенно в виде символа юникода");
                    else
                        Console.WriteLine((char)int.Parse(userInput));
                }
                lastInput = float.Parse(userInput);
            }
        }
        private static bool IsRealNumberInStr(string number)
        {
            int positionIndex = 0;
            int pointCounter = 0;
            
            if (number[0] == '-' && number.Length > 2)
            {
                if (!char.IsDigit(number[0 + 1]))
                    return false;

                positionIndex += 2;
            }

            for (; positionIndex < number.Length; positionIndex++)
                if (!char.IsDigit(number[positionIndex]))
                    if ((number[positionIndex] == ',') && (positionIndex != 0) && ((positionIndex + 1) < number.Length))
                        pointCounter++;
                    else
                        return false;

            if (pointCounter > 1)
                return false;

            return true;
        }
        
        private static bool IsDoubleAsStr(string number)
        {
            for (int i = 0; i < number.Length; i++)
                if (number[i] == ',')
                    return true;

            return false;
        }
    }
}
