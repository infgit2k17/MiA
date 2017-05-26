using MiA_projekt.Extensions;
using Microsoft.AspNetCore.Html;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MiA_projekt
{
    public class EditorFormHtmlHelper<T> where T : class
    {
        private readonly PropertyInfo[] _properties = typeof(T).GetProperties();

        public IHtmlContent Render()
        {
            StringBuilder html = new StringBuilder();

            foreach (var property in _properties)
                html.Append(RenderRow(property));

            return new HtmlString(html.ToString());
        }

        public IHtmlContent RenderResults()
        {
            StringBuilder html = new StringBuilder();

            foreach (var property in _properties)
            {
                string propId = property.Name.ToCamelCase();
                html.Append("document.getElementById(\"" + propId + "\").value = data." + propId + ";");
            }

            return new HtmlString(html.ToString());
        }

        public IHtmlContent RenderJsonData()
        {
            StringBuilder html = new StringBuilder();

            foreach (var property in _properties)
            {
                string propId = property.Name.ToCamelCase();
                if (propId != "id")
                    html.Append(propId + ": document.getElementById(\"" + propId + "\").value, ");
            }

            return new HtmlString(html.ToString());
        }

        private string RenderRow(PropertyInfo property)
        {
            string row =
                "<div class=\"form-group\"><label class=\"col-sm-2 control-label\">" + ReadDisplayName(property) +
                ":</label><div class=\"col-sm-6\">" + RenderInputForm(property) + "</div>" + RenderHelpBlock(property) + "</div>";

            return row;
        }

        private string RenderInputForm(PropertyInfo property)
        {
            if (property.PropertyType.GetTypeInfo().IsEnum)
                return "<select id=\"" + property.Name.ToCamelCase() + "\" class=\"form-control\">" + GetEnumStrings(property) + "</select>";

            var multiline = property.GetCustomAttributes<MultilineAttribute>().ToList();
            if (multiline.Any())
                return "<textarea id=\"" + property.Name.ToCamelCase() + "\" style=\"resize: none\" class=\"form-control\" rows=\"10\" placeholder=\"" + ReadDisplayName(property) + "\"></textarea>";

            return "<input id=\"" + property.Name.ToCamelCase() + "\" " + GetType(property) +
                   " class=\"form-control\" placeholder=\"" + ReadDisplayName(property) + "\">";
        }

        private string GetEnumStrings(PropertyInfo property)
        {
            string[] names = property.PropertyType.GetTypeInfo().GetEnumNames();

            string list = String.Empty;

            for (int i = 0; i < names.Length; i++)
                list += "<option value=\"" + i + "\">" + names[i] + "</option>";

            return list;
        }

        private string ReadDisplayName(PropertyInfo property)
        {
            var displayName = property.GetCustomAttributes<DisplayNameAttribute>().ToList();

            if (displayName.Any())
                return displayName[0].DisplayName;

            return property.Name;
        }

        private string RenderHelpBlock(PropertyInfo property)
        {
            TypeCode code = Type.GetTypeCode(property.PropertyType);

            if (code == TypeCode.Boolean)
                return "<span class=\"help-block\">true/false</span>";

            if (code == TypeCode.DateTime)
                return "<span class=\"help-block\">mm/dd/yyyy hh:mm</span>";

            return String.Empty;
        }

        private string GetType(PropertyInfo property)
        {
            TypeCode code = Type.GetTypeCode(property.PropertyType);

            if (code == TypeCode.Int32)
                return "type=\"number\" step=\"1\"";

            if (code == TypeCode.Single || code == TypeCode.Double)
                return "type=\"number\" step=\"0.01\"";

            return "type=\"text\"";
        }
    }
}
