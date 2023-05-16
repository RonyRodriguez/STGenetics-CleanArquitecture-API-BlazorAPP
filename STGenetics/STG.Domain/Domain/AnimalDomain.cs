namespace STG.Domain.Domain
{
    public class AnimalDomain
    {

        public AnimalDomain()
        {
            Quantity = 1;
        }
        public bool Selected { get; set; }
        public int Quantity { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Sex { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
    }
}
