using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace RunningText
{
    class CpowerUse
    {
        // main communication setting
        // setting tipe untuk komunikasi jika 0 => RS232, 1 => Network IP
        public byte m_nCommType { set; get; }
        // lama proses harus disetting, tidak ada nilai default
        // minimal 600ms
        public int m_nTimeout { set; get; }
        // card led tujuan, seperti 1,2,3 dan seterusnya
        private int m_nCardID { set; get; }
        // window view
        private int m_nWindowNo { set; get; }

        // for Communication RS232
        // setting variable ini sebelum menggunakan komunikasi
        // RS232, tidak ada nilai default untuk ini.
        private long[] m_baudtbl = new long[6] { 115200, 57600, 38400, 19200, 9600, 4800 };
        public int m_nPort { set; get; }
        public int m_nBaudrate { set; get; }

        // for Communication Network
        // setting variable ini sebelum menggunakan komunikasi
        // via network, tidak ada nilai default untuk ini.
        private uint m_dwIPAddr = 0;
        private uint m_dwIDCode = 0;
        public string IPAddr { set; get; }
        public string IDCode { set; get; }
        public int m_nIPPort { set; get; }

        public CpowerUse() { }

        // Constructor & overload for using network communication
        public CpowerUse(byte CommType, int TimeOut, int CardId,int WindNo, string IP, int port, String IdCode)
        {
            m_nCommType = CommType;
            m_nTimeout = TimeOut;
            m_nCardID = CardId;
            m_nWindowNo = WindNo;
            IPAddr = IP;
            m_nIPPort = port;
            IDCode = IdCode;
        }

        // Constructor & overload for using network communication
        public CpowerUse(byte CommType, int TimeOut, int CardId,int WindNo, int BaudRate, int Port)
        {
            m_nCommType = 0;
            m_nTimeout = 600;
            m_nCardID = CardId;
            m_nWindowNo = WindNo;
            m_nBaudrate = BaudRate;
            m_nPort = Port;
        }

        // fungsi untuk inisialisasi communication
        public int InitComm()
        {
            int nRet = 0;
            string strPort;
            if (0 == m_nCommType)
            {
                strPort = "COM" + m_nPort;
                nRet = CP5200.CP5200_RS232_InitEx(Marshal.StringToHGlobalAnsi(strPort), m_nBaudrate, m_nTimeout);
            }
            else
            {
                m_dwIPAddr = GetIP(IPAddr);
                if (0 != m_dwIPAddr)
                {
                    m_dwIDCode = GetIP(IDCode);
                    if (0 != m_dwIDCode)
                    {
                        CP5200.CP5200_Net_Init(m_dwIPAddr, m_nIPPort, m_dwIDCode, m_nTimeout);
                        nRet = 1;
                    }
                }

            }

            return nRet;
        }

        /**
         * Fungsi untuk mendapatkan sekaligus
         * untuk konversi alamat IP 
         */
        public uint GetIP(string strIp)
        {
            System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(strIp);
            uint lIp = (uint)ipaddress.Address;
            lIp = ((lIp & 0xFF000000) >> 24) + ((lIp & 0x00FF0000) >> 8) + ((lIp & 0x0000FF00) << 8) + ((lIp & 0x000000FF) << 24);
            return (lIp);
        }

        public int SendStaticText( int width=0, int height=0, string message="")
        {
            int ret = 0;

            if (1 == InitComm())
            {
                if (0 == m_nCommType)
                {
                    ret = CP5200.CP5200_RS232_SendStatic(m_nCardID, 0, Marshal.StringToHGlobalAnsi(message), Color.FromArgb(255, 0, 0).ToArgb(), 16, 0, 0, 0, width, height);
                }
                else
                {
                    ret = CP5200.CP5200_Net_SendStatic(m_nCardID, 0, Marshal.StringToHGlobalAnsi(message), Color.FromArgb(255, 0, 0).ToArgb(), 16, 0, 0, 0, width, height);
                }
            }

            return ret;
        }

        // Create image temporary & send image to device
        public int SendImg(TextImage img, int effect)
        {
            int ret = 0;
            if (1 == InitComm())
            {
                if (0 == m_nCommType)
                {
                    ret = CP5200.CP5200_RS232_SendPicture(m_nCardID, m_nWindowNo, 0, 0, img.Width, img.Height, 
                        Marshal.StringToHGlobalAnsi(img.path), 0, effect, 3, 0);

                } else
                {
                    ret = CP5200.CP5200_Net_SendPicture(m_nCardID, m_nWindowNo, 0, 0, img.Width, img.Height,
                    Marshal.StringToHGlobalAnsi(img.path), 0, effect, 3, 0);
                }

            }
            return ret;
        }

        public int SendPicture(int width = 0, int height = 0, string picture = null)
        {
            int ret = 0;

            if (1 == InitComm())
            {
                if (0 == m_nCommType)
                {
                    ret = CP5200.CP5200_RS232_SendPicture(m_nCardID, 0, 0, 0, width, height, Marshal.StringToHGlobalAnsi(picture), 1, 0, 3, 0);
                }
                else
                {
                    ret = CP5200.CP5200_Net_SendPicture(m_nCardID, 0, 0, 0, width, height, Marshal.StringToHGlobalAnsi(picture), 1, 0, 3, 0);
                }
            }

            return ret;
        }

        public int SplitWindow(int width=0, int height=0)
        {
            int ret = 0;
            int[] nWndRect = new int[4];
            nWndRect[0] = 0;
            nWndRect[1] = 0;
            nWndRect[2] = width;
            nWndRect[3] = height;

            if (1 == InitComm())
            {
                if (0 == m_nCommType)
                {
                    ret = CP5200.CP5200_RS232_SplitScreen(m_nCardID, width, height, 0, nWndRect);
                }
                else
                {
                    ret = CP5200.CP5200_Net_SplitScreen(m_nCardID, width, height, 0, nWndRect);
                }
            }

            return ret;
        }

        /**
         * Fungsi untuk mengirim data ke ledcenterM via COM
         * pastikan COMx sesuai, lihat pada device manager
         * gunakan kabel RS232 to USB 
         */
        public int SendTextToCom(String message="")
        {

            int icolor = 3000;
            int ret = 0;

            if (1 == InitComm())
            {
                try
                {
                    IntPtr iPtr = Marshal.StringToHGlobalAnsi(message);
                    ret = CP5200.CP5200_RS232_SendText(m_nCardID, 0, iPtr, icolor, 16, 3, 0, 3, 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return ret;
        }

        /**
         * Fungsi untuk mengirim data ke ledcenterM via network
         * pastikan IP sesuai, jika belum gunakan untuk membaca 
         * settingan network pada devicenya 
         */
        public int SendTextToNetwork(String message="")
        {
            int ret = 0;
            int icolor = 3000;

            if (1 == InitComm())
            {
                m_dwIPAddr = GetIP(IPAddr);
                if (0 != m_dwIPAddr)
                {
                    m_dwIDCode = GetIP(IDCode);
                    if (0 != m_dwIDCode)
                    {
                        try
                        {
                            IntPtr iPtr = Marshal.StringToHGlobalAnsi(message);
                            ret = CP5200.CP5200_Net_SendText(m_nCardID, 0, iPtr, icolor, 16, 3, 0, 3, 0);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }

            return ret;
        }
    }
}
