using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.V1.DTOs;

namespace SmartSchool.WebAPI.V1.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController( IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// Método responsável para retornar todos os meus alunos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);
            var alunosResult = _mapper.Map<IEnumerable<AlunoDTO>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);
            
            return Ok(alunosResult);
        }

        /// <summary>
        /// Método responsável por retornar apenas um único AlunoDTO.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDTO());
        }


        /// <summary>
        /// Método responsável por retornar apenas um Aluno por Código Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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