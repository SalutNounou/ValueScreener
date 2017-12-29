using ValueScreener.Models.Domain;

namespace ValueScreener.Services.Valuation
{
    public interface IStockEvaluator
    {
        void EvaluateStock(Stock stock);
    }
}
