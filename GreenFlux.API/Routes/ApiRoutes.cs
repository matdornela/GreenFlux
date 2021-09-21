namespace GreenFlux.API.Routes
{
    public static class ApiRoutes
    {
        private static readonly string BaseUrl = "https://localhost:5001/api/";

        public static class Group
        {
            private static readonly string _groupsControllerUrl = string.Concat(BaseUrl, "group");

            public static readonly string GetAll = _groupsControllerUrl;

            public static readonly string Post = _groupsControllerUrl;

            public static readonly string Put = _groupsControllerUrl;

            public static readonly string Get = string.Concat(_groupsControllerUrl, "/{groupId}");

            public static readonly string Delete = string.Concat(_groupsControllerUrl, "/{groupId}");
        }

        public static class Connector
        {
            private static readonly string _connectorControllerUrl = string.Concat(BaseUrl, "connector");

            public static readonly string GetAll = _connectorControllerUrl;

            public static readonly string Post = _connectorControllerUrl;

            public static readonly string Put = _connectorControllerUrl;

            public static readonly string Get = string.Concat(_connectorControllerUrl, "/{connectorId}");

            public static readonly string Delete = string.Concat(_connectorControllerUrl, "/{connectorId}");
        }

        public static class ChargeStation
        {
            private static readonly string _chargeStationControllerUrl = string.Concat(BaseUrl, "chargeStation");

            public static readonly string GetAll = _chargeStationControllerUrl;

            public static readonly string Post = _chargeStationControllerUrl;

            public static readonly string Put = _chargeStationControllerUrl;

            public static readonly string Get = string.Concat(_chargeStationControllerUrl, "/{chargeStationId}");

            public static readonly string Delete = string.Concat(_chargeStationControllerUrl, "/{chargeStationId}");
        }
    }
}