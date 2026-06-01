using SpedFiscalAuditoriaDominio.Dominio.Excecoes;

namespace SpedFiscalAuditoriaDominio.Dominio.Entidade;

// RUBRICA DDD 2 - AGREGADO: Esta entidade atua como raiz do agregado de documentos fiscais.
// RUBRICA OO 2 / DDD 1 - SRP: Esta entidade agrega os dados essenciais da nota sem conhecer a regra de auditoria.
public sealed class DocumentoFiscal
{
    private readonly IReadOnlyList<ItemDocumentoFiscal> _itens;

    public DocumentoFiscal(string numero, string emitente, IEnumerable<ItemDocumentoFiscal> itens, decimal totalInformado)
    {
        if (string.IsNullOrWhiteSpace(numero))
        {
            throw new RegraNegocioExcecao("O numero do documento deve ser informado.");
        }

        if (string.IsNullOrWhiteSpace(emitente))
        {
            throw new RegraNegocioExcecao("O emitente deve ser informado.");
        }

        _itens = (itens ?? throw new ArgumentNullException(nameof(itens))).ToList().AsReadOnly();

        Numero = numero;
        Emitente = emitente;
        TotalInformado = totalInformado;
    }

    public string Numero { get; }

    public string Emitente { get; }

    public IReadOnlyList<ItemDocumentoFiscal> Itens => _itens;

    public decimal TotalInformado { get; }

    public decimal SomarTotalItens()
    {
        return _itens.Sum(item => item.Total);
    }
}
