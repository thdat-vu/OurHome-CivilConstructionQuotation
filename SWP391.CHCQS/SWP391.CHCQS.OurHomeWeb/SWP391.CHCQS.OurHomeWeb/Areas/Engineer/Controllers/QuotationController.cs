using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.Models;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.Controllers
{
    [Area("Engineer")]
    public class QuotationController : Controller
    {
        /// <summary>
        /// This function return the Index of QuotationPage
        /// </summary>
        /// <returns>A view Index</returns>
        public async Task<IActionResult> Index()
        {
            return View();
        }

        /// <summary>
        /// This function return a form to create new Quotation
        /// </summary>
        /// <returns>A view create quotation form</returns>
        public async Task<IActionResult> Create()
        {
            return View();
        }

        /// <summary>
        /// This function take the quotation object from create form and create new quotation.
        /// </summary>
        /// <param name="quotation">The object Quotation to create new Quotation</param>
        /// <returns>Result of the process (Success or Fail)</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Quotation quotation)
        {
            return RedirectToAction("Index", "Quotation");
        }

        /// <summary>
        /// This function return the view detail of the quotation be selected
        /// </summary>
        /// <param name="id">Id of the quotation that be selected</param>
        /// <returns>Return view detail quotation</returns>
        public async Task<IActionResult> Detail(string id)
        {
            return View();
        }

        /// <summary>
        /// This function return a form to edit exist quotation
        /// </summary>
        /// <param name="id">Id of the quotation that be selected</param>
        /// <returns>Return a form with detail of the quotation to edit</returns>
        public async Task<IActionResult> Edit(string id)
        {
            return View();
        }

        /// <summary>
        /// This function take the quotaion info to update to the database
        /// </summary>
        /// <param name="quotation">Quotation object to update</param>
        /// <returns>Result of the process (Success or Fail)</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Quotation quotation)
        {
            return View("Index");
        }

        /// <summary>
        /// This function return the view detail of quotation be selected to delete
        /// </summary>
        /// <param name="Id">Id of the quotation be selected</param>
        /// <returns>A view quotation detail</returns>
        public async Task<IActionResult> Delete(string? Id)
        {
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(string? Id)
        {
            return RedirectToAction("Index");
        }
    }
}
