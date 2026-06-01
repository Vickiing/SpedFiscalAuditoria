using SpedFiscalAuditoriaDominio.Dominio.Excecoes;
using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;

namespace SpedFiscalAuditoriaDominio.Dominio.Entidade;

// RUBRICA OO 2 / DDD 1 - SRP: Este item concentra as validações e o cálculo do valor total da linha fiscal.
public sealed class ItemDocumentoFiscal
{
    public ItemDocumentoFiscal(string descricao, Ncm ncm, decimal quantidade, decimal valorUnitario)
    {
        if (string.IsNullOrWhiteSpace(descricao))
        {
            throw new RegraNegocioExcecao("A descricao do item deve ser informada.");
        }

        if (ncm is null)
        {
            throw new ArgumentNullException(nameof(ncm));
        }

        if (quantidade <= 0)
        {
            throw new RegraNegocioExcecao("A quantidade deve ser maior que zero.");
        }

        if (valorUnitario < 0)
        {
            throw new RegraNegocioExcecao("O valor unitario nao pode ser negativo.");
        }

        Descricao = descricao;
        Ncm = ncm;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    public string Descricao { get; }

    public Ncm Ncm { get; }

    public decimal Quantidade { get; }

    public decimal ValorUnitario { get; }

    public decimal Total => Quantidade * ValorUnitario;
}
