using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Note.Api.Filters;
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
        protected readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        // GET api/notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteListDTO>>> GetAsync()
        {
            var items = await _noteService.GetAllNoteListsAsync();
            return Ok(items.ToList());
        }

        // GET api/notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteListDTO>> GetAsync(string id)
        {
            var item = await _noteService.GetNoteListAsync(id);
            return Ok(item);
        }

        // POST api/notes
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<NoteListDTO>> PostAsync([FromBody] CreateNoteListDTO dto)
        {
            var note = await _noteService.CreateNotListAsync(dto);
            return Ok(note);
        }

        // PUT api/notes/5
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<ActionResult<NoteListDTO>> PutAsync(string id, [FromBody] UpdateNoteListDTO dto)
        {
            var note = await _noteService.UpdateNoteListAsync(id, dto);
            return Ok(note);
        }

        // DELETE api/notes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var result = await _noteService.DeleteNoteListAsync(id);
            return Ok();
        }

        // POST api/notes/5/items
        [HttpPost("{noteId}/items")]
        [ValidateModel]
        public async Task<ActionResult<NoteListDTO>> PostItemAsync(string noteId, [FromBody] CreateNoteItemDTO dto)
        {
            var note = await _noteService.CreateNoteItemAsync(noteId, dto);
            return Ok(note);
        }

        // PUT api/notes/5/items/6
        [HttpPut("{noteId}/items/{itemId}")]
        [ValidateModel]
        public async Task<ActionResult<NoteListDTO>> PutItemAsync(string noteId, string itemId, [FromBody] UpdateNoteItemDTO dto)
        {
            var note = await _noteService.UpdateNoteItemAsync(noteId, itemId, dto);
            return Ok(note);
        }

        // DELETE api/notes/5/items/6
        [HttpDelete("{noteId}/items/{itemId}")]
        public async Task<ActionResult> DeleteItemAsync(string noteId, string itemId)
        {
            var result = await _noteService.DeleteNoteItemAsync(noteId, itemId);
            return Ok();
        }
    }
}