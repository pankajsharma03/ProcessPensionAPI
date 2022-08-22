using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using ProcessPensionAPI.Const;
using System.Net.Http;

namespace ProcessPensionAPI.Filters
{
    public class ProcessPensionAuthorization : ActionFilterAttribute, IAuthorizationFilter

    {

        public void OnAuthorization(AuthorizationFilterContext context)

        {


            var token = context.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            if (token == null)

            {

                context.Result = new UnauthorizedResult();

            }

            else

            {

                TokenHelper.TokenString = token;

                Client obj = new Client();

                HttpClient client = obj.Authapi();

                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);

                HttpResponseMessage res = client.GetAsync("api/Auth/").Result;

                if (!res.IsSuccessStatusCode)

                {

                    context.Result = new UnauthorizedResult();

                }

            }



        }

    }
}
