using System.Web.Mvc;

namespace GadgetHub.WebUI.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Checkout(string returnUrl)
        {
            return RedirectToAction("Checkout", "Cart", new { returnUrl });
        }
    }
}
