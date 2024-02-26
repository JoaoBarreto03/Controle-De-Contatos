using ControleDeContatos.Models;
namespace ControleDeContatos.Helper;

public interface ISessao
{
    void CriarSessaoUsuario(UsuarioModel usuarioModel);
    void RemoverSessaoUsuario();
    UsuarioModel BuscarSessaoUsuario();

}