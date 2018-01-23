using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    public interface IScreenerCriteria
    {
        string Id { get;  }
        string DisplayName { get;  }
        CellKind ValueType { get;  }
        string DefaultOperator { get;  }
        bool StockMatch(Stock stock, ScreenerCriteriaViewModel viewModel);
    }

    public abstract class AbstractCriteria : IScreenerCriteria
    {
        public abstract string Id { get; }
        public abstract string DisplayName { get; }
        public abstract CellKind ValueType { get; }
        public abstract  string DefaultOperator { get; }

        public bool StockMatch(Stock stock, ScreenerCriteriaViewModel viewModel)
        {
            if (ValueType == CellKind.Number || ValueType == CellKind.Percentage)
            {
                switch (viewModel.Operation)
                {
                    case CriteriaConstants.Greater: return GetDecimalValue(stock) > viewModel.NumberValue;
                    case CriteriaConstants.Lower: return  GetDecimalValue(stock) < viewModel.NumberValue;
                    case CriteriaConstants.IsEqual: return GetDecimalValue(stock) == viewModel.NumberValue;
                    case CriteriaConstants.Different: return GetDecimalValue(stock) != viewModel.NumberValue;
                    default: return true;
                }
            }
            else if (ValueType == CellKind.Text)
            {
                switch (viewModel.Operation)
                {
                    case CriteriaConstants.IsEqual: return !string.IsNullOrEmpty(GetStringValue(stock)) && !string.IsNullOrEmpty(viewModel.StringValue)&& GetStringValue(stock) == viewModel.StringValue;
                    case CriteriaConstants.Different: return string.IsNullOrEmpty(GetStringValue(stock)) || string.IsNullOrEmpty(viewModel.StringValue) || GetStringValue(stock) != viewModel.StringValue;
                    default: return true;
                }
            }
            return true;
        }
        

        protected abstract decimal GetDecimalValue(Stock stock);
        protected abstract string GetStringValue(Stock stock);

    }


    public class CriteriaConstants
    {
        public const string IsEqual = "eq";
        public const string Different = "neq";
        public const string Greater = "gt";
        public const string Lower = "lt";
    }
}
