using AutoMapper;
using Microsoft.EntityFrameworkCore;
using twoday_Internship_Task.Database;
using twoday_Internship_Task.Database.DatabaseModels;
using twoday_Internship_Task.DtoModels;
using twoday_Internship_Task.Exceptions;
using twoday_Internship_Task.Models;
using twoday_Internship_Task.Services.Interfaces;

namespace twoday_Internship_Task.Services
{
    public class EnclosureService : IEnclosureService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _dbContext;

        public EnclosureService(IMapper mapper, DatabaseContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EnclosureGETModel>> AddEnclosuresAsync(EnclosuresJsonModel enclosuresModel)
        {
            var enclosures = _mapper.Map<IEnumerable<Enclosure>>(enclosuresModel.Enclosures).ToList();
            enclosures.ForEach(enclosure => _dbContext.Add(enclosure));
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<IEnumerable<EnclosureGETModel>>(enclosures);
        }

        public async Task<IEnumerable<EnclosureGETModel>> GetAllEnclosuresAsync()
            => _mapper.Map<IEnumerable<EnclosureGETModel>>(
                await _dbContext.Enclosures.Include(x => x.Objects)
                        .Include(x => x.Animals).AsNoTracking().ToListAsync());

        public async Task DeleteEnclosureAsync(string enclosureName)
        {
            var enclosure =
                await _dbContext.Enclosures.SingleOrDefaultAsync(x => x.Name.ToLower().Trim().Equals(enclosureName))
                    ?? throw new ItemNotFoundException("Enclosure with given Id was not found");

            _dbContext.Remove(enclosure);
            await _dbContext.SaveChangesAsync();
        }
    }
}
