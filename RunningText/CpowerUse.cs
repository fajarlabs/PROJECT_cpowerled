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
        private int m_nBaudrate = 0;
        private int m_nTimeout = 0;
        private int m_nCardID = 0;
        private byte m_nCommType = 0;
        private int m_nIPPort = 0;
        private uint m_dwIPAddr = 0;
        private uint m_dwIDCode = 0;
        private long[] m_baudtbl = new long[6] { 115200, 57600, 38400, 19200, 9600, 4800 };


        public CpowerUse()
        {
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

        /**
         * Fungsi untuk mengirim data ke ledcenterM via COM
         * pastikan COMx sesuai, lihat pada device manager
         * gunakan kabel RS232 to USB 
         */
        public void sendTextToCom(String ComPort = "COM3",int cardId=1,int BaudRate = 115200, String message="")
        {
            // Menggunakan Port COM
            String strPort = "COM3";
            try
            {
                m_nTimeout = 600;

                CP5200.CP5200_RS232_InitEx(Marshal.StringToHGlobalAnsi(ComPort), BaudRate, m_nTimeout);

                int icolor = 3000;
                IntPtr iPtr = Marshal.StringToHGlobalAnsi(message);

                int ret = 0;
                ret = CP5200.CP5200_RS232_SendText(cardId, 0, iPtr, icolor, 16, 3, 0, 3, 0);

                if (0 <= ret)
                {
                    MessageBox.Show("Successful");
                }
                else
                {
                    MessageBox.Show("Com Fail");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /**
         * Fungsi untuk mengirim data ke ledcenterM via network
         * pastikan IP sesuai, jika belum gunakan untuk membaca 
         * settingan network pada devicenya 
         */
        public void sendTextToNetwork(String ipDevice = "192.168.0.1", String passCode = "255.255.255.255", String message="")
        {
            // Menggunakan alamat IP
            m_dwIPAddr = GetIP(ipDevice);
            if (0 != m_dwIPAddr)
            {
                m_dwIDCode = GetIP(passCode);
                if (0 != m_dwIDCode)
                {
                    try
                    {
                        int icolor = 3000;
                        m_nTimeout = 600;

                        IntPtr iPtr = Marshal.StringToHGlobalAnsi(message);
                        m_nCardID = 1;
                        m_nIPPort = 5200;

                        CP5200.CP5200_Net_Init(m_dwIPAddr, m_nIPPort, m_dwIDCode, m_nTimeout);

                        int ret = 0;
                        ret = CP5200.CP5200_Net_SendText(m_nCardID, 0, iPtr, icolor, 16, 3, 0, 3, 0);

                        if (0 <= ret)
                        {
                            MessageBox.Show("Successful");
                        }
                        else
                        {
                            MessageBox.Show("Network Fail");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
