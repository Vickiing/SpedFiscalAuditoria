using SpedFiscalAuditoriaDominio.Dominio.Excecoes;
using SpedFiscalAuditoriaDominio.Dominio.Entidade;
using SpedFiscalAuditoriaDominio.Dominio.ObjetoDeValor;
using SpedFiscalAuditoriaDominio.Dominio.Servico.Interfaces;

namespace SpedFiscalAuditoriaDominio.Dominio.Servico;

// RUBRICA DDD 4 - BOUNDED_CONTEXT: Esse servico pertence ao contexto delimitado de Auditoria Fiscal.
// RUBRICA SOLID/GRASP 4 - CONTROLLER: Esse servico coordena a auditoria sem carregar detalhes de infraestrutura.
// RUBRICA DDD 3 - DOMAIN_SERVICE: Esse servico executa a regra de auditoria envolvendo documento, itens e valores.
// RUBRICA SOLID/GRASP 3 - LOW_COUPLING: A dependencia em uma fabrica abstrata reduz acoplamento e facilita testes.
// RUBRICA DDD 2 - AVALIACAO_AGREGADO: Esse servico coordena regras sobre o agregado DocumentoFiscal.
public sealed class AuditorFiscal : IAuditorFiscal
{
    private readonly IFabricaResultadoAuditoriaFiscal _fabricaResultado;
    private readonly IReadOnlyCollection<RegraAuditoriaDocumento> _regras;

    public AuditorFiscal(IFabricaResultadoAuditoriaFiscal fabricaResultado)
        : this(fabricaResultado, new RegraAuditoriaDocumento[]
        {
            new RegraItensDocumento(),
            new RegraTotalDocumento()
        })
    {
    }

    public AuditorFiscal(IFabricaResultadoAuditoriaFiscal fabricaResultado, IReadOnlyCollection<RegraAuditoriaDocumento> regras)
    {
        _fabricaResultado = fabricaResultado ?? throw new ArgumentNullException(nameof(fabricaResultado));
        _regras = regras ?? throw new ArgumentNullException(nameof(regras));
        if (_regras.Count == 0)
        {
            throw new ArgumentException("Ao menos uma regra de auditoria deve ser informada.", nameof(regras));
        }
    }

    public ResultadoAuditoriaFiscal Auditar(DocumentoFiscal documento)
    {
        if (documento is null)
        {
            throw new ArgumentNullException(nameof(documento));
        }

        var totalApurado = documento.SomarTotalItens();
        var mensagens = new List<string>();

        foreach (var regra in _regras)
        {
            var mensagem = regra.Avaliar(documento, totalApurado);
            if (!string.IsNullOrWhiteSpace(mensagem))
            {
                mensagens.Add(mensagem);
            }
        }

        if (mensagens.Count > 0)
        {
            return _fabricaResultado.Reprovar(totalApurado, mensagens.ToArray());
        }

        return _fabricaResultado.Aprovar(totalApurado);
    }
}
