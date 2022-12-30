using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryWorks
{
    public class LabWorkNumb5Task4
    {
        /*
        Пользователь вводит массив. Числа могут быть положительными, отрицательными, целыми или дробными. 
        Без изменения оригинального массива заменить все положительные целые числа в массиве на их факториал. 
        Отрицательные целые числа оставить без изменений. 
        Любое дробное число заменить на его дробную часть с откинутой целой частью, округленную до сотых. 
        Например, если число равно -12.067, то его нужно заменить на число 7 (округлили число -12.067 до сотых - получили -12.07, откинули целую часть - получили 7).

        Вывести для сравнения оригинальный массив, который не должен был поменяться, и результирующий массив.
        */
        public static void PrintProcesedInput(double[] userNumbers = null)
        {
            //double[] userNumbers = GetUserInput();
            double[] convertedUserNumbers = new double[userNumbers.Length];

            for(int i = 0; i < userNumbers.Length; i++)
            {
                if (userNumbers[i] - (int)userNumbers[i] > 0)
                    convertedUserNumbers[i] = Math.Round(userNumbers[i] - (int)userNumbers[i], 3) * 1000;
                else if (userNumbers[i] > 0)
                    convertedUserNumbers[i] = Fact(userNumbers[i]);
                else
                    convertedUserNumbers[i] = userNumbers[i];
            }

            for(int i = 0; i < convertedUserNumbers.Length; i++)
                Console.WriteLine($"User [{userNumbers[i]}], converted [{convertedUserNumbers[i]}]");
        }
        public static double Fact(double number)
        {
            if (number == 0)
                return 1;
            else
                return number * Fact(number - 1);
        }
        public static double[] GetUserInput()
        {
            Console.WriteLine("Write q for out");

            string input = "";
            List<double> userNumbres = new List<double>();

            while (input != "q")
            {
                input = Console.ReadLine();

                if(IsStrNumberCorrect(input, out _))
                    userNumbres.Add(Convert.ToDouble(input));
            }

            return userNumbres.ToArray();
        }
        private static bool IsStrNumberCorrect(string strNumber, out bool isDouble)
        {
            int positionIndex = 0;

            isDouble = false;

            if (strNumber[0] == '-' && strNumber.Length > 1)
            {
                if (!char.IsDigit(strNumber[0 + 1]))
                    return false;

                positionIndex += 2;
            }

            for (; positionIndex < strNumber.Length; positionIndex++)
                if (!char.IsDigit(strNumber[positionIndex]))
                    if (((strNumber[positionIndex] == ',') || (strNumber[positionIndex] == '.')) 
                        && (positionIndex != 0) 
                        && ((positionIndex + 1) < strNumber.Length) 
                        && !isDouble)

                        isDouble = true;
                    else
                        return false;

            if (isDouble)
                return double.TryParse(strNumber, out _);
            else
                return int.TryParse(strNumber, out _);
        }
    }
}
