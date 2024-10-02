using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FruitPowerAPI.Models
{
    public class PowerFruitOrder
    {
        public int Id;
        public int userID;
        public string Detail;
        public decimal Price;
        public bool Status;

    }
}