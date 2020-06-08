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

        private async Task<Pet> FindPetAsync(int id)
        {
            return await _context.Pets.FirstOrDefaultAsync(pet => pet.Id == id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAllPetsAsync()
        {
            return Ok(await _context.Pets.ToListAsync());
        }

        [HttpGet("/Pets_alive")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAllAlivePetsAsync()
        {
            return Ok(await _context.Pets.Where(pet => pet.IsDead == false).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPetByIdAsync(int id)
        {
            var selectedPet = await FindPetAsync(id);

            if (selectedPet == null)
            {
                return NotFound();
            }

            return Ok(selectedPet);
        }

        [HttpPost("/Create_new_pet")]
        public async Task<ActionResult<Pet>> PostNewPetAsync(Pet petToCreate)
        {
            petToCreate.Birthday = DateTime.Now;
            petToCreate.HungerLevel = 0;
            petToCreate.HappinessLevel = 0;
            petToCreate.LastInteracted = DateTime.Now;
            petToCreate.IsDead = false;

            _context.Pets.Add(petToCreate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(null, null, petToCreate);
        }

        [HttpPost("{id}/Play")]
        public async Task<ActionResult<Pet>> PostPlayPetByIdAsync(int id)
        {
            var selectedPet = await FindPetAsync(id);

            if (selectedPet == null)
            {
                return NotFound();
            }

            selectedPet.HappinessLevel += 5;
            selectedPet.HungerLevel += 3;

            if (DateTime.Now > selectedPet.LastInteracted.AddDays(+3))
            {
                selectedPet.IsDead = true;
            }
            selectedPet.LastInteracted = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(selectedPet);
        }

        [HttpPost("{id}/Feed")]
        public async Task<ActionResult<Pet>> PostFeedPetByIdAsync(int id)
        {
            var selectedPet = await FindPetAsync(id);

            if (selectedPet == null)
            {
                return NotFound();
            }

            selectedPet.HappinessLevel += 3;
            selectedPet.HungerLevel -= 3;
            selectedPet.LastInteracted = DateTime.Now;

            if (DateTime.Now > selectedPet.LastInteracted.AddDays(+3))
            {
                selectedPet.IsDead = true;
            }

            if (selectedPet.HungerLevel < 0)
            {
                selectedPet.HungerLevel = 0;
            }

            await _context.SaveChangesAsync();
            return Ok(selectedPet);
        }

        [HttpPost("{id}/Scold")]
        public async Task<ActionResult<Pet>> PostScoldPetByIdAsync(int id)
        {
            var selectedPet = await FindPetAsync(id);

            if (selectedPet == null)
            {
                return NotFound();
            }

            selectedPet.HappinessLevel -= 5;
            selectedPet.LastInteracted = DateTime.Now;

            if (DateTime.Now > selectedPet.LastInteracted.AddDays(+3))
            {
                selectedPet.IsDead = true;
            }

            if (selectedPet.HappinessLevel < 0)
            {
                selectedPet.HappinessLevel = 0;
            }

            await _context.SaveChangesAsync();
            return Ok(selectedPet);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pet>> DeletePetByIdAsync(int id)
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

    }
}