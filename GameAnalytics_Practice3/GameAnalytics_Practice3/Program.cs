using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace GameAnalytics_Practice3
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection C = new MySqlConnection("Server=localhost;Database=game_practise3;Uid=root;Pwd=;");
            C.Open();

            MySqlCommand CMD = new MySqlCommand("SELECT * FROM level", C);

            MySqlDataAdapter A = new MySqlDataAdapter(CMD);

            DataTable DT = new DataTable();
            A.Fill(DT);

            string[] TopData = Level.TopData();
            int[] TopLs = Level.GetDataLs(TopData);

            List<Level> CMPS = DT.Rows.Cast<DataRow>()
                                   .Select(R => new Level(R))
                                   .OrderBy(R => R.Hardness)
                                   .ToList();

            int DeleteCount = (int)(CMPS.Count * 0.1);
            for (int i = 0; i < DeleteCount; i++) {

                CMPS.RemoveAt(0);
                CMPS.RemoveAt(CMPS.Count - 1);
            }

            int SectorsCount = 5;
            int SectorLength = CMPS.Count / SectorsCount;

            List<Level> LB = new List<Level>();

            for (int i = 0; i < SectorsCount; i++) {

                int start = SectorLength * i;
                int end = i == SectorsCount - 1 ? CMPS.Count : SectorLength * (i + 1);
                int mid = start + (end - start) / 2;

                for (int j = start; j < mid; j++)
                    LB.Add(CMPS[j]);

                for(int j = end - 1; j >= mid; j--)
                    LB.Add(CMPS[j]);
            }

            int[] MaxLs = TopLs;

            foreach (Level L in LB) {

                int[] Ls = L.GetLs();
                for (int i = 0; i < Ls.Length; i++)
                    MaxLs[i] = Math.Max(Ls[i], MaxLs[i]);
            }

            string Top = Level.TableRow(TopData, MaxLs);
            Console.WriteLine(Top);
            Console.WriteLine(new string('=', Top.Length));

            foreach (Level L in LB)
                Console.WriteLine(Level.TableRow(L.GetData(), MaxLs));

            //List<double> Hardnesses = LB.Select(L => L.Hardness).ToList();

            //for (int i = 0; i < Hardnesses.Count; i++)
            //    Console.WriteLine(i + ";" + Hardnesses[i].ToString().Replace(",", "."));

            C.Close();
            Console.ReadLine();
        }
    }
}
