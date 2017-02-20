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

        public string MyProperty1 { get; set; }

        public int? MyProperty2 { get; set; }

        public DateTime? MyProperty3 { get; set; }

        public DateTimeOffset? MyProperty4 { get; set; }
    }
}
