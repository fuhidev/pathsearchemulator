using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
    public class TrangThai 
    {
        public int[,] trangThai { get; set; }

        public void DaXet(Point p)
        {
            trangThai[p.X, p.Y] = ConstApp.DA_XET;
        }
        public void ChoXet(Point p)
        {
            trangThai[p.X, p.Y] = ConstApp.CHO_XET;
        }public void DuongDi(Point p)
        {
            trangThai[p.X, p.Y] = ConstApp.DUONG_DI;
        }

        public TrangThai Clone()
        {
            int[,] clone = this.trangThai.Clone() as int[,];
            return new TrangThai
            {
                trangThai = clone
            };
        }
    }
}
