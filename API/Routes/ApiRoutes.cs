namespace API.Routes
{
    public static class ApiRoutes
    {
        private static readonly string BaseUrl = "https://localhost:5001/api/";

        public static class Group
        {
            private static readonly string _groupsControllerUrl = string.Concat(BaseUrl, "group");

            public static readonly string GetAll = _groupsControllerUrl;

            public static readonly string Get = string.Concat(_groupsControllerUrl, "/{groupId}");

            public static readonly string Delete = string.Concat(_groupsControllerUrl, "/{groupId}");
        }
    }
}