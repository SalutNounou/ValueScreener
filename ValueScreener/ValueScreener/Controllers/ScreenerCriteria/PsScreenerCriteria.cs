using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class PsScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.PriceToSales;
        public override string DisplayName => ColumnConstants.PriceToSalesDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.PriceToSalesRatio;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}