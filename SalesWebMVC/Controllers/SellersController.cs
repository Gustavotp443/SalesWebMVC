using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService)    //Injeção de depências do serviço
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);      //Action result contento esses dados que estão indo para view
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };  //populando o model da view com os departments
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]                  //Antiataque CSRF
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Id not provided"});
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) { return RedirectToAction(nameof(Error), new { message = "Id not found" }); ; }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" }); ;
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) { return RedirectToAction(nameof(Error), new { message = "Id not found" }); ; }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" }); ;
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) { return RedirectToAction(nameof(Error), new { message = "Id not found" }); ; }

            List<Departments> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid) {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller=seller, Departments = departments };
                return View(viewModel);
            }
            if (id != seller.Id) return RedirectToAction(nameof(Error), new { message = "Id mismatch" }); ;

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            } catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message }); ;
            }
          

        }

        public IActionResult Error(string message)      //Não precisa ser Async pois n da acesso a dados
        {
                //Macete do framework para pegar o id interno da requisição
            var viewModel = new ErrorViewModel { Message = message , RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier};
            return View(viewModel);
        }
    }
}
