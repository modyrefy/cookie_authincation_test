using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cookie_authincation_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthincateController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AuthincateAsync(UserAuthincateRequest request)
        {
            UserAuthincateResponse response = null;
            if (request.UserName == "abc" && request.Password == "abc")
            {
                response = new UserAuthincateResponse() { 
                UserId=Guid.NewGuid().ToString(),
                    UserName="abc",
                    EmailAddress="abc@gmail.com",
                    CreationDate=DateTime.Now
                };
                List<Claim> claims = new List<Claim>()
                {
                    new Claim("UserId", response.UserId.ToString()),
                     new Claim("UserName", response.UserName),
                    new Claim("EmailAddress", response.EmailAddress),
                };
                //ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                //ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                //await HttpContext.SignInAsync(principal);

                var claimsIdentity = new ClaimsIdentity(
        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }

                return response ==null?new  UnauthorizedObjectResult(new UserAuthincateResponse() { }):Ok(response);
        }
        [Authorize]
        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok(DateTime.Now.ToString());
        }

        [HttpGet]
        [Route("logout")]
        public async void LogOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
