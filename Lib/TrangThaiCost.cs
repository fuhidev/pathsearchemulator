using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
    public class TrangThaiCost : TrangThai
    {
        public int[,] chiPhi { get; set; }

        public void DoiChiPhi(Point p, int chiPhi)
        {
            this.chiPhi[p.X, p.Y] = chiPhi;
        }
        public object Clone()
        {
            return new TrangThaiCost
            {
                chiPhi = this.chiPhi.Clone() as int[,],
                trangThai = this.trangThai.Clone() as int[,]
            };
        }
    }
}
