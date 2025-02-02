using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Migrations;
using MusicStore.Repository.IRepository;
using MusicStore.ViewModels;

namespace MusicStore.Controllers.Components
{
    public class TechInfoComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public TechInfoComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync(int productTypeId)
        {
            // Retrieve product type and related technical info
            var productType = _unitOfWork.ProductType.Get(p => p.Id == productTypeId,
                include: q => q
                    .Include(p => p.Manufacturer)
                    .Include(p => p.TechInfo));

            if (productType == null)
            {
                return View("Error", $"Product Type with ID {productTypeId} not found.");
            }

            var viewModel = new ProductTypeVM
            {
                ProductType = productType,
                ManufacturerName = productType.Manufacturer?.Name,
                TechInfo = productType.TechInfo
            };
            return View("TechInfoComponent", viewModel);

        }
    }
}
