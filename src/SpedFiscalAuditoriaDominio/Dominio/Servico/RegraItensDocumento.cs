using SpedFiscalAuditoriaDominio.Dominio.Entidade;
using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;

namespace SpedFiscalAuditoriaDominio.Dominio.Servico;

public sealed class RegraItensDocumento : RegraAuditoriaDocumento
{
    public override string Nome => "Itens do documento";

    public override string? Avaliar(DocumentoFiscal documento, decimal totalApurado)
    {
        if (documento.Itens.Count == 0)
        {
            return "O documento fiscal nao possui itens para auditar.";
        }

        return null;
    }
}
