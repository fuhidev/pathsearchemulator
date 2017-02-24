using System;

namespace Lib
{
    public class Controller : IController
    {
        private Point diemBatDau;
        private Point diemKetThuc;

        private ISearching giaiThuat;
        private readonly int kichThuoc = 7;

        private int[,] matrix;
        private IView view;

        public Controller()
        {
            TaoMap();
        }

        public ISearching GiaiThuat
        {
            get { return giaiThuat; }
            set
            {
                giaiThuat = value;
                giaiThuat.SetView(view);
                giaiThuat.Matrix = this.matrix;
                giaiThuat.KichThuoc = kichThuoc;
                giaiThuat.DiemBatDau = diemBatDau;
                giaiThuat.DiemKetThuc = diemKetThuc;
            }
        }

        public int[,] LayMaTran()
        {
            return matrix;
        }


        public int LayKichThuoc()
        {
            return kichThuoc;
        }


        public void SetView(IView view)
        {
            this.view = view;
            if (giaiThuat != null)
                giaiThuat.SetView(view);
        }


        public int LayTrongSo(int x, int y)
        {
            return matrix[x, y];
        }


        public void ThemTuong(Point p)
        {
            if (matrix[p.X, p.Y] == ConstApp.DIEM_KET_THUC || matrix[p.X, p.Y] == ConstApp.DIEM_XUAT_PHAT) return;
            if (matrix[p.X, p.Y] == ConstApp.VAT_CAN)
            {
                matrix[p.X, p.Y] = ConstApp.CHUA_XET;
            }
            else
                matrix[p.X, p.Y] = ConstApp.VAT_CAN;
        }

        public void Run()
        {
            giaiThuat.Run();
            view.ThongBaoChayXongGiaiThuat();
        }

        private void TaoMap(bool flag = false)
        {
            matrix = new int[kichThuoc, kichThuoc];
            var rd = new Random();
            for (var i = 0; i < kichThuoc; i++)
            {
                for (var j = 0; j < kichThuoc; j++)
                {
                    var trongSo = 1;
                    if (flag)
                    {
                        do
                            trongSo = rd.Next(10);
                        while (trongSo == ConstApp.DA_XET || trongSo == ConstApp.DUONG_DI ||
                                                          trongSo == ConstApp.DIEM_KET_THUC ||
                                                          trongSo == ConstApp.DIEM_XUAT_PHAT ||
                                                          trongSo == ConstApp.CHO_XET);
                    }
                    matrix[i, j] = trongSo;
                }
            }
            matrix[0, 0] = ConstApp.DIEM_XUAT_PHAT;
            diemBatDau = new Point(0, 0);
            if (giaiThuat != null)
                giaiThuat.DiemBatDau = diemBatDau;
            matrix[kichThuoc - 1, kichThuoc - 1] = ConstApp.DIEM_KET_THUC;
            diemKetThuc = new Point(kichThuoc - 1, kichThuoc - 1);
            if (giaiThuat != null)
                giaiThuat.DiemKetThuc = diemKetThuc;
        }

        public void ResetMap(bool flag)
        {
            TaoMap(flag);
            giaiThuat.Matrix = this.matrix;
        }

        public void DiemBatDau(Point p)
        {
            if (diemKetThuc.Equals(p)) return;
            matrix[diemBatDau.X, diemBatDau.Y] = 1;
            matrix[p.X, p.Y] = ConstApp.DIEM_XUAT_PHAT;
            diemBatDau = p;
            if (giaiThuat != null)
                giaiThuat.DiemBatDau = diemBatDau;
        }

        public void DiemKetThuc(Point p)
        {
            if (diemBatDau.Equals(p)) return;
            matrix[diemKetThuc.X, diemKetThuc.Y] = 1;
            matrix[p.X, p.Y] = ConstApp.DIEM_KET_THUC;
            diemKetThuc = p;
            if (giaiThuat != null)
            {
                giaiThuat.DiemKetThuc = diemKetThuc;
            }
        }
    }
}