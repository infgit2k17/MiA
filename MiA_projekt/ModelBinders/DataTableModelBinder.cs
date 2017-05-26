using MiA_projekt.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiA_projekt.ModelBinders
{
    public class DataTableModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var request = bindingContext.HttpContext.Request;
            // Retrieve request data

            var draw = Convert.ToInt32(request.ReadValue("draw"));
            var start = Convert.ToInt32(request.ReadValue("start"));
            var length = Convert.ToInt32(request.ReadValue("length"));
            // Search
            var search = new DTSearch
            {
                Value = request.ReadValue("search[value]"),
                Regex = Convert.ToBoolean(request.ReadValue("search[regex]"))
            };
            // Order
            var o = 0;
            var order = new List<DTOrder>();
            while (request.ReadValue("order[" + o + "][column]") != null)
            {
                order.Add(new DTOrder
                {
                    Column = Convert.ToInt32(request.ReadValue("order[" + o + "][column]")),
                    Dir = request.ReadValue("order[" + o + "][dir]")
                });
                o++;
            }
            // Columns
            var c = 0;
            var columns = new List<DTColumn>();
            while (request.ReadValue("columns[" + c + "][name]") != null)
            {
                columns.Add(new DTColumn
                {
                    Data = request.ReadValue("columns[" + c + "][data]"),
                    Name = request.ReadValue("columns[" + c + "][name]"),
                    Orderable = Convert.ToBoolean(request.ReadValue("columns[" + c + "][orderable]")),
                    Searchable = Convert.ToBoolean(request.ReadValue("columns[" + c + "][searchable]")),
                    Search = new DTSearch
                    {
                        Value = request.ReadValue("columns[" + c + "][search][value]"),
                        Regex = Convert.ToBoolean(request.ReadValue("columns[" + c + "][search][regex]"))
                    }
                });
                c++;
            }

            bindingContext.Result = ModelBindingResult.Success(new DataTableParamDto
                {
                    Draw = draw,
                    Start = start,
                    Length = length,
                    Search = search,
                    Order = order,
                    Columns = columns
                });

            return TaskCache.CompletedTask;
        }
    }

    public static class HttpRequestExtensions
    {
        public static string ReadValue(this HttpRequest request, string keyName)
        {
            StringValues value = request.Form[keyName];

            if (!value.Any())
                return null;

            return value[0];
        }
    }
}
