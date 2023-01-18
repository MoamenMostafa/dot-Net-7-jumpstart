using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dot_Net_7_jumpstart.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper ;
        private readonly DataContext _context ;
        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        private static List<Character> characters = new List<Character>
        {
            new Character(),
                new Character(){
                    Id=1,
                    Name="Sam"
                }
        };

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse ;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId)   
        {
           var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
           var characters = await _context.Characters.Where(c=>c.User!.Id==userId).ToListAsync();
           serviceResponse.Data = characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
           return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = await _context.Characters.SingleOrDefaultAsync(c=>c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
             var serviceResponse = new ServiceResponse<GetCharacterDto>();
         try
          {
            var character = await _context.Characters.SingleOrDefaultAsync(c=>c.Id==updatedCharacter.Id);
            
            if(character is null)
              throw new Exception($"Character with Id '{updatedCharacter.Id}' not found ");

            _mapper.Map(updatedCharacter,character);
            character.Name = updatedCharacter.Name;
            character.Defense = updatedCharacter.Defense;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Class = updatedCharacter.Class;
            character.Name = updatedCharacter.Name;
            _context.Characters.Update(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch(Exception ex)
            {
             serviceResponse. Success = false ;
             serviceResponse.Message = ex.Message;  
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
               var character = await _context.Characters.FirstOrDefaultAsync(c=>c.Id==id);
               if(character is null)
                 throw new Exception($"Character with Id '{id}' not found");

               _context.Characters.Remove(character);
               await _context.SaveChangesAsync();
              serviceResponse.Data = await _context.Characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToListAsync();     
            //   serviceResponse.Data = _mapper.Map<GetCharacterDto>(characters);
            }
            catch(Exception ex)
            {
               serviceResponse.Success = false ;
               serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}