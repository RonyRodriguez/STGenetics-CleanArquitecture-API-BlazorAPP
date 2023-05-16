namespace STG.Domain.Entity
{
    public class Animal
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Sex { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
    }
}