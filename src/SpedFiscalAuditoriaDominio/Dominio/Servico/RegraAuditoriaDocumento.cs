using SpedFiscalAuditoriaDominio.Dominio.Entidade;
using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;

namespace SpedFiscalAuditoriaDominio.Dominio.Servico;

// RUBRICA OO 3 - HERANCA_POLIMORFISMO: Esta hierarquia modela regras extensiveis de validacao do documento.
public abstract class RegraAuditoriaDocumento
{
    public abstract string Nome { get; }

    public abstract string? Avaliar(DocumentoFiscal documento, decimal totalApurado);
}
