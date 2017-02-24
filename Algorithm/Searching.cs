using Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Algorithm
{
    public abstract class Searching<T> : ISearching where T : Node
    {
        protected IView view;

        protected Point diemBatDau;
        protected Point diemKetThuc;


        protected int[,] matrix;
        public int[,] Matrix
        {
            get { return matrix; }
            set { this.matrix = value; }
        }

        protected int kichThuoc;
        protected T[,] danhSachDiem;
        public abstract void ChayGiaiThuat();

        public void Run()
        {
            this.danhSachDiem = new T[kichThuoc,kichThuoc];
            this.KhoiTaoNode();
            this.DuyetHangXom();
            this.ChayGiaiThuat();
        }


        protected List<Point> LayDuongDi(T node)
        {
            var n = node as Node;
            List<Point> result = new List<Point>();
            var current = n;
            while (current != null)
            {
                Console.WriteLine(current.ViTri);
                result.Add(current.ViTri);
                current = current.nodeCha;
            }
            return result;
        }

        protected abstract void KhoiTaoNode();
        
        protected void DuyetHangXom()
        {
            for (int y = 0; y < kichThuoc; y++)
            {
                for (int x = 0; x < kichThuoc; x++)
                {
                    var node = danhSachDiem[x, y];

                    //di xuong
                    if (y < kichThuoc - 1 && matrix[x, y + 1] != ConstApp.VAT_CAN)
                        node.hangXom.Add(danhSachDiem[x, y + 1]);

                    //sang trai
                    if (x > 0 && matrix[x - 1, y] != ConstApp.VAT_CAN)
                        node.hangXom.Add(danhSachDiem[x - 1, y]);

                   

                    //di len
                    if (y > 0 && matrix[x, y - 1] != ConstApp.VAT_CAN)
                        node.hangXom.Add(danhSachDiem[x, y - 1]);

                    //sang phai
                    if (x < kichThuoc - 1 && matrix[x + 1, y] != ConstApp.VAT_CAN)
                        node.hangXom.Add(danhSachDiem[x + 1, y]);
                   
                  
                  
                }
            } 
        }

        public void SetView(IView view)
        {
            this.view = view;
        }


        Point ISearching.DiemBatDau
        {
            get { return this.diemBatDau; }
            set { this.diemBatDau = value; }
        }

        Point ISearching.DiemKetThuc
        {
            get { return this.diemKetThuc; }
            set { this.diemKetThuc = value; }
        }



        public int KichThuoc
        {
            get { return this.KichThuoc; }
            set { this.kichThuoc = value; }
        }
    }
}
