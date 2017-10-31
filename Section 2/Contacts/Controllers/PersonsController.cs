using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contacts.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        private ContactsContext _context;
        private IMapper _mapper;
        public PersonsController([FromServices]ContactsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: api/values
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<PersonInfo>), 200)]
        public IActionResult Get()
        {
            var data = _context.Persons
                .OrderBy(one => one.LastName)
                .Select(one => new PersonInfo
                {
                    PersonId = one.PersonId,
                    FirstName = one.FirstName,
                    LastName = one.LastName
                }).ToList();
            return Ok(data);
        }

        // GET api/values/5
        [HttpGet]
        [Route("{id}", Name = "GetPerson")]
        [ProducesResponseType(typeof(BusinessObjects.Person), 200)]
        public IActionResult Get(int id)
        {
            var data = _context.Persons.Include(p => p.PersonPnones)
                .FirstOrDefault(one => one.PersonId == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BusinessObjects.Person>(data));
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody]BusinessObjects.Person person)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Person>(person);
                _context.Add(entity);
                _context.SaveChanges();
                return CreatedAtRoute("GetPerson", new { id = entity.PersonId }, _mapper.Map<BusinessObjects.Person>(entity));
            }
            return BadRequest(ModelState);

        }


        [HttpPut("")]
        [Route("")]
        public IActionResult Put([FromBody]BusinessObjects.Person person)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Person>(person);
                _context.Update(entity);
                _context.SaveChanges();
                return NoContent();
            }
            return BadRequest(ModelState);
        }



        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //var data = _context.Persons.Include(p => p.PersonPnones)
            //    .FirstOrDefault(one => one.PersonId == id);
            var data = new Person { PersonId = id };
            _context.Remove(data);
            //data.PersonPnones.ToList().ForEach(p => _context.Remove(p));
            _context.SaveChanges();
            return NoContent();
        }
    }
}
