using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWorkKip.Entities
{
    public class Query
    {
        [Key()]
        public string Query_guid { get; set; }
        public DateTime StarDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string User_guid { get; set; }
        public int Percent { get; set; }

    }
}
