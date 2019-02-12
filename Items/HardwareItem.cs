using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Items
{
    public class HardwareItem : Item
    {
        public string Image { get; set; }
        public string Weight { get; set; }

        public HardwareItem(string itemName, string code, string manufacturer, string price, string image, string weight)
            : base(itemName, code, manufacturer, price)
        {
            Image = image;
            Weight = weight;
        }

        public override string ToString()
        {
            string stringToReturn = "";
            stringToReturn = base.ToString();
            stringToReturn = stringToReturn + ", " + Image + ", " + Weight;
            return stringToReturn;
        }
    }
}
