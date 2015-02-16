using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OpenXstan.Common;

namespace OpenXstan.Web.Api
{
    public class ProductApiController : ApiController
    {
        private List<Product> _productList = null;

        public ProductApiController()
        {
            _productList = new List<Product>()
            {
                new Product() { Id="101", Title ="Dell Inspiron", CurrentPrice= 545.3M, EndTime = DateTime.Now.AddMonths(1) },
                new Product() { Id="102", Title ="Sony Vaio", CurrentPrice= 556M, EndTime = DateTime.Now.AddMonths(2) },
                new Product() { Id="103", Title ="iPhone 5", CurrentPrice= 245.3M, EndTime = DateTime.Now.AddMonths(3) }
            };
        }

        // GET api/productapi
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/productapi/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/productapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/productapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/productapi/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        public IQueryable<Product> GetProduct()
        {
            return _productList.AsQueryable();
        }

        [HttpGet]
        public Product GetProduct(int id)
        {
            var product = _productList.Where(t => Convert.ToInt32(t.Id) == id);
            var data = new Product();

            if (product != null && product.Count() == 0)
            {
                var message = new HttpResponseMessage(HttpStatusCode.NotFound);
                message.Content = new StringContent(string.Format("Invalid Id, No product available for id: {0}", id));
                message.ReasonPhrase = "Not Found";

                throw new HttpResponseException(message);
            }
            else
            {
                data = product.First();
            }
            return data;
        }
    }
}