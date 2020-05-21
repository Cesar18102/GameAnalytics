using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace GameAnalytics_Practice5 {

    class Program {

        static void Main(string[] args) {

            MySqlConnection C = new MySqlConnection("Server=localhost;Database=game_practise5;Uid=root;Pwd=;");
            C.Open();

            MySqlCommand CMD = new MySqlCommand("SELECT * FROM monetize", C);
            MySqlDataAdapter A = new MySqlDataAdapter(CMD);

            DataTable DT = new DataTable();
            A.Fill(DT);

            string[] TopData = DailyReport.TopData();
            int[] TopLs = DailyReport.GetDataLs(TopData);

            List<DailyReport> DReports = DT.Rows.Cast<DataRow>().Select(R => new DailyReport(R)).ToList();

            int[] MaxLs = TopLs;

            foreach (DailyReport L in DReports) {

                int[] Ls = L.GetLs();
                for (int i = 0; i < Ls.Length; i++)
                    MaxLs[i] = Math.Max(Ls[i], MaxLs[i]);
            }

            string Top = DailyReport.TableRow(TopData, MaxLs);
            Console.WriteLine(Top);
            Console.WriteLine(new string('=', Top.Length));

            foreach (DailyReport L in DReports)
                Console.WriteLine(DailyReport.TableRow(L.GetData(), MaxLs));

            Func<DailyReport, double>[] IndicatorSelectors = new Func<DailyReport, double>[]
            {
                R => R.AdvsProfit + R.SellsProfit,
                R => R.ARPU,
                R => R.ARPPU,
                R => R.NewUsers,
                R => R.ConversionRate
            };

            string[] IndicatorNames = new string[] {"Profit", "NewUsers", "_ARPU", "_ARPPU", "ConversionRate" };

            double[] Bads = new double[IndicatorSelectors.Length];
            double[] Goods = new double[IndicatorSelectors.Length];

            Console.WriteLine();
            for (int i = 0; i < IndicatorSelectors.Length && i < IndicatorNames.Length; i++) {

                double MIN = Math.Round(DReports.Select(IndicatorSelectors[i]).Min(), 2);
                double AVG = Math.Round(DReports.Select(IndicatorSelectors[i]).Average(), 2);
                double MAX = Math.Round(DReports.Select(IndicatorSelectors[i]).Max(), 2);

                Bads[i] = AVG - (AVG - MIN) * 0.4;
                Goods[i] = AVG + (MAX - AVG) * 0.4;

                Console.WriteLine(IndicatorNames[i]);
                Console.WriteLine("MIN: " + MIN);
                Console.WriteLine("AVG: " + AVG);
                Console.WriteLine("MAX: " + MAX);
                Console.WriteLine("================================");
            }

            List<int> GoodDaysCounts = new List<int>();
            List<int> BadDaysCounts = new List<int>();
            List<int> SoSoDaysCounts = new List<int>();

            for (int i = 0; i < IndicatorSelectors.Length; i++)
            {

                int GoodCount = 0;
                int BadCount = 0;
                int SoSoCount = 0;

                Console.WriteLine("\n=====================================================\n");
                Console.WriteLine(IndicatorNames[i]);
                Console.WriteLine(Top);
                Console.WriteLine(new string('=', Top.Length));

                foreach (DailyReport L in DReports)
                {
                    if (IndicatorSelectors[i](L) <= Bads[i])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        BadCount++;
                    }
                    else if (IndicatorSelectors[i](L) <= Goods[i])
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        SoSoCount++;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        GoodCount++;
                    }

                    Console.WriteLine(DailyReport.TableRow(L.GetData(), MaxLs));
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine("Bad days count: " + BadCount);
                Console.WriteLine("Good days count: " + GoodCount);
                Console.WriteLine("So-So days count: " + SoSoCount);

                GoodDaysCounts.Add(GoodCount);
                BadDaysCounts.Add(BadCount);
                SoSoDaysCounts.Add(SoSoCount);
            }

            Console.WriteLine("\n=====================================================\n");
            Console.WriteLine("AVG Good days: " + GoodDaysCounts.Average());
            Console.WriteLine("AVG Bad days: " + BadDaysCounts.Average());
            Console.WriteLine("AVG So-so days: " + SoSoDaysCounts.Average());
        }
    }
}
