using AutoMapper;
using Microsoft.EntityFrameworkCore;
using twoday_Internship_Task.Database;
using twoday_Internship_Task.Database.DatabaseModels;
using twoday_Internship_Task.Database.DatabaseModels.Enums;
using twoday_Internship_Task.Exceptions;
using twoday_Internship_Task.Models;
using twoday_Internship_Task.Services.Interfaces;

namespace twoday_Internship_Task.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _dbContext;

        public AnimalsService(IMapper mapper, DatabaseContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AnimalModel>> AddAnimalsAsync(AnimalsJsonModel animalsModel)
        {
            // Ordering the list by size, so that if many herbivore species would need to share
            // one enclosure, they would get the biggest one
            var enclosures =
                await _dbContext.Enclosures.Include(x => x.Animals)
                    .OrderBy(x => x.Size)
                    .ToListAsync();

            if (enclosures.Count() == 0)
                throw new NoEnclosuresException();

            // Ordering the list, so that carnivores would be dealt with first
            var animals = _mapper.Map<IEnumerable<Animal>>(animalsModel.Animals)
                            .OrderByDescending(x => x.Food);

            var amountOfEmptyEnclosures = enclosures.Count(x => x.Animals.Count.Equals(0));
            var herbivoreEnclosureExists = enclosures.Any(enc => enc.Animals.Any(x => x.Food.Equals(AnimalDiet.Herbivore)));
            var areThereNewHerbivores = animals.Any(x => x.Food.Equals(AnimalDiet.Herbivore));
            var amountOfEnclosuresNeededForHerbivores = !herbivoreEnclosureExists && areThereNewHerbivores ? 1 : 0;

            foreach (var animal in animals)
            {
                // If species were already placed in some enclosure, place new animals with them
                var existingAnimalEnclosure =
                    enclosures.SingleOrDefault(enc => enc.Animals.Any(x => x.Species.Equals(animal.Species)));
                if (existingAnimalEnclosure != null)
                {
                    existingAnimalEnclosure.Animals.Single(x => x.Species.Equals(animal.Species)).Amount += animal.Amount;
                    continue;
                }

                // Place carnivores in seperate enclosures if possible while
                // leaving one empty enclosure for herbivores if none yet exist
                // and there are going to be new herbivores
                if (animal.Food.Equals(AnimalDiet.Carnivore) &&
                    amountOfEmptyEnclosures > amountOfEnclosuresNeededForHerbivores)
                {
                    enclosures.First(x => x.Animals.Count.Equals(0)).Animals.Add(animal);
                    amountOfEmptyEnclosures--;
                    continue;
                }

                // If no more empty available enclosures are left for Carnivores,
                // look for the least populated carnivore enclosure
                // (Since animal lists are ordered before sorting into enclosures
                // First enclosure with 1 animal species will have the least amount of animals
                if (animal.Food.Equals(AnimalDiet.Carnivore) &&
                    amountOfEmptyEnclosures == amountOfEnclosuresNeededForHerbivores)
                {
                    var enclosure =
                            enclosures.FirstOrDefault(enc => enc.Animals.All(x =>
                                x.Food.Equals(AnimalDiet.Carnivore)) && enc.Animals.Count == 1) ??
                                    MakeSpaceForCarnivoreEnclosure(enclosures) ??
                                    throw new NoAvailableEnclosureException(animal.Species);

                    enclosure.Animals.Add(animal);
                    continue;
                }

                // After sorting the Carnivores, deal with the remaining herbivores by
                // separating them into the remaining enclosures 
                // (AmountOfEmptyEnclosures) is not decreased here because it only matters when sorting
                // Carnivores.
                if (animal.Food.Equals(AnimalDiet.Herbivore))
                {
                    var enclosure =
                    enclosures.Where(enc => !enc.Animals.Any(x =>
                        x.Food.Equals(AnimalDiet.Carnivore))).MinBy(enc => enc.Animals.Count) ??
                            MakeSpaceForHerbivoreEnclosure(enclosures) ??
                            throw new NoAvailableEnclosureException(animal.Species);

                    enclosure.Animals.Add(animal);
                    continue;
                }

            }

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<IEnumerable<AnimalModel>>(animals);
        }

        public async Task DeleteAnimalsAsync(string species, int amount)
        {
            var enclosure =
                await _dbContext.Enclosures.Include(x => x.Animals)
                    .SingleOrDefaultAsync(enc =>
                        enc.Animals.Any(x => x.Species.Trim().ToLower().Equals(species)))
                    ?? throw new ItemNotFoundException($"{species} was not found in the zoo");

            var animal = enclosure.Animals.Single(x => x.Species.Trim().ToLower().Equals(species));
            animal.Amount -= amount;

            if (animal.Amount <= 0)
            {
                enclosure.Animals.Remove(animal);
            }

            await _dbContext.SaveChangesAsync();
        }


        private Enclosure? MakeSpaceForHerbivoreEnclosure(List<Enclosure> enclosures)
        {
            var firstCarnivoreEnclosure =
                enclosures.FirstOrDefault(enc =>
                    enc.Animals.Count == 1 &&
                        enc.Animals.All(x => x.Food.Equals(AnimalDiet.Carnivore)));

            var lastCarnivoreEnclosure =
                enclosures.LastOrDefault(enc =>
                    enc.Animals.Count == 1 &&
                        enc.Animals.All(x => x.Food.Equals(AnimalDiet.Carnivore)));

            return MoveAnimalsIntoOneEnclosure(firstCarnivoreEnclosure, lastCarnivoreEnclosure);
        }

        private Enclosure? MakeSpaceForCarnivoreEnclosure(List<Enclosure> enclosures)
        {
            var firstHerbivoreEnclosure =
                enclosures.FirstOrDefault(enc =>
                    enc.Animals.All(x => x.Food.Equals(AnimalDiet.Herbivore)));

            var lastHerbivoreEnclosure =
                enclosures.LastOrDefault(enc =>
                    enc.Animals.All(x => x.Food.Equals(AnimalDiet.Herbivore)));

            return MoveAnimalsIntoOneEnclosure(firstHerbivoreEnclosure, lastHerbivoreEnclosure);
        }

        // This Method handles the edge case when a new animal is being added, and all of the enclosures are currently occupied,
        // a new animal might only fit in with some rearrangements of current animals
        private Enclosure? MoveAnimalsIntoOneEnclosure(Enclosure? firstEnclosure, Enclosure? lastEnclosure)
        {
            if (firstEnclosure == null ||
                lastEnclosure == null ||
                firstEnclosure.Name == lastEnclosure.Name)
                return null;

            firstEnclosure.Animals = firstEnclosure.Animals.Concat(lastEnclosure.Animals).ToList();
            lastEnclosure.Animals.Clear();

            return lastEnclosure;
        }
    }
}
