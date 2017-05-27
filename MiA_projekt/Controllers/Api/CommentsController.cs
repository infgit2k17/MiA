using AutoMapper;
using MiA_projekt.Data;
using MiA_projekt.Dto;
using MiA_projekt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiA_projekt.Controllers.Api
{
    [Produces("application/json")]
    [Authorize(Roles = "Admin")]
    public class CommentsController : ApiController
    {
        public CommentsController(AppDbContext db, IMapper mapper) : base(db, mapper)
        { }

        [HttpPost]
        [Route("api/comments")]
        public IActionResult Create(CommentDto dto)
        {
            return AddItem<Comment, CommentDto>(dto);
        }

        [HttpGet]
        [Route("api/comments/{id}")]
        public IActionResult Get(int id)
        {
            return GetItem<Comment, CommentDto>(id);
        }

        [HttpPost]
        [Route("api/datatable/comments")]
        public IActionResult Get(DataTableParamDto dto)
        {
            return GetItems<Comment, CommentDto>(dto);
        }

        [HttpPut]
        [Route("api/comments/{id}")]
        public IActionResult Update(int id, CommentDto dto)
        {
            return UpdateItem<Comment, CommentDto>(id, dto);
        }

        [HttpDelete]
        [Route("api/comments/{id}")]
        public IActionResult Remove(int id)
        {
            return RemoveItem<Comment>(id);
        }
    }
}