
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Utility;

namespace SWP391.CHCQS.Services
{
    public  class AppState
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private static AppState _instance;
        public int SLIndex { get; set; }
        public int ENIndex { get; set; }
        public int MGIndex { get; set; }

        public int SLMax { get; set; }
        public int ENMax { get; set; }
        public int MGMax { get; set; }

        private readonly Random _random = new ();

        private AppState(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
            SLMax = _unitOfWork.Staff.GetAllWithFilter((x) => x.Id.Contains(SD.SellerIdKey)).Count();
            ENMax = _unitOfWork.Staff.GetAllWithFilter((x) => x.Id.Contains(SD.EngineertIdKey)).Count();
            MGMax = _unitOfWork.Staff.GetAllWithFilter((x) => x.Id.Contains(SD.ManagerIdKey)).Count();

            SLIndex =  _random.Next(1, SLMax);
            ENIndex = _random.Next(1, ENMax);
            MGIndex = _random.Next(1, MGMax);

        }

        private static readonly object _lock = new object();
        public static AppState Instance(IUnitOfWork unitOfWork)
        {
            //nếu _instance chưa dc tạo thì tạo ra - Singleton
            if(_instance == null)
            {
                //chặn thread khác đi vào - only one - thằng còn lại đợi
                lock(_lock)
                {
                    //check phát nữa cho chắc ko nó ko có là toang
                    if(_instance == null)
                    {
                        _instance = new AppState(unitOfWork);
                    }
                }
                _instance = new AppState(unitOfWork);
            }//nếu có rồi thì tiến hành kiểm tra số lượng nhân viên còn trung khớp hay ko
            else
            {
                lock (_lock)
                {
                    if (_instance.SLMax != unitOfWork.Staff.GetAllWithFilter((x) => x.Id.Contains(SD.SellerIdKey)).Count())
                    {
                        _instance.SLMax = unitOfWork.Staff.GetAllWithFilter((x) => x.Id.Contains(SD.SellerIdKey)).Count();
                    }
                    if (_instance.ENMax != unitOfWork.Staff.GetAllWithFilter((x) => x.Id.Contains(SD.EngineertIdKey)).Count())
                    {
                        _instance.ENMax = unitOfWork.Staff.GetAllWithFilter((x) => x.Id.Contains(SD.EngineertIdKey)).Count();
                    }
                    if (_instance.MGMax != unitOfWork.Staff.GetAllWithFilter((x) => x.Id.Contains(SD.ManagerIdKey)).Count())
                    {
                        _instance.MGMax = unitOfWork.Staff.GetAllWithFilter((x) => x.Id.Contains(SD.ManagerIdKey)).Count();
                    }
                }

            }
            return _instance;
        }
        //lần lượt sl, en, mg
        public Tuple<int, int, int> GetDelegationIndex()
        {
            //gán giá trị trả về cho Tuple
            var sl = SLIndex;
            var en = ENIndex;
            var mg = MGIndex;

            //thực hiện update lại index
            if (SLIndex == SLMax)
                SLIndex = 1;
            else SLIndex++;

            if (ENIndex == ENMax)
                ENIndex = 1;
            else ENIndex++;

            if (MGIndex == MGMax)
                MGIndex = 1;
            else MGIndex++;

            return Tuple.Create(sl, en, mg);
        }

        //Cách gọi ra AppState: AppState.Instance(_unitOfWork).GetDelegationIndex()
        //var demo = AppState.Instance(_unitOfWork).GetDelegationIndex()
        //demo.Item1
        //demo.Item2
        //demo.Item3

        //để lấy StaffId cho seller  thì query:  _unitOfWork.Staff.Where((x) = >x.Id.Contains(SD.SelerIdKey)).SkipWhile((entity, index) => index < demo.Item1).FirstOrdefault().Id;
    }
}
