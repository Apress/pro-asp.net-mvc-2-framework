using System.Web.Mvc;
using System.Xml.Linq;

namespace AlternativeViewEngines.Controllers
{
    public class BooksController : Controller
    {
        public ViewResult Index()
        {
            return View(GetBooks());
        }

        private XDocument GetBooks()
        {
            return XDocument.Parse(@"
              <Books>
                 <Book title='How to annoy dolphins' author='B. Swimmer'/>
                 <Book title='How I survived dolphin attack' author='B. Swimmer'/>
              </Books>
            ");
        }
    }
}
