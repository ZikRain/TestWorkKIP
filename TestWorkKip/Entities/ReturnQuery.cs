namespace TestWorkKip.Entities
{
    public class ReturnQuery
    {
        public string query {  get; set; }
        public int percent { get; set; }

        public ReturnResult result { get; set; }
    }

    public class ReturnResult
    {
        public string user_id { get; set; }
        public int count_sign_in { get; set; }
    }
}
