using SpedFiscalAuditoriaDominio.Dominio.Excecoes;

namespace SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;

// RUBRICA OO 2 / DDD 1 - SRP: Esta classe representa e valida um NCM sem misturar regra fiscal de auditoria.
public sealed class Ncm
{
    public Ncm(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
        {
            throw new RegraNegocioExcecao("O NCM deve ser informado.");
        }

        if (codigo.Length != 8)
        {
            throw new RegraNegocioExcecao("O NCM deve conter exatamente 8 digitos.");
        }

        if (!codigo.All(char.IsDigit))
        {
            throw new RegraNegocioExcecao("O NCM deve conter apenas digitos.");
        }

        Codigo = codigo;
    }

    public string Codigo { get; }

    public override string ToString() => Codigo;
}
