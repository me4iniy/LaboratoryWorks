using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryWorks
{
    public static class LabWorkN8TaskStatement
    {
        private static int StartedMoney = 0;
        public static void StartUserTest()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "statement1.txt");

            Console.WriteLine("Введите время для проверки");
            int tempMoney = GetResult(ReadFile(path), Console.ReadLine());

            Console.WriteLine(tempMoney < 0 ? "Файл не корректен" : tempMoney);
        }
        
        private static int GetResult(List<Operation> operationsList, string userDate)
        {
            if (userDate == "")
                throw new NotSupportedException();

            DateTime checkDate = DateTime.Parse(userDate, System.Globalization.CultureInfo.InvariantCulture);

            bool Revert = false;
            int SumOfMoney = 0;

            for (var i = operationsList.Count-1; i >= 0; i--)
            {
                if (operationsList[i].OperationTime > checkDate) 
                    continue;

                if (Revert)
                {
                    Revert = false;
                    continue;
                }

                if(operationsList[i].Type == Operation.OperationType.Revert)
                {
                    Revert = true;
                    continue;
                }

                if(operationsList[i].Type == Operation.OperationType.In)
                    SumOfMoney += operationsList[i].Money;
                else
                    SumOfMoney -= operationsList[i].Money;
            }

            return SumOfMoney + StartedMoney;
        }

        private static List<Operation> ReadFile(string path)
        {
            StreamReader reader = new StreamReader(path);

            List<Operation> operationsList = new List<Operation>();
            string tempOperationLine;

            using (reader)
            {
                while ((tempOperationLine = reader.ReadLine()) != null)
                {
                    string[] tempData = tempOperationLine.Split(" | ");

                    if (tempData.Length == 1)
                        StartedMoney = int.Parse(tempData[0]);
                    else if (tempData.Length == 3) 
                        AddNewOperationInList(operationsList, new Operation(tempData[2], tempData[1], tempData[0]));
                    else if (tempData.Length == 2)
                        AddNewOperationInList(operationsList, new Operation(Operation.OperationType.Revert, tempData[0]));

                }
            }

            return operationsList;
        }
        private static void AddNewOperationInList(List<Operation> operationsList, Operation newOperation)
        {
            for(int i = 0; i < operationsList.Count; i++)
            {
                if(operationsList[i].OperationTime >= newOperation.OperationTime)
                {
                    operationsList.Insert(i, newOperation);
                    return;
                } 
            }

            operationsList.Add(newOperation);
        }
        private class Operation
        {
            public enum OperationType { In, Out, Revert }
            public OperationType Type { get; set; }
            public int Money { get; set; }
            public DateTime OperationTime { get; set; }

            public Operation(string type, string money, string operationTime)
            {
                Type = type == "in" ? OperationType.In : OperationType.Out;
                Money = int.Parse(money);
                OperationTime = DateTime.Parse(operationTime, System.Globalization.CultureInfo.InvariantCulture);
            }
            public Operation(OperationType type, string operationTime)
            {
                Type = type;
                Money = 0;
                OperationTime = DateTime.Parse(operationTime, System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}
