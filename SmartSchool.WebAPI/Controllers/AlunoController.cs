using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.DTOs;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController( IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);
            
            return Ok(_mapper.Map<IEnumerable<AlunoDTO>>(alunos));
        }

        [HttpGet("GetRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDTO());
        }


        // api/aluno/byId
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");

            var alunoDto = _mapper.Map<AlunoDTO>(aluno);
            return Ok(alunoDto);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDTO model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            _repo.Add(aluno);

            if(!_repo.SaveChanges()) return BadRequest();
            return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDTO>(aluno));

        }

        // api/aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDTO model)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if(!_repo.SaveChanges()) return BadRequest("Aluno não atualizado");
            return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDTO>(aluno));
        }

        // api/aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDTO model)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno atualiado");

             _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if(!_repo.SaveChanges()) return BadRequest("Aluno não atualizado");
            return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDTO>(aluno));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);
             if(!_repo.SaveChanges()) return BadRequest("Aluno não deletado");
            return Ok("aluno deletado");
        }
    }   
}