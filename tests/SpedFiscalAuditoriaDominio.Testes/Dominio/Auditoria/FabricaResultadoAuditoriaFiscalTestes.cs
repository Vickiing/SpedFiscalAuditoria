using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;
using SpedFiscalAuditoriaDominio.Dominio.Servico;
using SpedFiscalAuditoriaDominio.Dominio.Servico.Interfaces;

namespace SpedFiscalAuditoriaDominio.Testes.Dominio.Auditoria;

public sealed class FabricaResultadoAuditoriaFiscalTestes
{
    [Fact]
    public void Deve_criar_resultado_aprovado()
    {
        var fabrica = new FabricaResultadoAuditoriaFiscal();

        var resultado = fabrica.Aprovar(50m);

        Assert.True(resultado.Aprovado);
        Assert.Equal(50m, resultado.TotalApurado);
        Assert.Empty(resultado.Mensagens);
    }

    [Fact]
    public void Deve_criar_resultado_reprovado_com_mensagem_padrao()
    {
        var fabrica = new FabricaResultadoAuditoriaFiscal();

        var resultado = fabrica.Reprovar(25m, string.Empty);

        Assert.False(resultado.Aprovado);
        Assert.Single(resultado.Mensagens);
    }
}
