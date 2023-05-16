using STG.Domain.Domain;

namespace STG.BlazorApp.Rules
{
    public class AnimalRules
    {
        public CardDomain getCardDomain(List<AnimalDomain> animals)
        {
            foreach (var animal in animals)
            {
                if (animal.Quantity > 5)
                {
                    decimal discountAmount = animal.Price * 0.05m;
                    animal.Price -= discountAmount;
                }
            }

            decimal totalAmount = animals.Sum(a => a.Price * a.Quantity);
            decimal discountPercentage = (animals.Sum(a => a.Quantity) > 10) ? 3.0m : 0.0m;
            decimal shippingAmount = (animals.Sum(a => a.Quantity) > 20) ? 0.0m : 1000.0m;
            decimal totalAmountWithDiscount = (totalAmount - (totalAmount * (discountPercentage / 100))) + shippingAmount;

            var animalBreeds = animals.GroupBy(a => a.Breed)
                 .Select(g => new AnimalBreedDomain
                 {
                     Name = g.Key,
                     Quantity = g.Sum(a => a.Quantity),
                     TotalPrice = g.Sum(a => a.Quantity * a.Price)
                 })
                 .OrderBy(a => a.Name)
                 .ToList();

            CardDomain card = new CardDomain
            {
                AnimalBreeds = animalBreeds,
                TotalAmount = totalAmount,
                DiscountPercentage = discountPercentage,
                ShippingAmount = shippingAmount,
                TotalAmountWithDiscount = totalAmountWithDiscount
            };

            return card;

        }

        public bool AnimalsExistInCard(List<AnimalDomain> selectedAnimals, List<AnimalDomain> cardAnimals)
        {
            var selectedAnimalIds = selectedAnimals.Select(animal => animal.Id);
            var cardAnimalIds = cardAnimals.Select(animal => animal.Id);

            var duplicates = selectedAnimalIds.Intersect(cardAnimalIds);

            return duplicates.Any();
        }

        public bool ValidateDuplicates(List<AnimalDomain> animals)
        {
            var duplicates = animals.GroupBy(animal => animal.Id)
                                   .Where(group => group.Count() > 1)
                                   .Select(group => group.Key)
                                   .ToList();

            if (duplicates.Count > 0)
                return true;

            return false;
        }
    }
}
