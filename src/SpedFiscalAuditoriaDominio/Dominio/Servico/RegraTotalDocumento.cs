using SpedFiscalAuditoriaDominio.Dominio.Entidade;
using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;

namespace SpedFiscalAuditoriaDominio.Dominio.Servico;

public sealed class RegraTotalDocumento : RegraAuditoriaDocumento
{
    public override string Nome => "Total do documento";

    public override string? Avaliar(DocumentoFiscal documento, decimal totalApurado)
    {
        if (totalApurado != documento.TotalInformado)
        {
            return $"Total informado {documento.TotalInformado} difere do total apurado {totalApurado}.";
        }

        return null;
    }
}
