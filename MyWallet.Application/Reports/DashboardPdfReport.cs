using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class DashboardPdfReport : IDocument
{
    private readonly MonthlySnapshotResponseDto _data;

    public DashboardPdfReport(MonthlySnapshotResponseDto data)
    {
        _data = data;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);

            page.Content().Column(col =>
            {
                col.Item().Text("MyWallet — Fechamento Mensal")
                    .FontSize(20).Bold();

                col.Item().Text($"Receitas: {_data.TotalIncome}");
                col.Item().Text($"Despesas: {_data.TotalExpense}");
                col.Item().Text($"Saldo: {_data.Balance}");
            });
        });
    }
}
