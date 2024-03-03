using ControleDeContatos.Repositorio;
using ControleDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ControleDeContatos.Filters;

namespace ControleDeContatos.Controllers;
[PaginaRestritaParaAdmin]
public class UsuarioController : Controller
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }
    public IActionResult Index()
    {
        List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
        return View(usuarios);
    }
    public IActionResult Criar()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Criar(UsuarioModel usuario)
    {
        if(ModelState.IsValid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuario cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);

            }
            catch(System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu usuario, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }
        return View(usuario);

    }

    public IActionResult Editar(int id)
    {
        UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
        return View(usuario);
    }


    [HttpPost]
    public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
    {
        try
        {
            UsuarioModel usuario = null;


            if(ModelState.IsValid)
            {
                usuario = new UsuarioModel()
                {
                    Id = usuarioSemSenhaModel.Id,
                    Nome = usuarioSemSenhaModel.Nome,
                    Login = usuarioSemSenhaModel.Login,
                    Email = usuarioSemSenhaModel.Email,
                    Perfil = usuarioSemSenhaModel.Perfil
                };
                usuario = _usuarioRepositorio.Atualizar(usuario);
                TempData["MensagemSucesso"] = "Usuário alterado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(usuario);
        }
        catch (System.Exception erro)
        {
            TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu usuário, tente novamente, detalhe do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }

    public IActionResult ApagarConfirmacao(int id)
    {
        UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);

        return View(usuario);
    }

    public IActionResult Apagar(int id)
    {
        try
        {
            bool apagado = _usuarioRepositorio.Apagar(id);
            
            if(apagado)
            {
                TempData["MensagemSucesso"] = "Usuário apagado com sucesso!";
            }
            else
            {
                TempData["MensagemErro"] = "Ops, não conseguimos apagar seu usuário!";
            }

            return RedirectToAction("Index");

        }
        catch (System.Exception erro)
        {
            TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu usuário, tente novamente, detalhe do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }
}