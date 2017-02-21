using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Repository.EntityFramework.UnitTest
{
    public class TestEntityA
    {
        public string Id { get; set; }

        public string MyProperty1 { get; set; } = "a";

        public int? MyProperty2 { get; set; } = 123;

        public DateTime? MyProperty3 { get; set; } = DateTime.Now;

        public DateTimeOffset? MyProperty4 { get; set; } = DateTimeOffset.Now;

        //public string MyProperty5 { get; set; } = "b";

        public string MyProperty6 { get; set; } = "c";

        //public string MyProperty7 { get; set; } = "d";

        //public string MyProperty8 { get; set; } = "e";

        //public string MyProperty9 { get; set; } = "f";

    }
}
