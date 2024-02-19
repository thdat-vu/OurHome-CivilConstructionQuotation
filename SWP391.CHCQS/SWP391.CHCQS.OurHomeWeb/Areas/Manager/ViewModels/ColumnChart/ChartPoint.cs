namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels.ColumnChart
{
    //đây là class nhận vào danh sách đối tượng class chứa nội dung của cột được hiển thị 
    public class ChartPoint<T>
    {
        public List<T>? _chart;
        public ChartPoint()
        {
            _chart = new List<T>() { };
        }
        //hàm gán dữ liệu cho list
        public void setColumnChart(List<T> data)
        {
            _chart = data;
        }
    }
}
