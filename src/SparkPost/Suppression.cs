namespace SparkPost
{
    public class Suppression
    {
        public bool Transactional { get; set; }
        public bool NonTransactional { get; set; }
        public string Description { get; set; }
    }
}