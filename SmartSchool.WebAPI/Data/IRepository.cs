using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
            void Add<T>(T entity) where T : class;
            void Update<T>(T entity) where T : class;
            void Delete<T>(T entity) where T : class;
            bool SaveChanges();

            //Alunos
            Aluno[] GetAllAlunos(bool professor);
            Aluno[] GetAllAlunoByDisciplinaId(int disciplinaId, bool professor);
            Aluno GetAlunoById(int alunoId, bool professor);

            //Professores
            Professor[] GetAllProfessors(bool includeAlunos = false);
            Professor[] GetAllProfessorByDisciplinaId(int disciplinaId, bool includeAlunos = false); 
            Professor GetProfessorById(int professorId, bool includeAlunos = false);

    }
}