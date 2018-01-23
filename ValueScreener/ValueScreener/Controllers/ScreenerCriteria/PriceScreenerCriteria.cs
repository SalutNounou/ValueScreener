using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class PriceScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.Price;
        public override string DisplayName => ColumnConstants.PriceDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.MarketData.LastPrice ?? 0;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}