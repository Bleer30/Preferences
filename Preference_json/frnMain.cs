using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Preference_json;
using static System.Windows.Forms.AxHost;

namespace Preference_json
{
    public partial class frnMain : Form
    {
        DataBase<Preferences> bd = new DataBase<Preferences>("bd.json");
        
        void mostrar(List<Preferences> list)
        {
            foreach (Preferences p in list)
            {
                this.textBox1.Text = p.Input.ToString();
                this.WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), p.State.ToString());
                if(p.BtnRadio.tipo == 0)
                {
                    radioButton1.Checked = true;
                }
                if(p.BtnRadio.tipo > 0)
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

        private void button1_Click(object sender, EventArgs e)
        {
            var s = this.WindowState;
            Radio r = new Radio();
            Preferences p = new Preferences((new Random()).Next(1000, 9999), textBox1.Text, s.ToString(), r);
            bd.Insertar(p);
            mostrar(bd.values);
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    int DNI = int.Parse(textBox1.Text);
        //    var s = this.WindowState;
        //    Radio r = new Radio();
        //}
    }
}
