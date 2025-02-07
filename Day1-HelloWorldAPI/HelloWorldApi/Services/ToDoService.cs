using HelloWorldApi.Models;

namespace HelloWorldApi.Services
{
    public class ToDoService
    {
        //Initialize
        private static List<ToDo> _toDos = new List<ToDo>();
        
        //C of CRUD
        public void Create(ToDo toDo)
        {
            toDo.Id = _toDos.Count + 1;
            _toDos.Add(toDo);
        }

        //R of CRUD
        //This will retrieve all ToDos
        public List<ToDo> RetrieveAll() => _toDos;

        //This will retrieve by ID
        public ToDo? RetrieveById(int id) => _toDos.FirstOrDefault(t => t.Id == id);

        //U of CRUD
        public void Update(int  id, ToDo toDo)
        {
            int index = _toDos.FindIndex(t => t.Id == toDo.Id);
            if(index == -1)
            {
                _toDos[index] = toDo;
            }
        }

        //D of CRUD
        public void Delete(int id) => _toDos.RemoveAll(t => t.Id == id);

    }
}
