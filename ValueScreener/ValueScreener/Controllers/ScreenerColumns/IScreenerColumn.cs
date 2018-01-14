using System.Linq;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public interface IScreenerColumn
    {
        string DisplayName { get; }
        ScreenerCellViewModel GetCell(Stock stock);
    }

    public class EnterpriseMultipleScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.EnterpriseMultipleDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal em = 0;
            if (stock.PricingResult != null)
                em = stock.PricingResult.EnterpriseMultiple;


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = em,
                StockId = stock.Id
            };
        }
    }

    public class PerScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.PerDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal per = 0;
            if (stock.PricingResult != null)
                per = stock.PricingResult.PriceEarningRatio;


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = per,
                StockId = stock.Id
            };
        }
    }

    public class AvgPiotroskiScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.AvgPiotroskiDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal piotroski = 0;
            if (stock.PricingResult != null && stock.PricingResult.PiotroskiResults != null &&
                stock.PricingResult.PiotroskiResults.Any())
                piotroski = (decimal)stock.PricingResult.PiotroskiResults.Average(r => r.GlobalFScore);


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = piotroski,
                StockId = stock.Id
            };
        }
    }

    public class PiotroskiScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.PiotroskiDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            int piotroski = 0;
            if (stock.PricingResult != null && stock.PricingResult.PiotroskiResults != null &&
                stock.PricingResult.PiotroskiResults.Any())
                piotroski = stock.PricingResult.PiotroskiResults.OrderByDescending(x=>x.Year).First().GlobalFScore;


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = piotroski,
                StockId = stock.Id
            };
        }
    }

    public class AvgRoicScreenerColumn : IScreenerColumn
   {
       public string DisplayName => ColumnConstants.AvgRoicDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal roic = 0;
            if (stock.PricingResult != null && stock.PricingResult.AnnualResults != null &&
                stock.PricingResult.AnnualResults.Any())
                roic = stock.PricingResult.AnnualResults.Average(r => r.ReturnOnInvestedCapital);


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Percentage,
                IsBold = false,
                IsLink = false,
                PercentageValue = roic,
                StockId = stock.Id
            };
        }
    }

    class RoicScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.RoicDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal roic = 0;
            if (stock.PricingResult != null && stock.PricingResult.AnnualResults != null &&
                stock.PricingResult.AnnualResults.Any())
                roic = stock.PricingResult.AnnualResults.OrderByDescending(x => x.Year).First().ReturnOnInvestedCapital;


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Percentage,
                IsBold = false,
                IsLink = false,
                PercentageValue = roic,
                StockId = stock.Id
            };
        }
    }

    class AvgRoeScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.AvgRoeDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal roe = 0;
            if (stock.PricingResult != null && stock.PricingResult.AnnualResults != null &&
                stock.PricingResult.AnnualResults.Any())
                roe = stock.PricingResult.AnnualResults.Average(r => r.ReturnOnEquity);


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Percentage,
                IsBold = false,
                IsLink = false,
                PercentageValue = roe,
                StockId = stock.Id
            };
        }
    }

    class RoeScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.RoeDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal roe = 0;
            if (stock.PricingResult != null && stock.PricingResult.AnnualResults != null &&
                stock.PricingResult.AnnualResults.Any())
                roe = stock.PricingResult.AnnualResults.OrderByDescending(x => x.Year).First().ReturnOnEquity;


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Percentage,
                IsBold = false,
                IsLink = false,
                PercentageValue = roe,
                StockId = stock.Id
            };
        }
    }

    public class AvgRoaScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.AvgRoaDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal roa = 0;
            if (stock.PricingResult != null && stock.PricingResult.AnnualResults != null &&
                stock.PricingResult.AnnualResults.Any())
                roa = stock.PricingResult.AnnualResults.Average(r => r.ReturnOnAssets);


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Percentage,
                IsBold = false,
                IsLink = false,
                PercentageValue = roa,
                StockId = stock.Id
            };
        }
    }

    public class RoaScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.RoaDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {

            decimal roa = 0;
            if (stock.PricingResult != null && stock.PricingResult.AnnualResults != null &&
                stock.PricingResult.AnnualResults.Any())
                roa = stock.PricingResult.AnnualResults.OrderByDescending(x => x.Year).First().ReturnOnAssets;


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Percentage,
                IsBold = false,
                IsLink = false,
                PercentageValue = roa,
                StockId = stock.Id
            };
        }
    }

    public class SectorScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.SectorDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Text,
                IsBold = false,
                IsLink = false,
                StringValue = stock.Sector,
                StockId = stock.Id
            };
        }
    }

    public class CountryScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.CountryDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Text,
                IsBold = false,
                IsLink = false,
                StringValue = stock.Country,
                StockId = stock.Id
            };
        }
    }

    public class PriceToSalesScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.PriceToSalesDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Percentage,
                IsBold = false,
                IsLink = false,
                PercentageValue = stock.PricingResult.PriceToSalesRatio,
                StockId = stock.Id
            };
        }
    }

    public class NcavDiscountScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.NcavDiscountDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Percentage,
                IsBold = false,
                IsLink = false,
                PercentageValue = stock.PricingResult.DiscountOnNcav,
                StockId = stock.Id
            };
        }
    }

    public class TickerScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.TickerDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Text,
                IsBold = false,
                IsLink = true,
                StringValue = stock.Ticker,
                StockId = stock.Id
            };
        }
    }

    public class CompanyNameScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.CompanyNameDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Text,
                IsBold = false,
                IsLink = true,
                StringValue = stock.Name,
                StockId = stock.Id
            };
        }
    }
}