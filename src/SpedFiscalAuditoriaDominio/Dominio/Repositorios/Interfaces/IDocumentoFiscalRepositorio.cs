using SpedFiscalAuditoriaDominio.Dominio.Entidade;

namespace SpedFiscalAuditoriaDominio.Dominio.Repositorios.Interfaces;

public interface IDocumentoFiscalRepositorio
{
    void Adicionar(DocumentoFiscal documento);

    DocumentoFiscal? ObterPorNumero(string numero);
}
