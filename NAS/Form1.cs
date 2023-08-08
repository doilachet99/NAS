using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpCifs.Smb;

namespace NAS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            string ipAddress = IPAddressTextBox.Text;
            string sharedFolder = SharedFolderTextBox.Text;
            string port = PortTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            try
            {
                string fullPath = $"smb://{ipAddress}:{port}/{sharedFolder}";

                SmbFile smbFile = new SmbFile(fullPath, new NtlmPasswordAuthentication(ipAddress, username, password));

                if (smbFile.Exists())
                {
                    SmbFile[] files = smbFile.ListFiles();
                    foreach (SmbFile file in files)
                    {
                        ResultListBox.Items.Add(file.GetName());
                    }
                }
                else
                {
                    ResultListBox.Items.Add("Thư mục không tồn tại hoặc không có quyền truy cập.");
                }
            }
            catch (Exception ex)
            {
                ResultListBox.Items.Add("Lỗi: " + ex.Message);
            }





        }

    }
}
