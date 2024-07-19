CRUD on Tasks and Subtasks API
------------------------------------------------------
Projects 

- Data
- Logic
- API (Bunzl)
- Test
------------------------------------------------------
API Endpoints

- Tasks
  -Get all Tasks without subtasks (httpget)(/api/tasks , /api/tasks/getAllWithoutSubTasks)
  -Get all tasks, each with it's subtasks (httpget)(/api/tasks/getAllWithSubTasks)
  -Get a task by ID, but without it's subtasks (httpget)(/api/task/id , /api/tasks/getWithoutSubTasks/id)
  -Get a task by ID, with it's subtasks (httpget)(/api/tasks/getWithSubTasks/id)
  -Create new task (httppost)(fields:name(string),completed(bool))
  -Update task (httpput(id))(fields:name(string),completed(bool))
  -Remove Task (httpdelete(id))

- Subtasks
  -Get all subtasks of a task (httpget)(api/subtask/taskid)
  -Get details of a subtask (httpget)(api/subtask/getSubTask/id)
  -Create new subtask (httppost)(fields:name(string),completed(bool),taskid(int))
  -Update subtask (httpput(id))(fields:name(string),completed(bool),taskid(int))
  -Remove subTask (httpdelete(id))
------------------------------------------------------
Installation

1-git clone https://github.com/hrsatar/BunzlRepository.git
2-cd BunzlRepository/Bunzl
3-dotnet restore Bunzl.sln
4-cd ../data
5-dotnet tool install --global dotnet-ef
6-dotnet ef database update --startup-project ../Bunzl
7-change connectionstring in appsettings.json
8-dotnet build
------------------------------------------------------
Test

1-dotnet test
------------------------------------------------------
Run

1-dotnet run
2-http://localhost:5351/swagger
------------------------------------------------------
Specifications

- ORM: Entity Framework core
- Database: SQL SERVER
- Test Framework: xUnit
- UI: Swagger
- using Repositories, DTOs, Unit of Work, DI


