using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Items
{
    public class Item
    {
        public string ItemName { get; set; }
        public string Code { get; set; }
        public string Manufacturer { get; set; }
        public string Price { get; set; }

        public Item(string itemName, string code, string manufacturer, string price)
        {
            ItemName = itemName;
            Code = code;
            Manufacturer = manufacturer;
            Price = price;
        }

        public override string ToString()
        {
            string stringToReturn = "";
            stringToReturn = stringToReturn + ", " + ItemName + ", " + Code + ", " + Manufacturer + ", " + Price;
            return stringToReturn;
        }
    }
}
