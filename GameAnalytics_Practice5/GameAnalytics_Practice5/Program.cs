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
        }
    }
}
