using Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Algorithm.AStar
{
    public class AStarSearch : Searching<Node>
    {
        protected int Heuristic(Node n1, Node n2)
        {
            return Math.Abs(n1.ViTri.X - n2.ViTri.X) + Math.Abs(n1.ViTri.Y - n2.ViTri.Y);
        }
        public override void ChayGiaiThuat()
        {
           
            PriorityQueue queue = new PriorityQueue();
            Node batDau = (Node)this.danhSachDiem[diemBatDau.X, diemBatDau.Y];
            batDau.DaXet = true;
            Node ketThuc = (Node)this.danhSachDiem[diemKetThuc.X, diemKetThuc.Y];
            queue.Enqueue(batDau, 0);
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
                    //neu da xet roi thi bo qua
                    if (n.DaXet)
                        continue;
                    //neu chua thi thuc hien tiep
                    var chiPhi = nodeHienTai.ChiPhi +n.GiaTri;
                    if (n.ChiPhi == 0 || chiPhi < n.ChiPhi)
                    {
                        //gan gia tri cho nodeHienTai
                        n.ChiPhi = chiPhi + this.Heuristic(n, ketThuc);

                        trangThai.DoiChiPhi(n.ViTri, n.ChiPhi);
                        //gan node Cha cua n
                        n.nodeCha = nodeHienTai;

                        n.DaXet = true;

                        queue.Enqueue(n,n.ChiPhi);

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
                    var node = new Node
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
