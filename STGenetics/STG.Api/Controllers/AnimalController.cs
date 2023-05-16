using STG.Domain.Enums;

namespace STG.Api.Controllers
{
    public static class AnimalController
    {

        public static List<string> GetBreedList()
        {
            Array values = Enum.GetValues(typeof(BreedEnum));
            List<string> breedList = new List<string>();

            foreach (var value in values)
            {
                breedList.Add(value.ToString());
            }

            return breedList;
        }

        public static List<string> GetSexList()
        {
            Array values = Enum.GetValues(typeof(SexEnum));
            List<string> sexList = new List<string>();

            foreach (var value in values)
            {
                sexList.Add(value.ToString());
            }

            return sexList;
        }

    }
}
