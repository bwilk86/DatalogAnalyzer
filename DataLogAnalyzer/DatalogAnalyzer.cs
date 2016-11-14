using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataLogAnalyzer
{
    public class DatalogAnalyzer
    {

        OpenFileDialog file = new OpenFileDialog();
        string path;
        public void GetIdleFile()
        {
            if (file.ShowDialog() == DialogResult.OK)
            {
                path = file.FileName;
            }

        }
    }
}
