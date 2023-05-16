using Microsoft.AspNetCore.Components;
using STG.BlazorApp.Base;
using STG.BlazorApp.Helpers;
using STG.BlazorApp.Rules;
using STG.BlazorApp.Service;
using STG.Domain.Domain;



namespace STG.BlazorApp.Managers
{
    public class AnimalManager : GridControllerBase<AnimalDomain>
    {
        private readonly AnimalService service;
        private readonly NavigationManager navigationManager;
        private readonly AnimalRules rules;
        public List<AnimalDomain> SelectedAnimals { get; set; }
        public CardDomain CardDomain { get; set; }
        public List<String> ASexes { get; set; }
        public List<String> ABreeds { get; set; }
        public bool ShowDialog { get; set; }

        public AnimalManager(AnimalService service, NavigationManager navigationManager, AnimalRules rules) : base(new List<AnimalDomain>())
        {
            sortingHelper = new SortingHelper<AnimalDomain>(items);
            this.service = service;
            this.navigationManager = navigationManager;
            this.rules = rules;

            SelectedAnimals = new List<AnimalDomain>();
            CardDomain = new CardDomain();
        }

        public async Task GetAnimalsRemote()
        {
            await GetAnimalBreeds();
            await GetAnimalSexes();
            var list = await service.GetAnimalsRemote();
            UpdateList(list);
        }

        public async Task GetAnimalSexes()
        {
            ASexes = await service.GetAnimalSexes();
        }

        public async Task GetAnimalBreeds()
        {
            ABreeds = await service.GetAnimalBreeds();
        }

        public void UpdateList(List<AnimalDomain> list)
        {
            items = list;
            paginationHelper.SetItems(items);
            sortingHelper = new SortingHelper<AnimalDomain>(items);
            IsDataLoaded = true;
        }

        public void DeleteItem(AnimalDomain item)
        {
            items.Remove(item);
            UpdateDisplayedItems();
        }

        public async Task UpdateAll()
        {
            var selected = items.Where(f => f.Selected).ToList();
            await service.UpdateAnimalsRemote(selected);

        }

        public void AddToCard()
        {
            var selected = items.Where(f => f.Selected).ToList();

            if (selected.Count > 0)
            {
                if (!rules.AnimalsExistInCard(selected, SelectedAnimals))
                {
                    SelectedAnimals.AddRange(items.Where(f => f.Selected).ToList());
                    CardDomain = rules.getCardDomain(SelectedAnimals);
                }
                else
                {
                    ShowDialog = true;
                }
            }
        }

        public async Task UpdateItem(AnimalDomain item)
        {
            List<AnimalDomain> list = new List<AnimalDomain>();
            list.Add(item);
            await service.UpdateAnimalsRemote(list);
        }

        public void SortList(string column)
        {
            bool sortAscending = column == sortColumn ? !this.sortAscending : true;

            switch (column)
            {
                case "Name":
                    sortingHelper.SortByProperty(f => f.Name, sortAscending);
                    break;
                case "BirthDate":
                    sortingHelper.SortByProperty(f => f.BirthDate, sortAscending);
                    break;
                case "Breed":
                    sortingHelper.SortByProperty(f => f.Breed, sortAscending);
                    break;
                case "Sex":
                    sortingHelper.SortByProperty(f => f.Sex, sortAscending);
                    break;
                case "Price":
                    sortingHelper.SortByProperty(f => f.Price, sortAscending);
                    break;
            }

            sortColumn = column;
            this.sortAscending = sortAscending;

            items = sortingHelper.GetSortedItems();
            UpdateDisplayedItems();
        }
        public DateOnly? BirthDateFilter { get; set; }
        public string NameFilter { get; set; }
        public string BreedFilter { get; set; }
        public string SexFilter { get; set; }
        public decimal? PriceFilter { get; set; }
        public void ApplyFilters()
        {
            CurrentPage = 1;

            UpdateList(items.Where(item =>
            (BirthDateFilter == null || item.BirthDate == BirthDateFilter.Value)
            && (NameFilter == null || item.Name == NameFilter)
            && (BreedFilter == null || item.Breed == BreedFilter)
            && (SexFilter == null || item.Sex == SexFilter)
            && (PriceFilter == null || item.Price == PriceFilter.Value)
            ).ToList());
        }

        public void ResetFilters()
        {
            BirthDateFilter = null;
            NameFilter = null;
            SexFilter = null;
            BreedFilter = null;
            PriceFilter = null;
            paginationHelper.SetItems(items);
            CurrentPage = 1;
        }

    }
}
