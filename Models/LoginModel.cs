using System.ComponentModel.DataAnnotations;
namespace ControleDeContatos.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Digite o login do usuário!")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Digite o senha do usuário!")]  
    public string Senha { get; set; }


}