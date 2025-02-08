using HelloWorldApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldApi.Services
{
    public class ToDoService
    {
        private readonly AppDbContext _db;

        //Initialize
        public ToDoService(AppDbContext db) => _db = db;
        
        //C of CRUD
        public async Task Create(ToDo toDo)
        {
            _db.ToDo.Add(toDo);
            await _db.SaveChangesAsync();
        }

        //R of CRUD
        //This will retrieve all ToDos
        public async Task<List<ToDo>>RetrieveAll() => await _db.ToDo.ToListAsync();

        //This will retrieve by ID
       public async Task<ToDo?> RetrieveById(int id)
        {
                return await _db.ToDo.FindAsync(id);
        }


        //U of CRUD
        public async Task<bool> Update(int  id, ToDo toDo)
        {
            var existingToDo = await _db.ToDo.FindAsync(id);
            if (existingToDo == null) return false;

            existingToDo.Title = toDo.Title;
            existingToDo.IsCompleted = toDo.IsCompleted;

            await _db.SaveChangesAsync();
            return true;
        }

        //D of CRUD
        public async Task<bool> Delete(int id)
        {
            var toDo = await _db.ToDo.FindAsync(id);
            if (toDo == null) 
                return false;

            _db.ToDo.Remove(toDo);
            await _db.SaveChangesAsync();
            return true;
        }

    }
}
