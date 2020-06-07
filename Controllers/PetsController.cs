using System;
using System.Collections.Generic;
using System.Linq;
using _04_04_The_Rise_of_the_Tamagotchi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace _04_04_The_Rise_of_the_Tamagotchi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PetsController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAllAsync()
        {
            var allThePets = await _context.Pets.Where(pet => pet.IsDead == false).ToListAsync();

            return Ok(allThePets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetByIdAsync(int id)
        {
            var selectedPet = await FindPetAsync(id);

            if (selectedPet == null)
            {
                return NotFound();
            }

            return Ok(selectedPet);
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> CreateNewPetAsync(Pet petToCreate)
        {

            // var errorMessage = new
            // {
            //     message = $"This is an error message"
            // };

            // return UnprocessableEntity(errorMessage);

            _context.Pets.Add(petToCreate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(null, null, petToCreate);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pet>> UpdateAsync(int id, Pet petUpdate)
        {
            if (id != petUpdate.Id)
            {
                var errorMessage = new
                {
                    message = "Error message"
                };
                return BadRequest(errorMessage);
            }

            _context.Entry(petUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(petUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pet>> DeleteAsync(int id)
        {
            var selectedPet = await FindPetAsync(id);

            if (selectedPet == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(selectedPet);
            await _context.SaveChangesAsync();

            return Ok(selectedPet);
        }

        private async Task<Pet> FindPetAsync(int id)
        {
            return await _context.Pets.FirstOrDefaultAsync(pet => pet.Id == id);
        }


        [HttpPut("{id}/play")]
        public async Task<ActionResult<Pet>> Playtimes(int id)
        {
            var selectedPet = await FindPetAsync(id);

            selectedPet.HappinessLevel += 5;
            selectedPet.HungerLevel += 3;
            selectedPet.LastInteracted = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(selectedPet);
        }

        [HttpPut("{id}/feed")]
        public async Task<ActionResult<Pet>> Feedings(int id)
        {
            var selectedPet = await FindPetAsync(id);

            selectedPet.HappinessLevel += 3;
            selectedPet.HungerLevel -= 3;
            selectedPet.LastInteracted = DateTime.Now;

            if (selectedPet.HungerLevel < 0)
            {
                selectedPet.HungerLevel = 0;
            }

            await _context.SaveChangesAsync();
            return Ok(selectedPet);
        }

        [HttpPut("{id}/scold")]
        public async Task<ActionResult<Pet>> Scoldings(int id)
        {
            var selectedPet = await FindPetAsync(id);

            selectedPet.HappinessLevel -= 5;
            selectedPet.LastInteracted = DateTime.Now;

            if (selectedPet.HappinessLevel < 0)
            {
                selectedPet.HappinessLevel = 0;
            }

            await _context.SaveChangesAsync();
            return Ok(selectedPet);
        }
    }
}