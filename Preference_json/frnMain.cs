using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Preference_json;
using static System.Windows.Forms.AxHost;

namespace Preference_json
{
    public partial class frnMain : Form
    {
        DataBase<Preferences> bd = new DataBase<Preferences>($"C:\\Users\\{WindowsIdentity.GetCurrent().Name}\\AppData\\Roaming\\BAS-Reporter\\bd.json");
        
        void mostrar(List<Preferences> list)
        {
            foreach (Preferences p in list)
            {
                this.textBox1.Text = p.Input.ToString();
                this.WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), p.State.ToString());
                if(p.BtnRadio == 1)
                {
                    radioButton1.Checked = true;
                }
                if(p.BtnRadio > 1)
                {
                    radioButton2.Checked = true;
                }
            }
        }

        public frnMain()
        {
            InitializeComponent();
            bd.Load();
            mostrar(bd.values);
        }

        private void frnMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            int DNI = 3613;
            var s = this.WindowState;
            int r = 1;
            
            if (radioButton1.Checked == true)
            {
                r = 1;
            }
            if (radioButton2.Checked == true)
            {
                r = 2;
            }
            Preferences p = new Preferences(DNI, textBox1.Text, s.ToString(), r);
            if (!File.Exists($"C:\\Users\\{WindowsIdentity.GetCurrent().Name}\\AppData\\Roaming\\BAS-Reporter\\bd.json"))
            {
                bd.Insertar(p);
                mostrar(bd.values);
            }
            bd.Actualizar(X => X.DNI == DNI, p);
            mostrar(bd.values);
        }
    }
}
