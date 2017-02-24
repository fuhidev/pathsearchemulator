using Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Algorithm.Dijkstra
{
    public class DijkstraSearch : Searching<Node>
    {
        public override void ChayGiaiThuat()
        {
           
            PriorityQueue queue = new PriorityQueue();
            Node batDau = (Node)this.danhSachDiem[diemBatDau.X, diemBatDau.Y];
            batDau.DaXet = true;
            Node ketThuc = (Node)this.danhSachDiem[diemKetThuc.X, diemKetThuc.Y];
            queue.Enqueue(batDau,0);
            var trangThai = new TrangThaiCost();
            trangThai.trangThai = this.matrix.Clone() as int[,];
            trangThai.chiPhi = this.matrix.Clone() as int[,];
            view.ThemTrangThai((TrangThai)trangThai.Clone());
            while (queue.Any())
            {
                var nodeHienTai = (Node)queue.Dequeue();
                trangThai.DaXet(nodeHienTai.ViTri);
                //kiem tra chien thang hay chua
                if (nodeHienTai.Equals(ketThuc))
                {
                    //neu chien thang thi hien thi duong di
                    if (view != null)
                    {
                        var path = LayDuongDi(ketThuc);
                        foreach (var point in path)
                        {
                            trangThai.DuongDi(point);

                        }
                        view.ThemTrangThai((TrangThai)trangThai.Clone());
                    }
                    break;
                }
                //kiem tra hang xom cua current
                foreach (Node n in nodeHienTai.hangXom)
                {
                    if (n.DaXet) continue;
                    var chiPhi = nodeHienTai.ChiPhi + n.GiaTri;
                    if (n.ChiPhi == 0 || chiPhi < n.ChiPhi)
                    {
                        n.nodeCha = nodeHienTai;
                        n.ChiPhi = chiPhi;
                        trangThai.DoiChiPhi(n.ViTri, n.ChiPhi);
                        queue.Enqueue(n,n.ChiPhi);
                        n.DaXet = true;
                        trangThai.ChoXet(n.ViTri);
                    }
                }
                view.ThemTrangThai((TrangThai)trangThai.Clone());
            }
        }

        protected override void KhoiTaoNode()
        {
            for (int y = 0; y < kichThuoc; y++)
            {
                for (int x = 0; x < kichThuoc; x++)
                {
                    var node = new Node()
                    {
                        ViTri = new Point(x, y),
                        DaXet = false,
                        GiaTri = matrix[x, y]
                    };
                    danhSachDiem[x, y] = node;
                }
            }
        }
    }
}
