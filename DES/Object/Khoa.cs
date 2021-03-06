﻿using DES.Function;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DES.Object
{
    class Khoa
    {
        public string KhoaChinh; //khoá chính 64bit
        public string[] KhoaPhu { get; private set; }//mảng 16 khoá con
        public bool KiemTraKhoa()// kiểm tra khoá có đúng 64bit k
        {
            return (KhoaChinh.Length % 64 == 0);
        }
        public Khoa(string khoa)
        {
            KhoaChinh = Method.Hex2Binary(khoa);
        }
        private static readonly int[] keyp = { 57, 49, 41, 33, 25, 17, 9,
                                             1, 58, 50, 42, 34, 26, 18,
                                             10, 2, 59, 51, 43, 35, 27,
                                             19, 11, 3, 60, 52, 44, 36,
                                             63, 55, 47, 39, 31, 23, 15,
                                             7, 62, 54, 46, 38, 30, 22,
                                             14, 6, 61, 53, 45, 37, 29,
                                             21, 13, 5, 28, 20, 12, 4 };

        private static readonly int[] shift_table = new int[] { 1, 1, 2, 2,
                                                                2, 2, 2, 2,
                                                                1, 2, 2, 2,
                                                                2, 2, 2, 1 };

        private static readonly int[] key_comp = { 14, 17, 11, 24, 1, 5,
                                                     3, 28, 15, 6, 21, 10,
                                                     23, 19, 12, 4, 26, 8,
                                                     16, 7, 27, 20, 13, 2,
                                                     41, 52, 31, 37, 47, 55,
                                                     30, 40, 51, 45, 33, 48,
                                                     44, 49, 39, 56, 34, 53,
                                                     46, 42, 50, 36, 29, 32 };

        public void SinhKhoaCon()
        {
            KhoaPhu = new string[16];// mảng khoá phụ cho 16 round
            string KeySauPC1 = Method.Permute(KhoaChinh, keyp);// đưa qua hộp hoán vị nén Pbox
            
            //chia thành 2 nửa trái phải
            string left = KeySauPC1.Substring(0,28);
            string right = KeySauPC1.Substring(28, 28);
        
            for(int i=0;i<16;i++)
            {
                //dịch trái theo bảng dịch
                left = Method.shift_left(left, shift_table[i]);
                right = Method.shift_left(right, shift_table[i]);

                string combine = left + right;//Gộp lại

                KhoaPhu[i] = Method.Permute(combine, key_comp);//Đưa qua hộp nén lần 2 rồi lưu vào mảng
            }    
        }
    }
}
