namespace STG.Domain.Domain
{
    public class CardDomain
    {
        public CardDomain()
        {
            AnimalBreeds = new List<AnimalBreedDomain>();
        }

        public List<AnimalBreedDomain> AnimalBreeds { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal TotalAmountWithDiscount { get; set; }
    }
}
