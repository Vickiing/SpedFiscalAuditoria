using Moq;
using SpedFiscalAuditoriaDominio.Dominio.Entidade;
using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;
using SpedFiscalAuditoriaDominio.Dominio.Servico;
using SpedFiscalAuditoriaDominio.Dominio.Servico.Interfaces;

namespace SpedFiscalAuditoriaDominio.Testes.Dominio.Auditoria;

public sealed class AuditorFiscalTestes
{
    // RUBRICA TESTES 15 - Isolamento: este teste usa stub manual para isolar a dependencia da fabrica.
    [Fact]
    public void Deve_aprovar_documento_quando_total_confere()
    {
        var fabrica = new FabricaResultadoAuditoriaFiscalStub();
        var auditor = new AuditorFiscal(fabrica);
        var documento = CriarDocumento(20m);

        var resultado = auditor.Auditar(documento);

        Assert.True(resultado.Aprovado);
        Assert.Equal(20m, fabrica.TotalRecebido);
    }

    // RUBRICA TESTES 15 - Isolamento: este teste usa mock para isolar o comportamento da fabrica.
    [Fact]
    public void Deve_reprovar_documento_quando_total_diverge()
    {
        var fabrica = new Mock<IFabricaResultadoAuditoriaFiscal>(MockBehavior.Strict);
        fabrica.Setup(x => x.Reprovar(20m, It.IsAny<string[]>()))
            .Returns((decimal totalApurado, string[] mensagens) => new ResultadoAuditoriaFiscal(false, totalApurado, mensagens));

        var auditor = new AuditorFiscal(fabrica.Object);
        var documento = CriarDocumento(99m);

        var resultado = auditor.Auditar(documento);

        Assert.False(resultado.Aprovado);
        Assert.Contains(resultado.Mensagens, mensagem => mensagem.Contains("Total informado", StringComparison.OrdinalIgnoreCase));
        fabrica.Verify(x => x.Reprovar(20m, It.IsAny<string[]>()), Times.Once);
    }

    // RUBRICA TESTES 15 - Isolamento: este teste usa mock para isolar o comportamento da fabrica.
    [Fact]
    public void Deve_reprovar_documento_sem_itens()
    {
        var fabrica = new Mock<IFabricaResultadoAuditoriaFiscal>(MockBehavior.Strict);
        fabrica.Setup(x => x.Reprovar(0m, It.IsAny<string[]>()))
            .Returns((decimal totalApurado, string[] mensagens) => new ResultadoAuditoriaFiscal(false, totalApurado, mensagens));

        var auditor = new AuditorFiscal(fabrica.Object);
        var documento = new DocumentoFiscal("NF-1", "Fornecedor", Array.Empty<ItemDocumentoFiscal>(), 0m);

        var resultado = auditor.Auditar(documento);

        Assert.False(resultado.Aprovado);
        Assert.Contains(resultado.Mensagens, mensagem => mensagem.Contains("nao possui itens", StringComparison.OrdinalIgnoreCase));
        fabrica.Verify(x => x.Reprovar(0m, It.IsAny<string[]>()), Times.Once);
    }

    // RUBRICA TESTES 15 - Isolamento: este teste verifica comportamento de extensibilidade com uma regra de teste.
    [Fact]
    public void Deve_ignorar_mensagem_em_branco_da_regra()
    {
        var fabrica = new FabricaResultadoAuditoriaFiscalStub();
        var regras = new RegraAuditoriaDocumento[]
        {
            new RegraMensagemEmBranco()
        };

        var auditor = new AuditorFiscal(fabrica, regras);
        var documento = CriarDocumento(20m);

        var resultado = auditor.Auditar(documento);

        Assert.True(resultado.Aprovado);
        Assert.Empty(resultado.Mensagens);
    }

    // RUBRICA TESTES 15 - Isolamento: este teste valida a falha de configuracao sem depender de infraestrutura.
    [Fact]
    public void Deve_rejeitar_construtor_sem_regras()
    {
        var fabrica = new FabricaResultadoAuditoriaFiscalStub();

        var excecao = Assert.Throws<ArgumentException>(() => new AuditorFiscal(fabrica, Array.Empty<RegraAuditoriaDocumento>()));

        Assert.Contains("regra", excecao.Message, StringComparison.OrdinalIgnoreCase);
    }

    private sealed class FabricaResultadoAuditoriaFiscalStub : IFabricaResultadoAuditoriaFiscal
    {
        public decimal TotalRecebido { get; private set; }

        public ResultadoAuditoriaFiscal Aprovar(decimal totalApurado)
        {
            TotalRecebido = totalApurado;
            return new ResultadoAuditoriaFiscal(true, totalApurado, Array.Empty<string>());
        }

        public ResultadoAuditoriaFiscal Reprovar(decimal totalApurado, params string[] mensagens)
        {
            TotalRecebido = totalApurado;
            return new ResultadoAuditoriaFiscal(false, totalApurado, mensagens);
        }
    }

    private sealed class RegraMensagemEmBranco : RegraAuditoriaDocumento
    {
        public override string Nome => "Mensagem em branco";

        public override string? Avaliar(DocumentoFiscal documento, decimal totalApurado)
        {
            return " ";
        }
    }

    private static DocumentoFiscal CriarDocumento(decimal totalInformado)
    {
        var itens = new[]
        {
            new ItemDocumentoFiscal("Produto", new Ncm("12345678"), 2, 10m)
        };

        return new DocumentoFiscal("NF-1", "Fornecedor", itens, totalInformado);
    }
}
