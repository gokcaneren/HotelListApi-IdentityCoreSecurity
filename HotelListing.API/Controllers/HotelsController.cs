using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Contracts;
using AutoMapper;
using HotelListing.API.Models.Hotels;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Exceptions;
using Microsoft.AspNetCore.OData.Query;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _repository;
        private readonly IMapper _mapper;
        public HotelsController(IHotelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var hotels= await _repository.GetAllAsync();
            return Ok(_mapper.Map<List<HotelDto>>(hotels));
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _repository.GetByIdAsync(id);

            if (hotel == null)
            {
                throw new NotFoundException($"Hotel with id:{id} not found!");
            }

            return Ok(_mapper.Map<HotelDto>(hotel));
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDto updateHotelDto)
        {
            if (id != updateHotelDto.Id)
            {
                return BadRequest();
            }

            //_context.Entry(hotel).State = EntityState.Modified;
            var hotel = await _repository.GetByIdAsync(id);
            if (hotel == null)
            {
                throw new NotFoundException($"Hotel with id:{id} not found!");
            }

            _mapper.Map(updateHotelDto, hotel);

            try
            {
                await _repository.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);
            await _repository.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _repository.GetByIdAsync(id);
            if (hotel == null)
            {
                throw new NotFoundException($"Hotel with id:{id} not found!");
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _repository.IsExistAsync(id);
        }
    }
}
