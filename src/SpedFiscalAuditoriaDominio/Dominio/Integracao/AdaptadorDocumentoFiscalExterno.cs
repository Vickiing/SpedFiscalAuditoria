using SpedFiscalAuditoriaDominio.Dominio.Entidade;
using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;

namespace SpedFiscalAuditoriaDominio.Dominio.Integracao;

// RUBRICA DDD 4 - ANTI_CORRUPTION_LAYER: Esse adaptador isola o dominio de um formato externo bruto.
// RUBRICA DDD 4 - CONTEXT_MAP: Esse adaptador faz a traducao entre o contexto externo e o contexto de Auditoria Fiscal.
public static class AdaptadorDocumentoFiscalExterno
{
    public static DocumentoFiscal ParaDocumentoFiscal(DocumentoFiscalExterno externo)
    {
        if (externo is null)
        {
            throw new ArgumentNullException(nameof(externo));
        }

        var itens = externo.Itens.Select(item =>
            new ItemDocumentoFiscal(item.Descricao, new Ncm(item.Ncm), item.Quantidade, item.ValorUnitario));

        return new DocumentoFiscal(externo.Numero, externo.Emitente, itens, externo.TotalInformado);
    }
}
