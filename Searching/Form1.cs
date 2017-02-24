using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Algorithm;
using Algorithm.AStar;
using Algorithm.Dijkstra;
using Algorithm.Greedy;
using Lib;
using Point = Lib.Point;

namespace Searching
{
    public partial class Form1 : Form, IView
    {
        private const int SIZE_BUTTON = 50;

        private readonly IController controller;

        private readonly List<TrangThai> danhSachTrangThai = new List<TrangThai>();

        private bool isTrongSo { get { return checkBox.Checked; } set { checkBox.Checked = value; } }
        private bool dangChay = false;

        private bool DangChay
        {
            get
            {
                return dangChay;
            }
            set
            {
                dangChay = value;
                btnStart.Text = value ? "Dừng" : "Chạy";
                pnTren.Enabled = !value;
                btnXoa.Enabled = !value;
            }
        }

        //private int index = -1;
        private int index { get { return scroll.Value; } set { this.scroll.Value = value; } }

        private Button[,] matrixButtons;

        private Thread startThread;

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DangChay) return;
            var rd = pnTren.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            if (rd.Equals(radioButton1))
            {
                controller.GiaiThuat = new BreadthFirstSearch();
            }
            else if (rd.Equals(radioButton2))
            {
                controller.GiaiThuat = new GreedyBestFirstSearch();
            }
            else if (rd.Equals(radioButton3))
            {
                controller.GiaiThuat = new DijkstraSearch();
            }
            else if (rd.Equals(radioButton4))
            {
                controller.GiaiThuat = new AStarSearch();
            }
        }

        private void scroll_Scroll(object sender, ScrollEventArgs e)
        {
            DuyetTrangThai(scroll.Value);
        }

        #region Giao Tiep Controller

        public void ThemTrangThai(TrangThai trangThai)
        {
            if (trangThai != null)
                danhSachTrangThai.Add(trangThai);
        }


        public void ThongBaoChayXongGiaiThuat()
        {
            scroll.Enabled = true;
            scroll.Maximum = danhSachTrangThai.Count + 8;
            scroll.Minimum = 0;
            DuyetHetTrangThai();
            DangChay = false;
        }

        private void LoadMap()
        {
            LoadMap(controller.LayMaTran());
        }

        #endregion

        #region Giao Dien

        private static class ButtonColor
        {
            public static readonly Color CHUA_XET = Color.DarkSlateGray;
            public static readonly Color DA_XET = Color.DarkGray;
            public static readonly Color CHO_XET = Color.White;

            public static readonly Color VAT_CAN = Color.Green;
            public static readonly Color DIEM_XUAT_PHAT = Color.CadetBlue;
            public static readonly Color DIEM_KET_THUC = Color.DarkMagenta;
            public static readonly Color DUONG_DI = Color.Red;
        }

        private Color LayMau(int i)
        {
            var color = ButtonColor.CHUA_XET;
            switch (i)
            {
                case ConstApp.VAT_CAN:
                    color = ButtonColor.VAT_CAN;
                    break;
                case ConstApp.DIEM_XUAT_PHAT:
                    color = ButtonColor.DIEM_XUAT_PHAT;
                    break;
                case ConstApp.DIEM_KET_THUC:
                    color = ButtonColor.DIEM_KET_THUC;
                    break;
                case ConstApp.CHO_XET:
                    color = ButtonColor.CHO_XET;
                    break;
                case ConstApp.DA_XET:
                    color = ButtonColor.DA_XET;
                    break;
                case ConstApp.DUONG_DI:
                    color = ButtonColor.DUONG_DI;
                    break;
            }
            return color;
        }

        public Form1()
        {
            InitializeComponent();
            controller = new Controller();
            controller.SetView(this);
            controller.GiaiThuat = new BreadthFirstSearch();
            ResizeForm();
            KhoiTaoBanDo();
        }

        private void ResizeForm()
        {
            pnTren.Height = 30;

            var wMain = controller.LayKichThuoc() * (SIZE_BUTTON + 3) + 10;
            var hMain = controller.LayKichThuoc() * (SIZE_BUTTON + 3);

            var w = wMain;
            var h = 39 + hMain + pnTren.Height + pnDuoi.Height;

            Size = new Size(w, h);
        }

        private Button GenerateButton(int loai)
        {
            var btn = new Button();
            var color = LayMau(loai);

            btn.BackColor = color;
            btn.BackgroundImageLayout = ImageLayout.None;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Margin = new Padding(1);
            btn.Name = "button1";
            btn.Size = new Size(SIZE_BUTTON, SIZE_BUTTON);
            btn.TabIndex = 0;
            btn.UseVisualStyleBackColor = false;
            //btn.Click += button_Click;
            //btn.MouseClick += button_Click;
            btn.MouseDown += button_Click;
            return btn;
        }

        private void KhoiTaoBanDo()
        {
            matrixButtons = new Button[controller.LayKichThuoc(), controller.LayKichThuoc()];
            for (var i = 0; i < controller.LayKichThuoc(); i++)
            {
                for (var j = 0; j < controller.LayKichThuoc(); j++)
                {
                    var trongSo = controller.LayTrongSo(i, j);
                    var btn = GenerateButton(trongSo);
                    if (isTrongSo && trongSo != ConstApp.DIEM_XUAT_PHAT && trongSo != ConstApp.DIEM_KET_THUC)
                        btn.Text = trongSo + "";
                    flowLayout.Controls.Add(btn);
                    matrixButtons[i, j] = btn;
                }
            }
        }

        private void ChuyenMau(Button btn, Color color)
        {
            if (btn != null)
                btn.BackColor = color;
        }

        private void LoadMap(int[,] matrix)
        {
            LoadMap(matrix, matrix);
        }
        private void LoadMap(int[,] matrix, int[,] state)
        {
            for (var i = 0; i < controller.LayKichThuoc(); i++)
            {
                for (var j = 0; j < controller.LayKichThuoc(); j++)
                {
                    var btn = matrixButtons[i, j];
                    var trongSo = matrix[i, j];
                    if (isTrongSo && trongSo != ConstApp.DIEM_XUAT_PHAT && trongSo != ConstApp.DIEM_KET_THUC)
                        btn.Text = trongSo + "";
                    else btn.Text = "";
                    ChuyenMau(btn, LayMau(state[i, j]));
                }
            }
        }
        #endregion

        #region Xu Ly Button

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (DangChay) return;
            controller.ResetMap(isTrongSo);
            LoadMap();
            danhSachTrangThai.Clear();
            index = 0;
        }

        private Point FindButtonIndex(Button _btn)
        {
            for (var i = 0; i < controller.LayKichThuoc(); i++)
            {
                for (var j = 0; j < controller.LayKichThuoc(); j++)
                {
                    var btn = matrixButtons[i, j];
                    if (btn.Equals(_btn))
                        return new Point(i, j);
                }
            }
            return null;
        }

        private void button_Click(object sender, MouseEventArgs e)
        {
            if (DangChay) return; //chương trình đang chạy không dc thay đổi trạng thái bản đồ

            var p = FindButtonIndex(sender as Button);
            //neu nhu chuot trai thi them tuong
            if (e.Button == MouseButtons.Left)
            {
                if (p != null && controller != null)
                    controller.ThemTuong(p);
            }
            //neu nhu nhan chuot giua thi them diem bat dau
            else if (e.Button == MouseButtons.Middle)
            {
                if (p != null && controller != null)
                    controller.DiemBatDau(p);
            }
            //neu nhu chuot phai thi them diem ket thuc
            else if (e.Button == MouseButtons.Right)
            {
                if (p != null && controller != null)
                    controller.DiemKetThuc(p);
            }
            LoadMap(controller.LayMaTran());
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!DangChay)
            {
                index = 0;
                danhSachTrangThai.Clear();
                DangChay = true;
                //pnTren.Enabled = false;
                startThread = new Thread(controller.Run);
                startThread.IsBackground = true;
                startThread.Start();
                //btnStart.Text = "Dừng";
                //btnXoa.Enabled = false;
            }
            else
            {
                startThread.Abort();
                DangChay = false;
                //btnStart.Text = "Chạy";
                //btnXoa.Enabled = true;
            }
        }

        #endregion

        #region Duyet Trang Thai

        private void DuyetHetTrangThai()
        {
            while (index < danhSachTrangThai.Count - 1)
            {
                DuyetTrangThai(true);
                scroll.Value = index;
                Thread.Sleep(ConstApp.DELAY);
            }
        }

        private void DuyetTrangThai(bool flag)
        {
            index += flag ? 1 : 0;
            if (index < 0)
                index = 0;
            if (index > danhSachTrangThai.Count - 1)
                index = danhSachTrangThai.Count - 1;
            DuyetTrangThai(index);
        }

        private void DuyetTrangThai(int index)
        {
            if (controller.GiaiThuat is BreadthFirstSearch)
                LoadMap(danhSachTrangThai[index].trangThai);
            if (controller.GiaiThuat is GreedyBestFirstSearch
                || controller.GiaiThuat is DijkstraSearch
                || controller.GiaiThuat is AStarSearch)
            {
                LoadMap(((TrangThaiCost)danhSachTrangThai[index]).chiPhi, danhSachTrangThai[index].trangThai);
            }
        }

        #endregion
    }
}