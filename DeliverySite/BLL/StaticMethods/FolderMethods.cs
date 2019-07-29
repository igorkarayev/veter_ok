using Delivery.BLL.Helpers;
using Delivery.DAL.DataBaseObjects;
using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Delivery.BLL.StaticMethods
{
    public class FolderMethods //// НЕ РАБОТАЮТ
    {
        protected delegate void ReadMethod(Stream stream);
        private static void ReadXLS(Stream stream)
        {
            ReadXLSMethods.ReadXLS(stream);
        }

        public static void TakeXLSFromFolder()
        {
            TakeFileFromFolder(new ReadMethod(ReadXLS), "");
        }

        private static void TakeFileFromFolder(ReadMethod readMethod, string filter = null)
        {
            Stream stream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (filter != null)
                openFileDialog.Filter = filter;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = openFileDialog.OpenFile()) != null)
                    {
                        readMethod(stream);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}