using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FruitPowerAPI.Models
{
    public class PowerFruitIssue
    {
        public int Id;
        public string FullName;
        public string Email;
        public string Subject;
        public string Message;
        public bool Status;
    }
}