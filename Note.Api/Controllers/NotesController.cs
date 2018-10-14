using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Note.Core.Models.DTO.Note;
using Note.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Api.Controllers
{
    [Route("api/v1/notes")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class NotesController : ControllerBase
    {
        protected readonly NoteService _noteService;

        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }

        // GET api/notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteListDTO>>> GetAsync()
        {
            var items = await _noteService.GetAllAsync();
            return Ok(items.ToList());
        }

        // GET api/notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteListDTO>> GetAsync(string id)
        {
            var item = await _noteService.GetAsync(id);
            return Ok(item);
        }

        // POST api/notes
        [HttpPost]
        public async Task<ActionResult<NoteListDTO>> PostAsync([FromBody] CreateNoteListDTO dto)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid parameter", nameof(dto));
            }

            var note = await _noteService.CreateAsync(dto);
            return Ok(note);
        }

        // PUT api/notes/5
        [HttpPut("{id}")]
        public async Task<ActionResult<NoteListDTO>> PutAsync(string id, [FromBody] UpdateNoteListDTO dto)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid parameter", nameof(dto));
            }

            var note = await _noteService.UpdateAsync(id, dto);
            return Ok(note);
        }

        // DELETE api/notes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var result = await _noteService.DeleteAsync(id);
            return Ok();
        }

        // POST api/notes/5/items
        [HttpPost("{noteId}/items")]
        public async Task<ActionResult<NoteListDTO>> PostItemAsync(string noteId, [FromBody] CreateNoteItemDTO dto)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid parameter", nameof(dto));
            }

            var note = await _noteService.CreateItemAsync(noteId, dto);
            return Ok(note);
        }

        // PUT api/notes/5/items/6
        [HttpPut("{noteId}/items/{itemId}")]
        public async Task<ActionResult<NoteListDTO>> PutItemAsync(string noteId, string itemId, [FromBody] UpdateNoteItemDTO dto)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid parameter", nameof(dto));
            }

            var note = await _noteService.UpdateItemAsync(noteId, itemId, dto);
            return Ok(note);
        }

        // DELETE api/notes/5/items/6
        [HttpDelete("{noteId}/items/{itemId}")]
        public async Task<ActionResult> DeleteItemAsync(string noteId, string itemId)
        {
            var result = await _noteService.DeleteItemAsync(noteId, itemId);
            return Ok();
        }
    }
}