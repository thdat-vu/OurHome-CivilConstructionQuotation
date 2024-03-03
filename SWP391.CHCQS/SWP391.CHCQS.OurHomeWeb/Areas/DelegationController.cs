using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Services;
using System.Configuration;

namespace SWP391.CHCQS.OurHomeWeb.Areas
{

    public class DelegationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DelegationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
      
    }
}
