using System.Web;
using System.Web.Mvc;

namespace TAD_Treinamento_a_distancia
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}