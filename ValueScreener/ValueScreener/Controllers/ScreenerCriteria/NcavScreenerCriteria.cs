using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class NcavScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.Ncav;
        public override string DisplayName => ColumnConstants.NcavDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.NetCurrentAssetValue;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}