using System;
using System.Collections.Generic;

namespace ManajementReservasiPC_API.Models
{
    public class PC
    {
        public int Number { get; set; }
        public string Specification { get; set; }
        public bool IsReserved { get; set; }
    }
}

