using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static LaboratoryWorks.LabWorkN8TaskSubtitles;
using static System.Net.Mime.MediaTypeNames;
using Timer = System.Timers.Timer;

namespace LaboratoryWorks
{
    public static class LabWorkN8TaskSubtitles
    {
        private static Subtitle[] _ActiveSubtitles = new Subtitle[4] { new Subtitle(), new Subtitle(), new Subtitle(), new Subtitle() };
        private static Subtitle[] _ListOfAllSubtitles = ReadSubtitlesFromFile();

        private static int _Seconds = 0;
        private static Timer Timer = new Timer(1000);

        public static void StartTimerWithSubtitles()
        {
            Timer.Elapsed += UpdateScreenDataEvent;

            Timer.Enabled = true;
            Timer.AutoReset = true;
            Timer.Start();

            Console.ReadKey();
        }

        private static void UpdateScreenDataEvent(object sender, ElapsedEventArgs args)
        {
            for (int i = 0; i < _ListOfAllSubtitles.Length; i++) 
            {
                if (_ListOfAllSubtitles[i].StartTime == _Seconds)
                    _ActiveSubtitles[_ListOfAllSubtitles[i].Place] = _ListOfAllSubtitles[i];

                if(_ListOfAllSubtitles[i].EndTime == _Seconds)
                    _ActiveSubtitles[_ListOfAllSubtitles[i].Place] = new Subtitle();
            }

            DrawScreen();
        }

        public static void DrawScreen()
        {
            Console.Clear();

            Console.WriteLine($"Time: {_Seconds++}");
            Console.Write("|----------------------------------------|");

            WriteSubtitleOnCenter(_ActiveSubtitles[0]);
            WriteEmptySubtitle();
            WriteSubtitleOnRightAndLeft(_ActiveSubtitles[1], _ActiveSubtitles[2]);
            WriteEmptySubtitle();
            WriteSubtitleOnCenter(_ActiveSubtitles[3]);

            Console.WriteLine("\n|----------------------------------------|");
        }
        private static void WriteEmptySubtitle()
        {
            Console.Write("\n|");
            WriteSpaces(40);
            Console.Write("|");
        }
        private static void WriteSubtitleOnCenter(Subtitle subtitle)
        {
            Console.Write("\n|");

            WriteSpaces((int)Math.Ceiling((double)(40 - subtitle.Text.Length) / 2));
            WriteSubtitle(subtitle);
            WriteSpaces((40 - subtitle.Text.Length) / 2);

            Console.Write("|");
        }
        private static void WriteSubtitleOnRightAndLeft(Subtitle subtitleLeft, Subtitle subtitleRight)
        {
            Console.Write("\n|");

            WriteSubtitle(subtitleLeft);
            WriteSpaces((40 - subtitleLeft.Text.Length - subtitleRight.Text.Length));
            WriteSubtitle(subtitleRight);

            Console.Write("|");
        }

        public static void WriteSpaces(int countOfSpaces) => Console.Write(string.Concat(Enumerable.Repeat(" ", countOfSpaces)));

        public static void WriteSubtitle(Subtitle subtitle)
        {
            Console.ForegroundColor = subtitle.Color;
            Console.Write(subtitle.Text);

            Console.ResetColor();
        }

        public static Subtitle[] ReadSubtitlesFromFile()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "subtitles.txt");
            StreamReader reader = new StreamReader(path);

            List<Subtitle> allData = new List<Subtitle>();

            string tempLineOfText;

            while ((tempLineOfText = reader.ReadLine()) != null)
            {
                tempLineOfText = tempLineOfText.Replace(" - ", "|");

                if (!tempLineOfText.Contains(" ["))
                    tempLineOfText = tempLineOfText.Insert(12, "[Bottom, White] ");

                tempLineOfText = tempLineOfText.Replace(" [", "|").Replace("] ", "|").Replace(", ", "|");

                string[] tempSubtitle = tempLineOfText.Split("|");

                allData.Add(new Subtitle(tempSubtitle[0], tempSubtitle[1], tempSubtitle[2], tempSubtitle[3], tempSubtitle[4]));
            }

            reader.Close();

            return allData.ToArray();
        }
        public class Subtitle
        {
            public int StartTime { get; set; }
            public int EndTime { get; set; }
            public int Place { get; set; }
            public ConsoleColor Color { get; set; }
            public string Text { get; set; }

            public Subtitle(string startTime, string endTime, string place, string color, string text)
            {
                this.StartTime = (int)(TimeSpan.Parse(startTime)).TotalSeconds / 60;
                this.EndTime = (int)(TimeSpan.Parse(endTime)).TotalSeconds / 60;

                this.Place = place switch
                {
                    "Top" => 0,
                    "Left" => 1,
                    "Right" => 2,
                    "Bottom" => 3,
                    _ => throw new NotSupportedException()
                };

                this.Color = color switch
                {
                    "Red" => ConsoleColor.Red,
                    "Green" => ConsoleColor.Green,
                    "Blue" => ConsoleColor.Blue,
                    _ => ConsoleColor.White
                };

                this.Text = text;
            }
            //def subtitle
            public Subtitle()
            {
                this.StartTime = -1;
                this.EndTime = -1;
                this.Place = -1;
                this.Color = ConsoleColor.White;
                this.Text = String.Empty;
            }
        }
    }
}
