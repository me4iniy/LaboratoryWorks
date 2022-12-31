using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryWorks
{
    public static class LabWorkN8TaskAnagrams
    {
        public static void GetNewAnagrams()
        {
            //string[] wordsForCheck = { "code", "doce", "ecod", "framer", "frame" };

            string[] uniqueAnagrams = GetUniqueAnagram(GetUserAnagrams());

            foreach (var anagram in uniqueAnagrams)
                Console.Write(anagram + " ");
        }
        private static string[] GetUserAnagrams()
        {
            Console.WriteLine("Write q for out");
            List<string> userAnagrams = new();

            string userInput = "";

            while (userInput != "q")
            {
                userAnagrams.Add(Console.ReadLine());
            }

            return userAnagrams.ToArray();
        }

        private static string[] GetUniqueAnagram(string[] wordsForCheck)
        {
            List<string> uniqueAnagrams = new List<string>();

            for (var i = 0; i < wordsForCheck.Length; i++)
            {
                bool isAnagram = false;

                char[] mainLeters = wordsForCheck[i].ToCharArray();
                Array.Sort(mainLeters);

                for (var j = i - 1; j >= 0; j--)
                {
                    char[] compareLeters = wordsForCheck[j].ToCharArray();
                    Array.Sort(compareLeters);

                    isAnagram = Enumerable.SequenceEqual(mainLeters, compareLeters);

                    if (isAnagram) 
                        break;
                }

                if (isAnagram) 
                    continue;

                uniqueAnagrams.Add(wordsForCheck[i]);
            }

            uniqueAnagrams.Sort();

            return uniqueAnagrams.ToArray();
        }
    }
}
