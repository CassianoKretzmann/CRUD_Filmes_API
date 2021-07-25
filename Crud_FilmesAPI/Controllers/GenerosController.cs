using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crud_FilmesAPI.Configuracao;
using Crud_FilmesAPI.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Crud_FilmesAPI.Validador;
using FluentValidation.Results;

namespace Crud_FilmesAPI.Controllers
{
    [Route("api/generos")]
    [ApiController]

    //Classe do controller de Genero com as ações CRUD.
    //Metodos Post e Put validam as rules do validador antes de executarem suas ações.
    public class GenerosController : ControllerBase
    {
        private readonly Contexto _context;
        public readonly ValidadorGenero _regrasValidacao = new ValidadorGenero();
        public ValidationResult _validacao = new ValidationResult();

        public GenerosController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Generos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
        {
            return await _context.Generos.ToListAsync();
        }

        // GET: api/Generos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            var genero = await _context.Generos.FindAsync(id);

            if (genero == null)
            {
                return NotFound();
            }

            return genero;
        }

        // PUT: api/Generos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenero(int id, Genero genero)
        {
            _validacao = _regrasValidacao.Validate(genero);

            if (_validacao.IsValid)
            {
                IDbContextTransaction transaction = _context.Database.BeginTransaction();

                if (id != genero.Id)
                {
                    return BadRequest();
                }

                _context.Entry(genero).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    transaction.Rollback();
                    if (!GeneroExists(id))
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

            return BadRequest(_validacao.Errors);
            
        }

        // POST: api/Generos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Genero>> PostGenero(Genero genero)
        {
            _validacao = _regrasValidacao.Validate(genero);

            if (_validacao.IsValid) 
            {
                IDbContextTransaction transaction = _context.Database.BeginTransaction();
                try
                {
                    _context.Generos.Add(genero);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                    return CreatedAtAction("GetGenero", new { id = genero.Id }, genero);
                }
                catch (Exception E)
                {
                    transaction.Rollback();
                    return BadRequest(E.Message);
                }
            }

            return BadRequest(_validacao.Errors);
        }

        // DELETE: api/Generos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();

            try
            {
                var genero = await _context.Generos.FindAsync(id);
                if (genero == null)
                {
                    return NotFound();
                }

                _context.Generos.Remove(genero);
                await _context.SaveChangesAsync();

                transaction.Commit();
                return NoContent();
            }
            catch (Exception E)
            {
                transaction.Rollback();
                return BadRequest(E.Message);
            }
        }

        private bool GeneroExists(int id)
        {
            return _context.Generos.Any(e => e.Id == id);
        }
    }
}
