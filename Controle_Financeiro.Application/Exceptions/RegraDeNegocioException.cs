namespace Controle_Financeiro.Application.Exceptions;

public class RegraDeNegocioException : Exception
{
    public RegraDeNegocioException(string mensagem)
        : base(mensagem)
    {
    }
}