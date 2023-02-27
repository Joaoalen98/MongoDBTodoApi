using System.ComponentModel.DataAnnotations;

namespace MongoDBTodoApi.ViewModels;

public class TodoViewModel
{
    [Required]
    public string Titulo { get; set; }

    [Required]
    public string Descricao { get; set; }

    [Required]
    public bool Completa { get; set; }
}
