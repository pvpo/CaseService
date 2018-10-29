namespace CaseService.Services.Configuration {
    public class Persistence {
        public static string endpointURI = "https://caseservice.documents.azure.com";
        public static string authKey = "J2LBHS0JZBlZTYNnelqFReI3kcVmSJbl4OtMMf6uhvSXnpuWkUt1nU3VusOx1CtxNOLW8nWSBrokIzsFw60wYQ==";
        public static string dbName = "CaseService";
    }

    public class Swagger {
        public static string version = "v1";
        public static string title = "Case Service";
    }

    public class ServiceBus {
        public static string connectionURI = "Endpoint=sb://caseorders.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=UfUyg49Eic+4MHu5fvw6XRXqXnutPPZk5NDYzy2EUFs=";
        public static string orderQueue = "neworder";
    }
}