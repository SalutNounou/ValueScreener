using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValueScreener.Controllers.ScreenerCriteria;

namespace ValueScreener.Models.ViewModels
{
    public class GenericScreenerViewModel
    {
        public GenericScreenerViewModel()
        {
            AvailableCriterias = new List<SelectListItem>();
            ColumnTitles = new List<ColumnTitle>();
            Rows = new List<ScreenerRowViewModel>();
            AvailableAdditionalColumns = new Dictionary<string, string>();
        }
        public List<ScreenerCriteriaViewModel> Criterias { get; set; }
       
        public List<SelectListItem> AvailableCriterias { get; set; }
        [Display(Name = "Add New Criteria : ")]
        public string CriteriaToAdd { get; set; }

        public string CriteriaToRemove { get; set; }

        public string ColumnToAdd { get; set; }
        public string ColumnToRemove { get; set; }
        public string Columns { get; set; }


        public List<SelectListItem> CriteriaOperators { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value =CriteriaConstants.Greater, Text = "Greater Than" },
            new SelectListItem { Value = CriteriaConstants.Lower, Text = "Lower Than" },
            new SelectListItem { Value = CriteriaConstants.IsEqual, Text = "Equals"  },
            new SelectListItem { Value = CriteriaConstants.Different, Text = "Different From"  },
        };

        public List<ColumnTitle> ColumnTitles { get; }
        public List<ScreenerRowViewModel> Rows { get; }

        public string GetUrl(int pageIndex, string criteriaToRemove, string columnToAdd, string columnToRemove, string columns)
        {
            var endPoint = "/Screener/ScreenGeneric?";
            if (Criterias != null & (Criterias ?? throw new InvalidOperationException()).Any())
            {
                for (int i = 0; i < Criterias.Count; i++)
                {
                    endPoint +=$"Criterias%5B{i}%5D.Name={Criterias[i].Name}&";
                    endPoint += $"Criterias%5B{i}%5D.Operation={Criterias[i].Operation}&";
                    endPoint += $"Criterias%5B{i}%5D.NumberValue={Criterias[i].NumberValue}&";
                    endPoint += $"Criterias%5B{i}%5D.StringValue={Criterias[i].StringValue}&";
                    endPoint += $"Criterias%5B{i}%5D.ValueType={Criterias[i].ValueType}&";
                    endPoint += $"Criterias%5B{i}%5D.Id={Criterias[i].Id}&";
                }
            }
            if(!string.IsNullOrEmpty(criteriaToRemove))
                endPoint += "CriteriaToRemove=" + criteriaToRemove+"&";
            if (!string.IsNullOrEmpty(columnToAdd))
                endPoint += "ColumnToAdd=" + columnToAdd + "&";
            if (!string.IsNullOrEmpty(columnToRemove))
                endPoint += "ColumnToRemove=" + columnToRemove + "&";
            if (!string.IsNullOrEmpty(columns))
                endPoint += "Columns=" + columns + "&";
            if (pageIndex > 0)
                endPoint += "PageIndex=" + pageIndex+"&";
            if (endPoint.EndsWith("&"))
                endPoint.Remove(endPoint.Length - 1);

            endPoint = endPoint.Replace(' ', '+');
            endPoint = endPoint.Replace("(", "%28");
            endPoint = endPoint.Replace(")", "%29");
            return endPoint;
        }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public Dictionary<string, string> AvailableAdditionalColumns { get; set; }

    }


  
    public class ScreenerCriteriaViewModel 
    {
        [Display(Name = "Criteria : ")]
        public string Name { get; set; }
        public string Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal NumberValue { get; set; }
        public string Operation { get; set; }

        public CellKind ValueType { get; set; }
        public string StringValue { get; set; }
    }
}
