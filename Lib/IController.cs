using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
    public interface IController
    {
        ISearching GiaiThuat { get; set; }
        void ThemTuong(Point p);
        int[,] LayMaTran();
        int LayTrongSo(int x,int y);
        int LayKichThuoc();
        void Run();
        void SetView(IView view);
        void ResetMap(bool flag);
        void DiemBatDau(Point p );
        void DiemKetThuc(Point p);
    }
}
