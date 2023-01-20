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
using Newtonsoft.Json.Linq;
using Preference_json;
using static System.Windows.Forms.AxHost;

namespace Preference_json
{
    public partial class frmMain : Form
    {
        Json<Preference> preferences = new Json<Preference>($"C:\\Users\\{(WindowsIdentity.GetCurrent().Name).Remove(0, 4)}\\AppData\\Roaming\\BAS-Reporter\\Preferences.json");

        public frmMain()
        {
            InitializeComponent();
            this.Text = $"{this.Text} - USER: {(WindowsIdentity.GetCurrent().Name).Remove(0, 4)}";
            preferences.Load();
            showPreferences(preferences.values);
        }

        void checkPreference(string reportName,string namePreference, string valuePreference)
        {
            var name = "";
            var value = "";
            Preference p;
            var id = preferences.checkId(preferences.values, reportName, namePreference);
            if (id != 0)
            {
                name = namePreference;
                value = valuePreference;
                p = new Preference(id, reportName, name, value);
                preferences.Update(X => X.ID == id, p);
            }
            if (id == 0)
            {
                name = namePreference;
                value = valuePreference;
                p = new Preference(preferences.getId(preferences.values), reportName, name, value);
                preferences.Insert(p);
            }
        }
        
        void showPreferences(List<Preference> list)
        {
            foreach (Preference p in list)
            {
                if (p.Name == "textBox1")
                {
                    this.textBox1.Text = p.Value.ToString();
                }
                if (p.Name == "WindowState")
                {
                    this.WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), p.Value.ToString());
                }
                if (p.Name == "radioButton")
                {
                    if(p.Value == "1")
                    {
                        radioButton1.Checked = true;
                    }
                    if (p.Value == "2")
                    {
                        radioButton2.Checked = true;
                    }
                }
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            checkPreference(this.Name, "WindowState", (this.WindowState).ToString());
            var value = "";
            if(radioButton1.Checked == true)
            {
                value = "1";
            }
            if (radioButton2.Checked == true)
            {
                value = "2";
            }
            checkPreference(this.Name, "radioButton", value);
            checkPreference(this.Name, "textBox1", this.textBox1.Text);
            
            showPreferences(preferences.values);
        }
    }
}
