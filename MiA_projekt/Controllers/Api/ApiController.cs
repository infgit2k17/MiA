using AutoMapper;
using MiA_projekt.Data;
using MiA_projekt.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace MiA_projekt.Controllers.Api
{
    public abstract class ApiController : Controller
    {
        private AppDbContext _db;
        private readonly IMapper _mapper;

        protected ApiController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        protected IActionResult AddItem<TCollection, TItem>(TItem item) where TCollection : class
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model");

            var added = _db.Set<TCollection>().Add(_mapper.Map<TItem, TCollection>(item));
            _db.SaveChanges();

            return Ok(_mapper.Map<TCollection, TItem>(added.Entity));
        }

        protected IActionResult GetItem<TIn, TOut>(object id) where TIn : class
        {
            var item = _db.Set<TIn>().Find(id);

            if (item == null)
                return NotFound();

            return Ok(_mapper.Map<TIn, TOut>(item));
        }

        protected IActionResult GetItems<TIn, TOut>(DataTableParamDto dto) where TIn : class
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model");

            var source = _db.Set<TIn>();
            IQueryable<TIn> items = source.OrderBy(dto.GetOrderBy()).Skip(dto.Start).Take(dto.Length);

            if (ShouldSearch(dto.Search))
                items = items.Where(dto.GetSearchCommand<TIn>());

            if (items == null)
                return Ok();

            return Ok(new DataTableDto<TOut>
            {
                Draw = dto.Draw,
                RecordsFiltered = source.Count(),
                RecordsTotal = source.Count(),
                Data = items.ToList().Select(_mapper.Map<TIn, TOut>)
            });
        }

        private bool ShouldSearch(DTSearch search)
        {
            return !String.IsNullOrWhiteSpace(search.Value);
        }

        protected IActionResult UpdateItem<TCollection, TItem>(object id, TItem item) where TCollection : class
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model");

            var itemInDb = _db.Set<TCollection>().Find(id);

            if (itemInDb == null)
                return NotFound();

            _mapper.Map(item, itemInDb);
            _db.SaveChanges();

            return Ok();
        }

        protected IActionResult RemoveItem<TIn>(object id) where TIn : class
        {
            var item = _db.Set<TIn>().Find(id);

            if (item == null)
                return NotFound();

            _db.Remove(item);
            _db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}