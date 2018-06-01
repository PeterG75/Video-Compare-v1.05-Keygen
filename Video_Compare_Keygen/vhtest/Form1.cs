using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace vhtest
{
    public enum LicenseType
    {
        // Token: 0x040002D2 RID: 722
        ProEdition = 20,
        // Token: 0x040002D3 RID: 723
        ExpertEdition = 15,
        // Token: 0x040002D4 RID: 724
        HomeEdition = 10,
        // Token: 0x040002D5 RID: 725
        Unvalid = 0,
        // Token: 0x040002D6 RID: 726
        Free
    }

    public partial class Form1 : Form
    {
        protected int m_nKeyLength = 16;
        protected int m_nCrcBase = 116;
        protected int m_nCrcAdd = 19;
        protected byte[] byte_0 = new byte[]
   {
    180,
    189,
    68,
    56,
    50,
    184,
    181,
    185,
    211,
    0,
    234,
    231,
    54,
    134,
    23,
    202,
    40,
    165,
    3,
    104,
    120,
    185,
    88,
    118,
    136,
    93,
    120,
    127,
    117,
    135,
    249,
    231,
    195,
    11,
    221,
    201,
    47,
    83,
    125,
    80,
    161,
    88,
    18,
    122,
    110,
    149,
    2,
    154,
    223,
    191,
    51,
    201,
    119,
    129,
    190,
    57,
    120,
    90,
    168,
    219,
    208,
    104,
    137,
    88
   };
        public Form1()
        {
            InitializeComponent();
        }
        protected bool ConvKey(string strKey, ref byte[] data)
        {
            if (strKey == null)
            {
                return false;
            }
            bool result;
            try
            {
                strKey = strKey.Replace("-", "");
                strKey = strKey.Replace(" ", "");
                if (strKey.Length < 4)
                {
                    return false;
                }
                data = new byte[Math.Min(strKey.Length, this.m_nKeyLength) / 2 - 1];
                uint num = 0u;
                uint num2 = (uint)this.m_nCrcBase;
                for (int i = 0; i <= data.Length; i++)
                {
                    int num3 = Convert.ToInt32(strKey.Substring(i * 2, 2), 16);
                    byte[] array = data;
                    if (i < array.Length)
                    {
                        array[i] = (byte)num3;
                        num2 = (uint)(this.m_nCrcAdd + (num3 ^ (int)num2));
                    }
                    else
                    {
                        num = (uint)num3;
                    }
                }
                num2 &= 255u;
                if (num != num2)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception (traceline {0})\t{1}\n{2}", new object[]
                {
                    294,
                    ex.Message,
                    ex.StackTrace
                });
                result = false;
            }
            return result;
        }
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
        internal static string DFX_dividia(double dimensione, string stringa, string Divisore = "-")
        {
            return String.Join(Divisore, Enumerable.Range(0, (int)Math.Ceiling(stringa.Length / dimensione))
               .Select(i => new string(stringa
                   .Skip(i * (int)dimensione)
                   .Take((int)dimensione)
                   .ToArray())));
        }
        private void Generate()
        {
            byte lic = 0x14;
            if (re.Checked)
                lic = 0xF;
            else if (rh.Checked)
                lic = 0xA;
            HashAlgorithm hashAlgorithm = MD5.Create();
            byte[] xor = { 0xEA, 0xE7, 0x36, 0x86, 0x17, 0xCA, 0x28 };
            uint nXorLicense = (uint)new Random().Next();
            LicenseType licenseType = (LicenseType)nXorLicense;
            uint nLimiteDate = 0u;
            string strComputerKey = textBox1.Text;
            byte[] dfox = StringToByteArray(strComputerKey.Replace("-",""));
            byte[] dfox2 = new byte[dfox.Length - 1 + 5];

            dfox2[0] = 86;
            dfox2[1] = 67;
            Array.Copy(dfox, 0, dfox2, 2, dfox.Length - 1);
            dfox2[9] = lic;
            dfox2[10] = 0xff;
            dfox2[11] = 0x00;
            byte[] dfox3 = hashAlgorithm.ComputeHash(dfox2);
            byte[] dfox4 = new byte[dfox.Length];
            dfox4[0] = lic;
            dfox4[1] = 0xff;
            dfox4[2] = 0x00;
            Array.Copy(dfox3, 0, dfox4, 3, 4);

            int dnum2 = 0;
            do
            {
                dfox4[dnum2] ^= xor[dnum2];
                dnum2++;
            }
            while (dnum2 < dfox4.Length-1);
            string attiv = ByteArrayToString(dfox4).Substring(0, 14);

            uint ddnum2 = (uint)this.m_nCrcBase;
            for (int i = 0; i <= dfox4.Length - 2; i++)
            {
                int num3 = Convert.ToInt32(attiv.Substring(i * 2, 2), 16);
                ddnum2 = (uint)(this.m_nCrcAdd + (num3 ^ (int)ddnum2));
            }
            ddnum2 &= 255u;
            attiv = (attiv + ddnum2.ToString("X2")).ToUpper();
            textBox3.Text = DFX_dividia(4, attiv);

            //Test
            string strLicenseKey = textBox3.Text;
            byte[] array = null;
            byte[] array2 = null;
            if (!this.ConvKey(strComputerKey, ref array) || !this.ConvKey(strLicenseKey, ref array2))
            {
                MessageBox.Show("Errore");
                return;
            }
            int num2 = 0;
            do
            {
                array2[num2] ^= xor[num2];
                num2++;
            }
            while (num2 < array2.Length);
            
            byte[] array3 = new byte[array.Length + 5];
            array3[0] = 86;
            array3[1] = 67;
            Array.Copy(array, 0, array3, 2, array.Length);
            Array.Copy(array2, 0, array3, array.Length + 2, 3);
            byte[] array4 = hashAlgorithm.ComputeHash(array3);
            int num5 = 0;
            int num6 = array2.Length - 3;
            if (0 < num6)
            {
                while (array4[num5] == array2[num5 + 3])
                {
                    num5++;
                    if (num5 >= num6)
                    {
                        goto IL_16F;
                    }
                }
                MessageBox.Show("Errore");
                return;
            }
            IL_16F:
            uint num7 = (uint)array2[0];
            if (num7 != 10u && num7 != 15u && num7 != 20u)
            {
                MessageBox.Show("Errore");
                return;
            }
            licenseType = (LicenseType)(num7 ^ nXorLicense);
            uint num8 = (uint)((int)array2[2] << 16 | (int)array2[1]);
            nLimiteDate = num8;
            //MessageBox.Show((num8 <= 1200u).ToString());
            return;
        }
        
        protected void FormatKey(byte[] data, ref string strKey)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int num = Math.Min(data.Length, this.m_nKeyLength / 2 - 1);
            uint num2 = (uint)this.m_nCrcBase;
            int num3 = 0;
            if (0 < num)
            {
                int nCrcAdd = this.m_nCrcAdd;
                do
                {
                    num2 = ((uint)data[num3] ^ num2) + (uint)nCrcAdd;
                    num3++;
                }
                while (num3 < num);
            }
            num2 &= 255u;
            int num4 = 0;
            if (0 < num)
            {
                do
                {
                    if ((num4 & 1) == 0 && num4 > 0)
                    {
                        stringBuilder.Append("-");
                    }
                    byte b = data[num4];
                    stringBuilder.Append(b.ToString("X2"));
                    num4++;
                }
                while (num4 < num);
            }
            uint num5 = num2;
            stringBuilder.Append(num5.ToString("X2"));
            strKey = stringBuilder.ToString();
        }
        public bool GetComputerKey(ref string strComputerKey)
        {
            bool result;
            try
            {
                string str = null;
                ulong num = 0UL;
                if (!this.GetSystemDiskSerialNumber(ref str, ref num))
                {
                    return false;
                }
                ulong num2 = num;
                string s = str + num2.ToString();
                byte[] data = MD5.Create().ComputeHash(Encoding.Default.GetBytes(s));
                this.FormatKey(data, ref strComputerKey);
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception (traceline {0})\t{1}\n{2}", new object[]
                {
                    245,
                    ex.Message,
                    ex.StackTrace
                });
                result = false;
            }
            return result;
        }
       
        protected bool GetSystemDiskSerialNumber(ref string string_0, ref ulong ulong_0)
        {
            string_0 = "NoSN";
            ulong_0 = 1UL;
            try
            {
                string str = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 2);
                ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DeviceID='" + str + "'").Get().GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        ManagementBaseObject managementBaseObject = enumerator.Current;
                        ManagementObjectCollection.ManagementObjectEnumerator enumerator2 = ((ManagementObject)managementBaseObject).GetRelated("Win32_DiskPartition").GetEnumerator();
                        try
                        {
                            while (enumerator2.MoveNext())
                            {
                                ManagementBaseObject managementBaseObject2 = enumerator2.Current;
                                ManagementObjectCollection.ManagementObjectEnumerator enumerator3 = ((ManagementObject)managementBaseObject2).GetRelated("Win32_DiskDrive").GetEnumerator();
                                try
                                {
                                    while (enumerator3.MoveNext())
                                    {
                                        ManagementBaseObject managementBaseObject3 = enumerator3.Current;
                                        ManagementObject managementObject = (ManagementObject)managementBaseObject3;
                                        try
                                        {
                                            ulong_0 = (ulong)managementObject["Size"];
                                            object obj = null;
                                            try
                                            {
                                                if (obj == null)
                                                {
                                                    for (; ; )
                                                    {
                                                        IL_101:
                                                        int num = 437563971;
                                                        for (; ; )
                                                        {
                                                            switch (num ^ 437563970)
                                                            {
                                                                case 1:
                                                                    obj = managementObject["SerialNumber"];
                                                                    num = 437563970;
                                                                    continue;
                                                                case 2:
                                                                    goto IL_101;
                                                            }
                                                            goto Block_20;
                                                        }
                                                    }
                                                    Block_20:;
                                                }
                                            }
                                            catch (Exception)
                                            {
                                            }
                                            try
                                            {
                                                if (obj == null)
                                                {
                                                    obj = managementObject["Signature"];
                                                }
                                            }
                                            catch (Exception)
                                            {
                                            }
                                            if (obj != null)
                                            {
                                                for (; ; )
                                                {
                                                    IL_15A:
                                                    int num2 = 437563969;
                                                    for (; ; )
                                                    {
                                                        switch (num2 ^ 437563970)
                                                        {
                                                            case 0:
                                                                goto IL_15A;
                                                            case 1:
                                                                goto IL_161;
                                                            case 3:
                                                                string_0 = obj.ToString();
                                                                num2 = 437563971;
                                                                continue;
                                                        }
                                                        goto Block_17;
                                                    }
                                                }
                                                Block_17:
                                                continue;
                                                IL_161:
                                                return true;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                }
                                finally
                                {
                                    if (enumerator3 != null)
                                    {
                                        for (; ; )
                                        {
                                            IL_1A4:
                                            int num3 = 437563971;
                                            for (; ; )
                                            {
                                                switch (num3 ^ 437563970)
                                                {
                                                    case 0:
                                                        goto IL_1A4;
                                                    case 1:
                                                        ((IDisposable)enumerator3).Dispose();
                                                        num3 = 437563968;
                                                        continue;
                                                }
                                                goto Block_27;
                                            }
                                        }
                                        Block_27:;
                                    }
                                }
                            }
                        }
                        finally
                        {
                            if (enumerator2 != null)
                            {
                                for (; ; )
                                {
                                    IL_1E2:
                                    int num4 = 437563968;
                                    for (; ; )
                                    {
                                        switch (num4 ^ 437563970)
                                        {
                                            case 0:
                                                goto IL_1E2;
                                            case 2:
                                                ((IDisposable)enumerator2).Dispose();
                                                num4 = 437563971;
                                                continue;
                                        }
                                        goto Block_30;
                                    }
                                }
                                Block_30:;
                            }
                        }
                    }
                }
                finally
                {
                    if (enumerator != null)
                    {
                        for (; ; )
                        {
                            IL_220:
                            int num5 = 437563971;
                            for (; ; )
                            {
                                switch (num5 ^ 437563970)
                                {
                                    case 0:
                                        goto IL_220;
                                    case 1:
                                        ((IDisposable)enumerator).Dispose();
                                        num5 = 437563968;
                                        continue;
                                }
                                goto Block_33;
                            }
                        }
                        Block_33:;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string strComputerKey = "";
            GetComputerKey(ref strComputerKey);
            textBox1.Text = strComputerKey;
            Generate();
        }

        private void rh_CheckedChanged(object sender, EventArgs e)
        {
            Generate();
        }
    }
}
