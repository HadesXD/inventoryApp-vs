using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Items
{
    public class Monitor : HardwareItem
    {
        public string Resolution { get; set; }
        public string AspectRatio { get; set; }
        public string MonitorType { get; set; }

        public Monitor(string itemName, string code, string manufacturer, string price, string image, string weight,
            string resolution, string aspectRatio, string monitorType) : base(itemName, code, manufacturer, price, image, weight)
        {
            Resolution = resolution;
            AspectRatio = aspectRatio;
            MonitorType = monitorType;
        }

        public override string ToString()
        {
            string stringToReturn = "";
            stringToReturn = base.ToString();
            stringToReturn = stringToReturn + ", " + Resolution + ", " + AspectRatio + ", " + MonitorType;
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

                    createCommand.CommandText = "SELECT * FROM Monitors WHERE (name = '" + ItemName + "') OR (code = '" + Code + "')";
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
                        createCommand.CommandText = "INSERT INTO Monitors (name, code, manufacturer, price, image, weight, resolution, aspect_ratio, monitor_type)" +
                        "VALUES ('" + ItemName + "', '" + Code + "', '" + Manufacturer + "', '" + Price + "', '" + Image + "', '" + Weight + "', '" + Resolution + "', '" + AspectRatio + "', '" + MonitorType + "')";
                        createCommand.ExecuteNonQuery();
                        Conn.Close();
                    }

                    return error;
                }
            }
        }
    }
}
