using SpedFiscalAuditoriaDominio.Dominio.Entidade;
using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;

namespace SpedFiscalAuditoriaDominio.Dominio.Servico.Interfaces;

public interface IAuditorFiscal
{
    ResultadoAuditoriaFiscal Auditar(DocumentoFiscal documento);
}
