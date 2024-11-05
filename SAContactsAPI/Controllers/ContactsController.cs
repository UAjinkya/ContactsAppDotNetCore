using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAContactsAPI.Models;
using SAContactsAPI.Services;

namespace SAContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _service = new ContactService();

        [HttpGet]
        public ActionResult<List<Contact>> GetAll() => _service.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Contact> Get(int id)
        {
            var contact = _service.Get(id);
            return contact == null ? NotFound() : Ok(contact);
        }

        [HttpPost]
        public IActionResult Create(Contact contact)
        {
            _service.Add(contact);
            return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Contact contact)
        {
            if (id != contact.Id) return BadRequest();
            var existingContact = _service.Get(id);
            if (existingContact is null) return NotFound();
            _service.Update(id, contact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contact = _service.Get(id);
            if (contact is null) return NotFound();
            _service.Delete(id);
            return NoContent();
        }

        [HttpGet("error")]
        public IActionResult GetError()
        {
            // This will simulate an error
            throw new ArgumentException("This is a test argument exception.");
        }
    }
}
