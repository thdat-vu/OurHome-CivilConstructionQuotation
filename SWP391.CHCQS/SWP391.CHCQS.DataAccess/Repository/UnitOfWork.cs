﻿using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SWP391DBContext _db;
<<<<<<< HEAD
        public IStandardQuotationRepository StandardQuotation {  get; private set; }
=======
        public IComboRepository Combo {  get; private set; }
        public IComboMaterialRepository ComboMaterial { get; private set; }
        public IComboTaskRepository ComboTask { get; private set; }
>>>>>>> Demostration
        public IProjectRepository Project {  get; private set; }
        public IProjectImageRepository ProjectImage { get; private set; }
        public IMaterialDetailRepository MaterialDetail { get; private set; }
        public ICustomQuotaionTaskRepository CustomQuotaionTask { get; private set; }
        public ICustomQuotationRepository CustomQuotation { get; private set; }
        public ITaskRepository Task { get; private set; }
        public IMaterialRepository Material { get; private set; }
        public IConstructDetailRepository ConstructDetail { get; private set; }
		public ITaskCategoryRepository TaskCategory { get; private set; }
        public IRequestRepository RequestForm { get; private set; }
        public IConstructionTypeRepository ConstructionType { get; private set; }
        public IInvestmentTypeRepository InvestmentType { get; private set; }
        public IFoundationTypeRepository FoundationType { get; private set; }
        public IBasementTypeRepository BasementType { get; private set; }
        public IRoofTypeRepository RoofType { get; private set; }
        public IMaterialCategoryRepository MaterialCategory { get; private set; }
        public IRejectedCustomQuotationRepository RejectedCustomQuotation { get; private set; }

<<<<<<< HEAD

		public UnitOfWork(SWP391DBContext db)
=======
        public IWorkingReportRepository WorkingReport { get; private set; }

        public IPricingRepository Pricing { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public UnitOfWork(SWP391DBContext db)
>>>>>>> Demostration
        {
            _db = db;
            ProjectImage = new ProjectImageRepository(_db);
            Project = new ProjectRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);

<<<<<<< HEAD
            StandardQuotation = new StandardQuotationRepository(_db);
            CustomQuotation = new CustomQuotationRepository(_db);
=======
			Combo = new ComboRepository(_db);
            ComboMaterial = new ComboMaterialRepository(_db);
			ComboTask = new ComboTaskRepository(_db);
			CustomQuotation = new CustomQuotationRepository(_db);
>>>>>>> Demostration

            Task = new TaskRepository(_db);
            CustomQuotaionTask = new CustomQuotaionTaskRepository(_db);
            TaskCategory = new TaskCategoryRepository(_db);

            ConstructDetail = new ConstructDetailRepository(_db);
            RequestForm = new RequestRepository(_db);

            ConstructionType = new ConstructionTypeRepository(_db);
            InvestmentType = new InvesmentTypeRepository(_db);
            FoundationType = new FoundationTypeRepository(_db);
            BasementType = new BasementTypeRepository(_db);
            RoofType = new RoofTypeRepository(_db);

            MaterialCategory = new MaterialCategoryRepository(_db);
            MaterialDetail = new MaterialDetailRepository(_db);
            Material = new MaterialRepository(_db);
            RejectedCustomQuotation = new RejectedCustomQuotationRepository(_db);
<<<<<<< HEAD
=======

            
            WorkingReport = new WorkingReportRepository(_db);

            Pricing = new PricingRepository(_db);

>>>>>>> Demostration
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
