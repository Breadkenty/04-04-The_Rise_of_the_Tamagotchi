using System;
using System.Collections.Generic;
using System.Linq;
using _04_04_The_Rise_of_the_Tamagotchi.Models;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<Pet>> GetAll()
        {
            var allThePets = _context.Pets;

            return Ok(allThePets);
        }

        [HttpGet("{id}")]
        public ActionResult<Pet> GetById(int id)
        {
            var selectedPet = _context.Pets.FirstOrDefault(pet => pet.Id == id);

            if (selectedPet == null)
            {
                return NotFound();
            }

            return Ok(selectedPet);
        }

        [HttpPost]
        public ActionResult<Pet> CreateNewPet(Pet petToCreate)
        {

            // var errorMessage = new
            // {
            //     message = $"This is an error message"
            // };

            // return UnprocessableEntity(errorMessage);

            _context.Pets.Add(petToCreate);
            _context.SaveChanges();

            return CreatedAtAction(null, null, petToCreate);
        }
    }
}