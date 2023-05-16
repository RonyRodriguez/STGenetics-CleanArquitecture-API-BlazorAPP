using Microsoft.EntityFrameworkCore;
using STG.Domain.Entity;
using STG.Domain.Enums;
using STG.Infrastructure.Databse;

namespace STG.Infrastructure.Database
{
    public static class AppDbSeed
    {
        private static Random random = new Random();

        public static async Task Init(AppDbContext dbContext)
        {
            if (!(await dbContext.Animals.AnyAsync()))
            {
                List<Animal> animals = new List<Animal>();

                for (int i = 1; i < 21; i++)
                {
                    Animal animal = new Animal
                    {
                        Id = i + 1,
                        Name = GenerateName(),
                        Breed = GenerateRandomBreed(),
                        BirthDate = GenerateRandomBirthDate(),
                        Sex = GenerateRandomSex(),
                        Price = GenerateRandomPrice(),
                        Status = GenerateRandomStatus()
                    };

                    animals.Add(animal);
                }
                await dbContext.Animals.AddRangeAsync(animals);
                await dbContext.SaveChangesAsync();
            }
        }

        private static string GenerateName()
        {
            return "Animal" + random.Next(1, 1000);
        }

        private static string GenerateRandomBreed()
        {
            Array values = Enum.GetValues(typeof(BreedEnum));
            return ((BreedEnum)values.GetValue(random.Next(values.Length))).ToString();
        }

        private static DateOnly GenerateRandomBirthDate()
        {
            int year = random.Next(2000, 2020);
            int month = random.Next(1, 13);
            int day = random.Next(1, DateTime.DaysInMonth(year, month));
            return new DateOnly(year, month, day);
        }

        private static string GenerateRandomSex()
        {
            Array values = Enum.GetValues(typeof(SexEnum));
            return ((SexEnum)values.GetValue(random.Next(values.Length))).ToString();
        }

        private static decimal GenerateRandomPrice()
        {
            double randomValue = random.Next(5000, 15000) + random.NextDouble();
            return decimal.Round(new decimal(randomValue), 2);
        }

        private static bool GenerateRandomStatus()
        {
            return random.Next(2) == 0;
        }
    }
}
