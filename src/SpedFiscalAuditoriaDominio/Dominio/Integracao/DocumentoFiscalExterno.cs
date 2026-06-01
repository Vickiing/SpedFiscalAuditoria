namespace SpedFiscalAuditoriaDominio.Dominio.Integracao;

public sealed class DocumentoFiscalExterno
{
    public string Numero { get; init; } = string.Empty;

    public string Emitente { get; init; } = string.Empty;

    public decimal TotalInformado { get; init; }

    public List<ItemDocumentoFiscalExterno> Itens { get; init; } = [];
}

public sealed class ItemDocumentoFiscalExterno
{
    public string Descricao { get; init; } = string.Empty;

    public string Ncm { get; init; } = string.Empty;

    public decimal Quantidade { get; init; }

    public decimal ValorUnitario { get; init; }
}
