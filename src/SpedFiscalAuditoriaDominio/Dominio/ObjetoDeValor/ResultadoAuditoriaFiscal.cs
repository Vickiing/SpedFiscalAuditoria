namespace SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;

// RUBRICA DDD 3 - FACTORY: Este resultado recebe instancias validas produzidas pela factory de auditoria.
public sealed class ResultadoAuditoriaFiscal
{
    public ResultadoAuditoriaFiscal(bool aprovado, decimal totalApurado, IReadOnlyList<string> mensagens)
    {
        Aprovado = aprovado;
        TotalApurado = totalApurado;
        Mensagens = mensagens;
    }

    public bool Aprovado { get; }

    public decimal TotalApurado { get; }

    public IReadOnlyList<string> Mensagens { get; }
}
