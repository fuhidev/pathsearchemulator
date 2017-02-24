using Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace Algorithm
{
    public class BreadthFirstSearch : Searching<Node>
    {



        protected Node LayNode(Point p)
        {
            return danhSachDiem[p.X, p.Y];
        }
        public override void ChayGiaiThuat()
        {

            Node ketThuc = danhSachDiem[diemKetThuc.X, diemKetThuc.Y];
            Queue<Node> queue = new Queue<Node>();
            //dua vi tri dau tien la DiemBatDau vao
            var batDau = this.LayNode(diemBatDau);
            batDau.DaXet = true;
            queue.Enqueue(batDau);
            var trangThai = new TrangThai();
            trangThai.trangThai = this.matrix.Clone() as int[,];
            view.ThemTrangThai(trangThai.Clone());
            //gan DiemBatDau la da vieng
            while (queue.Any())
            {
                Node nodeHienTai = queue.Dequeue();

                trangThai.DaXet(nodeHienTai.ViTri);

                //neu nhu tim duoc roi thi dung thuat toan
                if (nodeHienTai.Equals(ketThuc))
                {
                    var path = LayDuongDi(ketThuc);
                    foreach (var point in path)
                    {
                        trangThai.DuongDi(point);

                    }
                    view.ThemTrangThai(trangThai.Clone());
                    break;
                }
                //them danh sach ke vao trong queue
                foreach (var node in nodeHienTai.hangXom)
                {
                    if (!node.DaXet)
                    {
                        //giu node cha lai de lat nua truy vet
                        node.nodeCha = nodeHienTai;

                        //them vao queue
                        queue.Enqueue(node);

                        node.DaXet = true;

                        trangThai.ChoXet(node.ViTri);
                    }
                }
                view.ThemTrangThai(trangThai.Clone());
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
