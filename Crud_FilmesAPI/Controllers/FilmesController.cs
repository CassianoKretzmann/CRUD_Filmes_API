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
using FluentValidation.Results;
using Crud_FilmesAPI.Validador;

namespace Crud_FilmesAPI.Controllers
{
    [Route("api/filmes")]
    [ApiController]
    //Classe do controller de Filme com as ações CRUD.
    //Metodos Post e Put validam as rules do validador antes de executarem suas ações.
    public class FilmesController : ControllerBase
    {
        private readonly Contexto _context;
        public readonly ValidadorFilme _regrasValidacao = new ValidadorFilme();
        public ValidationResult _validacao = new ValidationResult();

        public FilmesController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Filmes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filme>>> GetFilmes()
        {
            return await _context.Filmes.ToListAsync();
        }

        // GET: api/Filmes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> GetFilme(int id)
        {
            var filme = await _context.Filmes.FindAsync(id);

            if (filme == null)
            {
                return NotFound();
            }

            return filme;
        }

        // PUT: api/Filmes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilme(int id, Filme filme)
        {
            _validacao = _regrasValidacao.Validate(filme);

            if (_validacao.IsValid) 
            {
                IDbContextTransaction transaction = _context.Database.BeginTransaction();

                if (id != filme.Id)
                {
                    return BadRequest();
                }

                _context.Entry(filme).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    transaction.Rollback();
                    if (!FilmeExists(id))
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

        // POST: api/Filmes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Filme>> PostFilme(Filme filme)
        {
            _validacao = _regrasValidacao.Validate(filme);

            if (_validacao.IsValid)
            {
                IDbContextTransaction transaction = _context.Database.BeginTransaction();

                try
                {
                    _context.Filmes.Add(filme);
                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return CreatedAtAction("GetFilme", new { id = filme.Id }, filme);
                }
                catch (Exception E)
                {
                    transaction.Rollback();
                    return BadRequest(E.Message);
                }
            }

            return BadRequest(_validacao.Errors);
            
        }

        // DELETE: api/Filmes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilme(int id)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();

            try
            {
                var filme = await _context.Filmes.FindAsync(id);
                if (filme == null)
                {
                    return NotFound();
                }

                _context.Filmes.Remove(filme);
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

        // DELETE: api/Filmes
        //Metodo que exclui os filmes com base na lista de Ids passados, retornando not found caso o id seja inválido.
        [HttpDelete]
        public async Task<IActionResult> DeleteFilmes(List<int> filmesId)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();

            try
            {
                foreach (var filmeId in filmesId)
                {
                    var filme = await _context.Filmes.FindAsync(filmeId);
                    if (filme == null)
                    {
                        return NotFound();
                    }
                    _context.Filmes.Remove(filme);
                }

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

        private bool FilmeExists(int id)
        {
            return _context.Filmes.Any(e => e.Id == id);
        }
    }
}
