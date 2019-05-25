using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GameAnalytics_Practice5 {

    public class DailyReport {

        public DateTime Date { get; private set; }
        public double SellsProfit { get; private set; }
        public double AdvsProfit { get; private set; }
        public int DAU { get; private set; }
        public int MAU { get; private set; }
        public int LAU { get; private set; }
        public int NewUsers { get; private set; }
        public int PAU { get; private set; }
        public int NetworkPostsCount { get; private set; }
        public double AvgSessionTime { get; private set; }
        public double AvgSessionCount { get; private set; }

        public double Revenue { get; private set; }
        public double ARPU { get; private set; }
        public double ARPPU { get; private set; }
        public double Involvement { get; private set; }
        public double ConversionRate { get; private set; }

        public DailyReport(DataRow DR) {

            Date = Convert.ToDateTime(DR["date"]);
            SellsProfit = Convert.ToDouble(DR["profit_sells"]);
            AdvsProfit = Convert.ToDouble(DR["profit_advs"]);

            DAU = Convert.ToInt32(DR["DAU"]);
            MAU = Convert.ToInt32(DR["MAU"]);
            LAU = Convert.ToInt32(DR["LAU"]);
            PAU = Convert.ToInt32(DR["PAU"]);

            NewUsers = Convert.ToInt32(DR["new_users"]);
            NetworkPostsCount = Convert.ToInt32(DR["network_posts"]);

            AvgSessionTime = Convert.ToDouble(DR["avg_session_time"]);
            AvgSessionCount = Convert.ToDouble(DR["avg_session_count"]);

            Revenue = SellsProfit + AdvsProfit;
            ARPU = Math.Round(Revenue / DAU, 2);
            ARPPU = Math.Round(Revenue / PAU, 2);
            Involvement = Math.Round((double)DAU / MAU, 2);
            ConversionRate = Math.Round((double)PAU / DAU, 2);
        }

        public int[] GetLs() {

            return DailyReport.GetDataLs(GetData());
        }

        public string[] GetData() {

            return new string[] { Date.ToShortDateString(), SellsProfit.ToString() + "$", AdvsProfit.ToString() + "$",
                                  DAU.ToString(), MAU.ToString(), LAU.ToString(), NewUsers.ToString(), PAU.ToString(),
                                  NetworkPostsCount.ToString(), AvgSessionTime.ToString(), AvgSessionCount.ToString(),
                                  ARPU.ToString(), ARPPU.ToString(), Involvement.ToString() + "%", ConversionRate.ToString() + "%" };
        }

        public static string TableRow(string[] Data, int[] MaxLs) {

            string Result = "";
            for (int i = 0; i < Data.Length && i < MaxLs.Length; i++)
                Result += Data[i] + new String(' ', MaxLs[i] - Data[i].Length) + "|";

            return Result.Substring(0, Result.Length - 1);
        }

        public static string[] TopData() {

            return new string[] { "DT", "SELL PRFT", "ADV PRFT", "DAU", "MAU", "LAU", "NEW USR", "PAU", "POSTS", 
                                  "AVG SESS TM", "AVG SESS CNT", "ARPU", "ARPPU", "INV-MENT", "CNVRS RATE" };
        }

        public static int[] GetDataLs(string[] Data) {

            int[] Ls = new int[Data.Length];
            for (int i = 0; i < Data.Length; i++)
                Ls[i] = Data[i].Length;
            return Ls;
        }
    }
}
