using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace GameAnalytics_Practice2 {

    class Program {

        static void Main(string[] args) {

            MySqlConnection C = new MySqlConnection("Server=localhost;Database=companies_practise2;Uid=root;Pwd=;");
            C.Open();

            MySqlCommand CMD = new MySqlCommand("SELECT C.name AS name, T.name AS type, C.price AS price, C.cnt AS count, U.name AS unit, C.conversion_rate AS CR, C.CTR AS CTR " + 
                                                "FROM company C, type T, unit U " + 
                                                "WHERE C.type_id = T.id AND T.unit_id = U.id", C);

            MySqlDataAdapter A = new MySqlDataAdapter(CMD);

            DataTable DT = new DataTable();
            A.Fill(DT);

            string[] TopData = Company.TopData();
            int[] TopLs = Company.GetDataLs(TopData);

            List<Company> CMPS = DT.Rows.Cast<DataRow>()
                                   .Select(R => new Company(R))
                                   .OrderByDescending(CMP => CMP.Users)
                                   .ToList();
            int[] MaxLs = TopLs;

            foreach(Company CMP in CMPS) {

                int[] Ls = CMP.GetLs();
                for(int i = 0; i < Ls.Length; i++)
                    MaxLs[i] = Math.Max(Ls[i], MaxLs[i]);
            }

            string Top = Company.TableRow(TopData, MaxLs);
            Console.WriteLine(Top);
            Console.WriteLine(new string('=', Top.Length));

            foreach (Company CMP in CMPS)
                Console.WriteLine(Company.TableRow(CMP.GetData(), MaxLs).Replace("-1", "--").Replace("і", "и"));

            C.Close();
            Console.ReadLine();
        }
    }
}
