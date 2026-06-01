using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;

namespace SpedFiscalAuditoriaDominio.Dominio.Servico.Interfaces;

public interface IFabricaResultadoAuditoriaFiscal
{
    ResultadoAuditoriaFiscal Aprovar(decimal totalApurado);

    ResultadoAuditoriaFiscal Reprovar(decimal totalApurado, params string[] mensagens);
}
