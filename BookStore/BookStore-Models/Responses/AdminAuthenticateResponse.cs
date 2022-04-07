using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Models.Responses
{
    public class AdminAuthenticateResponse
    {
        public AdminAuthenticateResponse()
        {
            this.Token = "";
            this.ResponseMesssage = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.Unauthorized };
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public int ShopId { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
        public HttpResponseMessage ResponseMesssage { get; set; }
    }
}
