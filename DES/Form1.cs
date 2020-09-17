using DES.Function;
using DES.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DES
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxKEY.Text = "0123456789ABCDE1";//khoá kiểu hex 64bit
        }

        int MaHoaOrGiaMa = 1;//chế độ(=1:mã hoá, =-1:giải mã)
        Khoa key;
        Des des;
        void MaHoa()
        {
            key = new Khoa(textBoxKEY.Text);
            if (key.KiemTraKhoa())
            {
                string text = textBoxText.Text;
                des = new Des();
                string cipher = des.MaHoa(text, key, chose: MaHoaOrGiaMa);
                textBoxReturn.Text = cipher;
            }
            else
            {
                MessageBox.Show("Key không hợp lệ!");
            }    
        }

        private void btnDecrypt_Click(object sender, EventArgs e)//giải mã
        {
            MaHoaOrGiaMa = -1;
            MaHoa();
        }

        private void btnCrypt_Click(object sender, EventArgs e)//mã hoá
        {
            MaHoaOrGiaMa = 1;
            MaHoa();
        }

    }
}
