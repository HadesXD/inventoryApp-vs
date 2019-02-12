using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Items
{
    public class SoftwareItem : Item
    {
        public string Size { get; set; }
        public string License { get; set; }
        public string Version { get; set; }

        public SoftwareItem(string itemName, string code, string manufacturer, string price, string size, string license, string version)
            : base(itemName, code, manufacturer, price)
        {
            Size = size;
            License = license;
            Version = version;
        }

        public override string ToString()
        {
            string stringToReturn = "";
            stringToReturn = base.ToString();
            stringToReturn = stringToReturn + ", " + Size + ", " + License + ", " + Version;
            return stringToReturn;
        }

        public int addToDatabase()
        {
            using (SQLiteConnection Conn = new SQLiteConnection("Data Source = database.sqlite"))
            {
                Conn.Open();
                using (SQLiteCommand createCommand = new SQLiteCommand(Conn))
                {
                    int error;

                    createCommand.CommandText = "SELECT * FROM Programs WHERE (name = '" + ItemName + "') OR (code = '" + Code + "')";
                    SQLiteDataReader result = createCommand.ExecuteReader();

                    if (result.HasRows)
                    {
                        createCommand.Dispose();
                        error = 0;
                        Conn.Close();
                    }

                    else
                    {
                        result.Close();
                        error = 1;
                        createCommand.CommandText = "INSERT INTO Programs (name, code, developer, price, memory_size, license, version)" +
                        "VALUES ('" + ItemName + "', '" + Code + "', '" + Manufacturer + "', '" + Price + "', '" + Size + "', '" + License + "', '" + Version + "')";
                        createCommand.ExecuteNonQuery();
                        Conn.Close();
                    }

                    return error;
                }
            }
        }
    }
}
