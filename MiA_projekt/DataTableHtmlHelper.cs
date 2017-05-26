using MiA_projekt.Extensions;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MiA_projekt
{
    public class DataTableHtmlHelper<T> where T : class
    {
        public IEnumerable<Button> Buttons { get; set; }

        public string DataSource { get; set; }

        private List<Column> Columns { get; } = new List<Column>();
        private StringBuilder _html;

        public IHtmlContent Render()
        {
            ReadColumns();
            RenderTable();

            return new HtmlString(_html.ToString());
        }

        private void ReadColumns()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var column = new Column(property.Name);

                var displayName = property.GetCustomAttributes<DisplayNameAttribute>().ToList();
                if (displayName.Any())
                    column.DisplayName = displayName[0].DisplayName;
                else
                    column.DisplayName = property.Name;

                var orderable = property.GetCustomAttributes<OrderableAttribute>().ToList();
                if (orderable.Any())
                    column.Orderable = orderable[0].Value;

                var searchable = property.GetCustomAttributes<SearchableAttribute>().ToList();
                if (searchable.Any())
                    column.Searchable = searchable[0].Value;

                var link = property.GetCustomAttributes<LinkAttribute>().ToList();
                if (link.Any())
                    column.Link = link[0];

                var onClick = property.GetCustomAttributes<OnClickAttribute>().ToList();
                if (onClick.Any())
                    column.OnClick = onClick[0];

                var multiline = property.GetCustomAttributes<MultilineAttribute>().ToList();
                if (multiline.Any())
                    continue; // we don't want to display multiline fields in table

                Columns.Add(column);
            }
        }

        private void RenderTable()
        {
            _html = new StringBuilder();

            _html.AppendLine("<table id=\"datatable\" class=\"table table-striped table-bordered table-hover\">");
            _html.AppendLine("<thead>");
            _html.AppendLine("<tr>");

            foreach (var c in Columns)
                _html.AppendLine("<th>" + c.DisplayName + "</th>");

            foreach (var b in Buttons)
                _html.AppendLine("<th>Action</th>");

            _html.AppendLine("</tr>");
            _html.AppendLine("</thead>");
            _html.AppendLine("<tbody></tbody>");
            _html.AppendLine("</table>");
        }

        public IHtmlContent RenderScipt()
        {
            var html = new StringBuilder();

            html.AppendLine("<script>");
            html.AppendLine("$(document).ready(function () {");
            html.AppendLine("var table = $(\"#datatable\").DataTable({");
            html.AppendLine("responsive: true,");
            html.AppendLine("processing: true,");
            html.AppendLine("serverSide: true,");
            html.AppendLine("ajax: {");
            html.AppendLine("url: \"" + DataSource + "\",");
            html.AppendLine("type: 'POST',");
            html.AppendLine("dataSrc: \"data\"");
            html.AppendLine("},");
            html.AppendLine("columns: [");

            foreach (var c in Columns)
                html.AppendLine(c.GetJsColumn());

            foreach (var b in Buttons)
                html.AppendLine(b.GetJsColumn());

            html.AppendLine("]");
            html.AppendLine("});");
            html.AppendLine("$(\"div.dataTables_filter input\").unbind();");
            html.AppendLine("$(\"div.dataTables_filter input\").on('keydown', function (e) {");
            html.AppendLine("if (e.which == 13) {");
            html.AppendLine("table.search($(\"div.dataTables_filter input\").val()).draw();");
            html.AppendLine("}");
            html.AppendLine("});");

            foreach (var b in Buttons)
                html.AppendLine(b.GetClickJsScript());

            html.AppendLine("});");
            html.AppendLine("</script>");

            return new HtmlString(html.ToString());
        }
    }

    public class Column
    {
        /// <summary>
        /// The same as used in Json response.
        /// </summary>
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool Searchable { get; set; } = true;

        public bool Orderable { get; set; } = true;

        public LinkAttribute Link { get; set; }

        public OnClickAttribute OnClick { get; set; }

        public Column()
        { }

        public Column(string name)
        {
            Name = name;
        }

        public string GetJsColumn()
        {
            string s = "{ data: \"" + Name.ToCamelCase() + "\"";

            if (!Orderable)
                s += ", \"orderable\": false";

            if (!Searchable)
                s += ", \"searchable\": false";

            if (Link != null)
                s += ", render: function (data, type, item) { return \"<a href=\\\"" + Link.Href + "\\\">\" + " + Link.Name + " + \"</a>\"; }";

            if (OnClick != null)
                s += ", render: function (data, type, item) { return \"<a href=\\\"#\\\" onclick=\\\"" + OnClick.FunctionName + "\\\">\" + " + OnClick.Name + " + \"</a>\"; }";

            return s + " },";
        }
    }

    public class Button
    {
        public string Name { get; set; }

        public string DataId { get; set; }

        public string ConfirmMsg { get; set; }

        public string ActionUri { get; set; }

        public bool AddAntiForgeryToken { get; set; }

        public string GetJsColumn()
        {
            var s = new StringBuilder();
            s.AppendLine("{");
            s.AppendLine("render: function (data, type, item) {");
            s.AppendLine("return \"<button data-" + Name.ToLower() + "-id=\\\"\" + item." + DataId + " + \"\\\"  class=\\\"btn btn-danger js-" + Name.ToLower() + "\\\">" + Name + "</button>\";");
            s.AppendLine("}, \"orderable\": false, \"searchable\": false");
            s.AppendLine("}");
            return s.ToString();
        }

        public string GetClickJsScript()
        {
            var s = new StringBuilder();

            s.AppendLine("$(\"#datatable\").on(\"click\", \".js-" + Name.ToLower() + "\", function () {");
            s.AppendLine("var button = $(this);");
            s.AppendLine("toastr.options = { \"closeButton\": true, \"progressBar\": true, \"timeOut\": \"7000\", \"extendedTimeOut\": \"1500\" };");
            s.AppendLine("swal({");
            s.AppendLine("title: \"Confirmation required!\",");
            s.AppendLine("text: \"" + ConfirmMsg + " (\" + button.attr(\"data-" + Name.ToLower() + "-id\") + \")?\",");
            s.AppendLine("type: \"warning\",");
            s.AppendLine("showCancelButton: true,");
            s.AppendLine("confirmButtonColor: \"#DD6B55\",");
            s.AppendLine("confirmButtonText: \"Yes, delete it!\",");
            s.AppendLine("cancelButtonText: \"No\"");
            s.AppendLine("},");
            s.AppendLine("function(isConfirm){");
            s.AppendLine("if (isConfirm) {");
            s.AppendLine("$.ajax({");
            s.AppendLine("url: '" + ActionUri + "' + button.attr(\"data-" + Name.ToLower() + "-id\"),");
            s.AppendLine("type: 'DELETE',");
            s.AppendLine("contentType: 'application/json; charset=utf-8',");
            s.AppendLine("success: function () {");
            s.AppendLine("toastr.success(\"Item has been removed\");");
            s.AppendLine("table.ajax.reload();");
            s.AppendLine("},");
            s.AppendLine("error: function () {");
            s.AppendLine("toastr.error(\"Error removing item\");");
            s.AppendLine("}");
            s.AppendLine("});");
            s.AppendLine("}");
            s.AppendLine("})");
            s.AppendLine("});");

            return s.ToString();
        }
    }

    public class SearchableAttribute : Attribute
    {
        public bool Value { get; }

        public SearchableAttribute(bool value)
        {
            Value = value;
        }
    }

    public class OrderableAttribute : Attribute
    {
        public bool Value { get; }

        public OrderableAttribute(bool value)
        {
            Value = value;
        }
    }

    public class LinkAttribute : Attribute
    {
        public string Href { get; }

        public string Name { get; }

        public LinkAttribute(string href, string name)
        {
            Href = href;
            Name = name;
        }
    }

    public class OnClickAttribute : Attribute
    {
        public string FunctionName { get; }

        public string Name { get; }

        public OnClickAttribute(string functionName, string name)
        {
            FunctionName = functionName;
            Name = name;
        }
    }

    public class MultilineAttribute : Attribute
    { }
}
