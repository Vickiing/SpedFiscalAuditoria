namespace SpedFiscalAuditoriaDominio.Dominio.Excecoes;

public sealed class RegraNegocioExcecao : Exception
{
    public RegraNegocioExcecao(string mensagem)
        : base(mensagem)
    {
    }
}
