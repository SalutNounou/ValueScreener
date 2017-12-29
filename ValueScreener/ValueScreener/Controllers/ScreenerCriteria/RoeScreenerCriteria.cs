using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class RoeScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.Roe;
        public override string DisplayName => ColumnConstants.RoeDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.CurrentRoe;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}