using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GameAnalytics_Practice3
{
    public class Level {

        public string Name { get; private set; }
        public int Starts { get; private set; }
        public int Wins { get; private set; }
        public int AlmostWins { get; private set; }

        public int Loses { get; private set; }
        public double Hardness { get; private set; }
        public double EmotionFactor { get; private set; }

        public Level(DataRow DR) {

            Name = DR["name"].ToString();
            Starts = Convert.ToInt32(DR["starts"]);
            Wins = Convert.ToInt32(DR["wins"]);
            AlmostWins = Convert.ToInt32(DR["almost_wins"]);

            Loses = Starts - Wins - AlmostWins;
            Hardness = Math.Round((double)Loses * 100 / Starts, 2);
            EmotionFactor = Math.Round((double)AlmostWins / Loses, 2);
        }

        public int[] GetLs() {

            return Level.GetDataLs(GetData());
        }

        public string[] GetData() {

            return new string[] { Name, Starts.ToString(), Wins.ToString(), AlmostWins.ToString(), Loses.ToString(), Hardness.ToString() + "%", EmotionFactor.ToString() + "%" };
        }

        public static string TableRow(string[] Data, int[] MaxLs) {

            string Result = "";
            for (int i = 0; i < Data.Length && i < MaxLs.Length; i++)
                Result += Data[i] + new String(' ', MaxLs[i] - Data[i].Length) + " | ";

            return Result.Substring(0, Result.Length - 1);
        }

        public static string[] TopData() {

            return new string[] { "NAME", "STARTS", "WINS", "ALMOST WINS", "LOSES", "HARDNESS", "EMOTION FACTOR" };
        }

        public static int[] GetDataLs(string[] Data) {

            int[] Ls = new int[Data.Length];
            for (int i = 0; i < Data.Length; i++)
                Ls[i] = Data[i].Length;
            return Ls;
        }
    }
}
