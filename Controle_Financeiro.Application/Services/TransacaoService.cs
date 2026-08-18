using Controle_Financeiro.Application.DTOs.Transacao;
using Controle_Financeiro.Application.Interfaces;
using Controle_Financeiro.Domain.Entities;

namespace Controle_Financeiro.Application.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public TransacaoService(

        ITransacaoRepository repository,
        ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<TransacaoDto>> GetAllAsync()
    {
        var usuarioId = _currentUserService.UsuarioId;
        var transacoes = await _repository.GetAllByUsuarioAsync(usuarioId);

        return transacoes.Select(t => new TransacaoDto
        {
            Id = t.Id,
            Descricao = t.Descricao,
            Valor = t.Valor,
            Data = t.Data,
            Tipo = t.Tipo,
            Categoria = t.Categoria?.Nome ?? string.Empty
        });
    }

    public async Task<TransacaoDto?> GetByIdAsync(int id)
    {
        var transacao = await _repository.GetByIdAsync(id);

        if (transacao == null)
            return null;

        if (transacao.UsuarioId != _currentUserService.UsuarioId)
            return null;

        return new TransacaoDto
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Data = transacao.Data,
            Tipo = transacao.Tipo,
            Categoria = transacao.Categoria?.Nome ?? string.Empty
        };
    }

    public async Task<TransacaoDto> AddAsync(CreateTransacaoDto dto)
    {
       var transacao = new Transacao
        {
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            Data = dto.Data,
            Tipo = dto.Tipo,
            CategoriaId = dto.CategoriaId,
            UsuarioId = _currentUserService.UsuarioId
       };

        await _repository.AddAsync(transacao);

        return new TransacaoDto
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Data = transacao.Data,
            Tipo = transacao.Tipo,
            Categoria = transacao.Categoria?.Nome ?? string.Empty
        };
    }

    public async Task UpdateAsync(UpdateTransacaoDto dto)
    {
        var transacao = await _repository.GetByIdAsync(dto.Id);

        if (transacao == null)
            throw new Exception("Transação não encontrada.");

        if (transacao.UsuarioId != _currentUserService.UsuarioId)
            throw new UnauthorizedAccessException(
                "Você não tem permissão para alterar esta transação."
            );

        transacao.Descricao = dto.Descricao;
        transacao.Valor = dto.Valor;
        transacao.Data = dto.Data;
        transacao.Tipo = dto.Tipo;
        transacao.CategoriaId = dto.CategoriaId;

        await _repository.UpdateAsync(transacao);
    }

    public async Task DeleteAsync(int id)
    {
        var transacao = await _repository.GetByIdAsync(id);

        if (transacao == null)
            throw new Exception("Transação não encontrada.");

        if (transacao.UsuarioId != _currentUserService.UsuarioId)
            throw new UnauthorizedAccessException(
                "Você não tem permissão para excluir esta transação."
            );

        await _repository.DeleteAsync(transacao);
    }
}