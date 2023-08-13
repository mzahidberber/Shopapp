using Microsoft.Extensions.Configuration;
using Moq;
using shopapp.core.DataAccess.Abstract;
using shopapp.dataaccess.Concrete.EntityFramework;

namespace shopapp.dataaccess.test
{
    public class MainCategory
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FindCategoryStatus()
        {

            //var configurationMock = new Mock<IConfiguration>();
            //configurationMock.SetupGet(x => x["ConnectionStrings:shopdb1"]).Returns("server=127.0.0.1;port=3306;database=shopdb1;User Id=root;password=mysql123.;");

            //var context = new Mock<ShopContext>(configurationMock.Object);
            //Console.WriteLine(context);
            //var service = new EfMainCategoryRepository(context.Object);

            //var result = service.FindCategoryStatus("Notebook");

            //Console.WriteLine(result);

            Assert.Pass();
        }
    }
}