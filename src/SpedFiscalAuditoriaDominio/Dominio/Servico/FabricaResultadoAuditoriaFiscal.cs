using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;
using SpedFiscalAuditoriaDominio.Dominio.Servico.Interfaces;

namespace SpedFiscalAuditoriaDominio.Dominio.Servico;

// RUBRICA DDD 3 - FACTORY: Esta factory cria resultados validos sem executar a regra de auditoria.
public sealed class FabricaResultadoAuditoriaFiscal : IFabricaResultadoAuditoriaFiscal
{
    public ResultadoAuditoriaFiscal Aprovar(decimal totalApurado)
    {
        return new ResultadoAuditoriaFiscal(true, totalApurado, Array.Empty<string>());
    }

    public ResultadoAuditoriaFiscal Reprovar(decimal totalApurado, params string[] mensagens)
    {
        var mensagensSeguras = mensagens?.Where(mensagem => !string.IsNullOrWhiteSpace(mensagem)).ToArray()
            ?? Array.Empty<string>();

        if (mensagensSeguras.Length == 0)
        {
            mensagensSeguras = new[] { "Auditoria reprovada sem detalhamento adicional." };
        }

        return new ResultadoAuditoriaFiscal(false, totalApurado, mensagensSeguras);
    }
}
