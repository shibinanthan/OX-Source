using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenXstan.Web.Controllers;
using OpenXstan.Web.Helpers;
using OpenXstan.Common;
using OpenXstan.Web.Interfaces;
using Rhino.Mocks;
using System.Collections.Generic;
using OpenXstan.Web.Extensions;

namespace OpenXstan.Web.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        private IXposureHelper _helper = null;
        private IProductGateway _gateway = null;
        private IXposureCache _cache = null;
        private ProductController _productController = null;
        private List<Product> _products = null;
        private XposureUser _user = null;
        private string _productId = "001";
        /// <summary>
        /// Initialises each test run
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _helper = MockRepository.GenerateMock<IXposureHelper>();
            _gateway = MockRepository.GenerateMock<IProductGateway>();
            _cache = MockRepository.GenerateMock<IXposureCache>();
            _user = new XposureUser()
                                        { 
                                            ConnectionString  = "connection string", 
                                            Id = 1, 
                                            UserName = "mkumar", 
                                            Password = "shibi143"
                                        };
            _products = new List<Product>() { new Product() { 
                                                                Id = "001",
                                                                Title = "Washing Machine",
                                                                Description = "This is home appliance product",
                                                                CurrentPrice = 459.99M,
                                                                StartTime = DateTime.Now.AddDays(1),
                                                                EndTime = DateTime.Now.AddMonths(1),
                                                                IsEdit = false
                                                             },
                                               new Product() { 
                                                                Id = "002",
                                                                Title = "Samsung AC",
                                                                Description = "This is home appliance product",
                                                                CurrentPrice = 709.59M,
                                                                StartTime = DateTime.Now.AddDays(3),
                                                                EndTime = DateTime.Now.AddMonths(2),
                                                                IsEdit = false
                                                               }
                                                };
            _helper.Stub(x => x.GetUserName()).Return("mkumar").Repeat.Any();
            _helper.Stub(x => x.GetXposureUser()).Return(_user).Repeat.Any();
            _helper.Stub(x => x.GetConnectionString()).Return("my connection string").Repeat.Any();
            _productController = new ProductController(_helper, _cache, _gateway);
        }

        [TestMethod]
        public void TestIndex()
        {
            _gateway.Stub(x => x.GetProduct(_user, string.Empty)).Return(_products).Repeat.Once();

            var product = new ProductController(_helper, _cache, _gateway);
            Assert.IsNotNull(product);
            var indexResult = product.Index() as ViewResult;
            Assert.IsNotNull(indexResult);
            Assert.IsNotNull(indexResult.Model);

            _gateway.VerifyAllExpectations();
            _helper.VerifyAllExpectations();
        }

        [TestMethod]
        public void TestDetailForViewResult()
        {
            _gateway.Stub(x => x.GetProduct(_user, _productId)).Return(_products).Repeat.Any();
            HttpContextBase mockHttpContext = MockRepository.GenerateMock<HttpContextBase>();
            HttpRequestBase mockRequest = MockRepository.GenerateMock<HttpRequestBase>();
            mockHttpContext.Stub(x => x.Request).Return(mockRequest).Repeat.Any();

            var product = new ProductController(_helper, null, _gateway);
            product.ControllerContext = new ControllerContext(mockHttpContext, new RouteData(), product);
            Assert.IsNotNull(product);
            var indexResult = product.Detail(_productId) as ViewResult;
            Assert.IsNotNull(indexResult);
            Assert.IsNotNull(indexResult.Model);
            Assert.AreEqual(_products[0], indexResult.Model);
            _gateway.VerifyAllExpectations();
            _helper.VerifyAllExpectations();
        }

        [TestMethod]
        public void TestDetailForJsonResult()
        {
            _gateway.Stub(x => x.GetProduct(_user, _productId)).Return(_products).Repeat.Any();
            var mockHttpContext = MockRepository.GenerateMock<HttpContextBase>();
            var mockRequest = MockRepository.GenerateMock<HttpRequestBase>();
            mockRequest.Stub(x => x["X-Requested-With"]).Return("XMLHttpRequest").Repeat.Any();
            mockRequest.Stub(x => x.ContentType).Return("application/json").Repeat.Any();
            mockHttpContext.Stub(x => x.Request).Return(mockRequest).Repeat.Any();

            var product = new ProductController(_helper, null, _gateway);
            product.ControllerContext = new ControllerContext(mockHttpContext, new RouteData(), product);
            Assert.IsNotNull(product);
            var indexResult = product.Detail(_productId) as JsonResult;
            Assert.IsNotNull(indexResult);
            Assert.IsNotNull(indexResult.Data);
            Assert.AreEqual(_products[0], indexResult.Data);
            _gateway.VerifyAllExpectations();
            _helper.VerifyAllExpectations();
        }

        [TestMethod]
        public void TestDetailForPartialResult()
        {
            _gateway.Stub(x => x.GetProduct(_user, _productId)).Return(_products).Repeat.Any();
            HttpContextBase mockHttpContext = MockRepository.GenerateMock<HttpContextBase>();
            HttpRequestBase mockRequest = MockRepository.GenerateMock<HttpRequestBase>();
            mockRequest.Stub(x => x["X-Requested-With"]).Return("XMLHttpRequest").Repeat.Any();
            mockRequest.Stub(x => x.ContentType).Return(string.Empty).Repeat.Any();
            mockHttpContext.Stub(x => x.Request).Return(mockRequest).Repeat.Any();

            var product = new ProductController(_helper, null, _gateway);
            product.ControllerContext = new ControllerContext(mockHttpContext, new RouteData(), product);
            Assert.IsNotNull(product);
            var indexResult = product.Detail(_productId) as PartialViewResult;
            Assert.IsNotNull(indexResult);
            Assert.IsNotNull(indexResult.Model);
            Assert.AreEqual(_products[0], indexResult.Model);
            _gateway.VerifyAllExpectations();
            _helper.VerifyAllExpectations();
        }

        [TestMethod]
        public void TestDetailForContentResult()
        {
            _gateway.Stub(x => x.GetProduct(_user, _productId)).Return(new List<Product>()).Repeat.Any();
            var product = new ProductController(_helper, null, _gateway);

            Assert.IsNotNull(product);
            var indexResult = product.Detail(_productId) as ContentResult;
            Assert.IsNotNull(indexResult);
            Assert.AreEqual("The product details are not found.", indexResult.Content);
            _gateway.VerifyAllExpectations();
            _helper.VerifyAllExpectations();
        }

        [TestMethod]
        public void TestSaveHttpGet()
        {
            var category = new List<Category>() 
                                            {
                                                new Category(){
                                                    Id = 1,
                                                    Name = "iPhone"
                                                },
                                                new Category(){
                                                    Id = 2,
                                                    Name = "iPod"
                                                },
                                                new Category(){
                                                    Id = 3,
                                                    Name = "iPad"
                                                }
                                            };
            _cache.Stub(x => x.GetCategory(_user)).Return(category).Repeat.Any();
            var product = new ProductController(_helper, _cache, _gateway);

            Assert.IsNotNull(product);
            var saveResult = product.Save() as ViewResult;
            Assert.IsNotNull(saveResult);
            Assert.IsNotNull(saveResult.Model);
            Assert.IsNotNull(((Product)saveResult.Model).Category);
            Assert.IsTrue(((Product)saveResult.Model).Category.Count > 0);
            _gateway.VerifyAllExpectations();
            _helper.VerifyAllExpectations();
        }

        [TestMethod]
        public void TestSave()
        {
            _gateway.Stub(x => x.SaveProduct(_user, _products[0])).Return(true).Repeat.Any();
            var product = new ProductController(_helper, _cache, _gateway);

            Assert.IsNotNull(product);
            var saveResult = product.Save(_products[0]) as ViewResult;
            Assert.IsNotNull(saveResult);
            _gateway.VerifyAllExpectations();
            _helper.VerifyAllExpectations();
        }
    }
}
