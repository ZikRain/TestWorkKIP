using System.ComponentModel.DataAnnotations;

namespace TestWorkKip.Entities
{
    public class Result
    {
        [Key()]
        public string Query_guid { get; set; }
        public string user_guid { get; set; }
        public int Count_sign_in { get; set; }
    }
}
