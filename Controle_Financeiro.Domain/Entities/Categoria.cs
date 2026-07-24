namespace Controle_Financeiro.Domain.Entities;

public class Categoria
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
}