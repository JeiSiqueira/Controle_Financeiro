namespace Controle_Financeiro.Domain.Entities;

public class Transacao
{
    //chave primaria //
    public int Id { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public DateTime Data { get; set; }

    public string Tipo { get; set; } = string.Empty;

    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; } 
}