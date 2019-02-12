using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Items
{
    public class Computer : HardwareItem
    {
        public string NumOfCores { get; set; }
        public string Ram { get; set; }
        public string Disk { get; set; }

        public Computer(string itemName, string code, string manufacturer, string price, string image, string weight,
            string numOfCores, string ram, string disk) : base(itemName, code, manufacturer, price, image, weight)
        {
            NumOfCores = numOfCores;
            Ram = ram;
            Disk = disk;
        }

        public override string ToString()
        {
            string stringToReturn = "";
            stringToReturn = base.ToString();
            stringToReturn = stringToReturn + ", " + NumOfCores + ", " + Ram + ", " + Disk;
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

                    createCommand.CommandText = "SELECT * FROM Computers WHERE (name = '" + ItemName + "') OR (code = '" + Code + "')";
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
                        createCommand.CommandText = "INSERT INTO Computers (name, code, manufacturer, price, image, weight, num_of_cores, ram, disk_size)" +
                        "VALUES ('" + ItemName + "', '" + Code + "', '" + Manufacturer + "', '" + Price + "', '" + Image + "', '" + Weight + "', '" + NumOfCores + "', '" + Ram + "', '" + Disk + "')";
                        createCommand.ExecuteNonQuery();
                        Conn.Close();
                    }

                    return error;
                }
            }
        }
    }
}
