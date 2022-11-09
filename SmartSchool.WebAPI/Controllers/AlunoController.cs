using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;

        public AlunoController( IRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllAlunos(true));
        }

        // api/aluno/byId
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");

            return Ok(aluno);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if(!_repo.SaveChanges()) return BadRequest();
            return Ok(aluno);

        }

        // api/aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("Aluno não encontrado");

            _repo.Update(aluno);
            if(!_repo.SaveChanges()) return BadRequest("Aluno não atualizado");
            return Ok(aluno);
        }

        // api/aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("Aluno atualiado");

            _repo.Update(aluno);
            if(!_repo.SaveChanges()) return BadRequest("Aluno não atualizado");
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);
             if(!_repo.SaveChanges()) return BadRequest("Aluno não deletado");
            return Ok("Daluno deletado");
        }
    }
}