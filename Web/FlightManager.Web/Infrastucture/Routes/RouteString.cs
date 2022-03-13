namespace FlightManager.Web.Infrastucture.Routes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FlightManager.Common;
    using FlightManager.Web.Infrastucture.Extensions;

    public class RouteString
    {
        private const char Slash = '/';

        public int QueryStringPairsCount { get; private set; }

        public string Value { get; private set; }

        public RouteString(string controller, string action)
        {
            Value = $"{Slash}{controller.ToControllerName()}{Slash}{action}";
        }

        public RouteString(string area, string controller, string action) : this(controller, action)
        {
            Value = $"{Slash}{area}{Value}";
        }

        public RouteString AppendId(object id)
        {
            Value += $"{Slash}{id}";
            return this;
        }

        public RouteString Append(string key, object value)
        {
            if (QueryStringPairsCount == 0)
            {
                AppendQueryStringSymbol();
            }

            string pair = $"{key}={value}";
            QueryStringPairsCount++;

            if (QueryStringPairsCount > 1)
            {
                pair = GlobalConstants.QueryStringDelimiter + pair;
            }

            Value += pair;
            return this;
        }

        public RouteString AppendPaginationPlaceholder() => Append(GlobalConstants.PageKey, "{0}");

        public RouteString AppendItemPerPagePlaceholder() => Append(GlobalConstants.ItemsPerPageKey, "{1}");

        private void AppendQueryStringSymbol() => Value += "?";

        public static implicit operator string(RouteString route) => route.Value;
    }
}
