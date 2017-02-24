using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
    public interface ISearching
    {
        void Run();

        void SetView(IView view);
        int[,] Matrix { get; set; }
        Point DiemBatDau { get; set; }
        Point DiemKetThuc { get; set; }
        int KichThuoc { get; set; }
    }
}
