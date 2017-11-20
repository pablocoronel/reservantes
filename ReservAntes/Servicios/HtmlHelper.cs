using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ReservAntes.Servicios
{

    public static class HttpHelper
        {
            public static string AsQueryString(this List<KeyValuePair<string, object>> parameters)
            {
                if (!parameters.Any())
                    return "";

                var builder = new StringBuilder("?");

                var separator = "";
                foreach (var kvp in parameters.Where(kvp => kvp.Value != null))
                {
                    builder.AppendFormat("{0}{1}={2}", separator, kvp.Key, kvp.Value);

                    separator = "&";
                }

                return builder.ToString();
            }
        }
    
}