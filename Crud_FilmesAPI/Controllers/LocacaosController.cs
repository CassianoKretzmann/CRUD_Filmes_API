using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crud_FilmesAPI.Configuracao;
using Crud_FilmesAPI.Models;
using Crud_FilmesAPI.Validador;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;

namespace Crud_FilmesAPI.Controllers
{
    [Route("api/locacaos")]
    [ApiController]

    //Classe do controller de Locação, executa metodos Get, Post e Delete, sem Put.
    //Metodo Post valida as rules do validador antes de executar suas ações.
    public class LocacaosController : ControllerBase
    {
        
        private readonly Contexto _context;
        public readonly ValidadorLocacao _regrasValidacao = new ValidadorLocacao();
        public ValidationResult _validacao = new ValidationResult();
        public LocacaosController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Locacaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locacao>>> GetLocacoes()
        {
            return await _context.Locacoes.ToListAsync();
        }

        // GET: api/Locacaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Locacao>> GetLocacao(int id)
        {
            var locacao = await _context.Locacoes.FindAsync(id);

            if (locacao == null)
            {
                return NotFound();
            }

            return locacao;
        }

        // POST: api/Locacaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Locacao>> PostLocacao(Locacao locacao)
        {
            _validacao = _regrasValidacao.Validate(locacao);

            if (_validacao.IsValid)
            {
                IDbContextTransaction transaction = _context.Database.BeginTransaction();

                try
                {
                    _context.Locacoes.Add(locacao);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                    return CreatedAtAction("GetLocacao", new { id = locacao.Id }, locacao);
                }
                catch (Exception E)
                {
                    transaction.Rollback();
                    return BadRequest(E.Message);
                }
            }

            return BadRequest(_validacao.Errors);
        }

        // DELETE: api/Locacaos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocacao(int id)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();

            var locacao = await _context.Locacoes.FindAsync(id);
            if (locacao == null)
            {
                return NotFound();
            }

            _context.Locacoes.Remove(locacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocacaoExists(int id)
        {
            return _context.Locacoes.Any(e => e.Id == id);
        }
    }
}
