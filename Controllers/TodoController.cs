using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBTodoApi.Models;

namespace MongoDBTodoApi.Controllers;

[ApiController]
[Route("api/v1/todo")]
public class TodoController : ControllerBase
{
    private readonly IMongoCollection<Todo> _todoCollection;

    public TodoController(IOptions<TodoDbSettings> todoDbSettings)
    {
        var mongoClient = new MongoClient(
            todoDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            todoDbSettings.Value.DatabaseName);

        _todoCollection = mongoDatabase.GetCollection<Todo>(
            todoDbSettings.Value.TodosCollectionName);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var todos = await _todoCollection.Find(_ => true).ToListAsync();
        return Ok(todos);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetPorIdAsync(string id)
    {
        try
        {
            var todo = await _todoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(Todo todo)
    {
        try
        {
            await _todoCollection.InsertOneAsync(todo);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> AtulizarAsync(string id, Todo todo)
    {
        try
        {
            var uptd = await _todoCollection.ReplaceOneAsync(x => x.Id == id, todo);
            return Ok(uptd);
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id)
    {
        try
        {
            var rmv = await _todoCollection.DeleteOneAsync(x => x.Id == id);
            return Ok(rmv);
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
}