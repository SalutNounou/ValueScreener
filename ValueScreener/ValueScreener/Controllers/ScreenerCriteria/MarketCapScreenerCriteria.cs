using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class MarketCapScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.MarketCap;
        public override string DisplayName => ColumnConstants.MarketCapDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.MarketData.MarketCapitalization??0;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}