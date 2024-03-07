using Microsoft.AspNetCore.Mvc.Filters;

namespace MyAPI
{
    public class MyEndpointFilter : IActionFilter
    {
        private readonly string _methodName;

        public MyEndpointFilter()
        {
            _methodName = GetType().Name;
        }
        public void OnActionExecuting(ActionExecutingContext context) 
        {
            Console.WriteLine($"{_methodName} executing");
        }

        public void OnActionExecuted(ActionExecutedContext context) 
        {
            Console.WriteLine($"{_methodName} executed");
        }
    }
}
