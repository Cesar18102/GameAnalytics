using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GameAnalytics_Practice2 {

    public class Company {

        public string name { get; private set; }
        public string type { get; private set; }
        public double price { get; private set; }
        public int count { get; private set; }
        public string unit { get; private set; }
        public double CR { get; private set; }
        public double CTR { get; private set; }

        public int Views { get; private set; }
        public int Clicks { get; private set; }
        public int Users { get; private set; }

        public double UserPrice { get; private set; }

        public const int NullCR = -1;
        public const int NullCTR = -1;

        public const int ViewsUnknown = -1;
        public const int ClicksUnknown = -1;
        public const int FieldCount = 11;

        public Company(DataRow R) {

            name = R["name"].ToString();
            type = R["type"].ToString();
            price = Convert.ToDouble(R["price"]);
            count = Convert.ToInt32(R["count"]);
            unit = R["unit"].ToString();
            CR = R["CR"] == DBNull.Value ? NullCR : Convert.ToDouble(R["CR"]);
            CTR = R["CTR"] == DBNull.Value ? NullCTR : Convert.ToDouble(R["CTR"]);

            Views = CTR == NullCTR ? ViewsUnknown : count;
            Clicks = CR == NullCR ? ClicksUnknown : (CTR == NullCTR ? count : (int)(Views * CTR));
            Users = CR == NullCTR ? count : (int)(Clicks * CR);

            UserPrice = Math.Round(price / Users, 2);
        }

        public int[] GetLs() {

            return Company.GetDataLs(GetData());
        }

        public string[] GetData() {

            return new string[] { name, type, price.ToString() + "$", count.ToString(), unit, CR.ToString(), 
                                  CTR.ToString(), Users.ToString(), Clicks.ToString(), Views.ToString(), UserPrice.ToString() + "$" };
        }

        public static string TableRow(string[] Data, int[] MaxLs) {

            string Result = "";
            for (int i = 0; i < Data.Length && i < MaxLs.Length; i++)
                Result += Data[i] + new String(' ', MaxLs[i] - Data[i].Length) + " | ";

            return Result.Substring(0, Result.Length - 1);
        }

        public static string[] TopData() {

            return new string[] { "NAME", "TYPE", "PRICE", "COUNT", "UNIT", "CONVERSION RATE", "CTR", "USERS", "CLICKS", "VIEWS", "USER PRICE" };
        }

        public static int[] GetDataLs(string[] Data) {

            int[] Ls = new int[Data.Length];
            for (int i = 0; i < Data.Length; i++)
                Ls[i] = Data[i].Length;
            return Ls;
        }
    }
}
