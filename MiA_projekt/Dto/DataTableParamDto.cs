using MiA_projekt.Extensions;
using MiA_projekt.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MiA_projekt.Dto
{
    [ModelBinder(BinderType = typeof(DataTableModelBinder))]
    public class DataTableParamDto
    {
        public int Draw { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Start { get; set; }

        [Range(1, Int32.MaxValue)]
        public int Length { get; set; }

        [Required]
        public DTSearch Search { get; set; }

        [Required]
        public IEnumerable<DTOrder> Order { get; set; }

        [Required]
        public IEnumerable<DTColumn> Columns { get; set; }

        public string GetColumnName()
        {
            DTOrder order = Order.First();

            return Columns.ToList()[order.Column].Data.ToPascalCase();
        }

        public string GetOrderBy()
        {
            DTOrder order = Order.First();

            string columnName = Columns.ToList()[order.Column].Data;
            return columnName + " " + order.Dir;
        }

        public string GetSearchCommand<T>()
        {
            decimal temp;
            bool isFilterNumeric = Decimal.TryParse(Search.Value, out temp);

            var commands = new List<string>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var searchable = property.GetCustomAttributes<SearchableAttribute>().ToList();
                if (!searchable.Any() || searchable[0].Value)
                {
                    if (property.PropertyType == typeof(string))
                        commands.Add(property.Name + ".Contains(\"" + Search.Value + "\")");
                    else if (isFilterNumeric && (property.PropertyType == typeof(int) || property.PropertyType == typeof(double)))
                        commands.Add(property.Name + "=" + Search.Value);
                }
            }

            return String.Join(" Or ", commands);
        }
    }

    public sealed class DTSearch
    {
        public string Value { get; set; }

        public bool Regex { get; set; }
    }

    public sealed class DTOrder
    {
        [Range(0, Int32.MaxValue)]
        public int Column { get; set; }

        [Required]
        [RegularExpression(@"(asc|desc)")]
        public string Dir { get; set; }

        public static readonly string Ascending = "asc";

        public static readonly string Descending = "desc";
    }

    public sealed class DTColumn
    {
        [Required]
        public string Data { get; set; }

        public string Name { get; set; }

        public bool Orderable { get; set; }

        public bool Searchable { get; set; }

        [Required]
        public DTSearch Search { get; set; }
    }
}
