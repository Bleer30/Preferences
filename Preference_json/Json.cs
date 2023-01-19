using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Preference_json
{
    internal class Json<T>
    {
        public List<T> values = new List<T>();
        public string route;

        public Json(string r)
        {
            route = r;
        }

        public int getId(List<Preference> list)
        {
            int ID = 1;
            if (list.Count > 0)
            {
                ID = list.Count + 1;
            }
            return ID;
        }

        public int checkId(List<Preference> list, string formularioName, string propName)
        {
            int id = 0;
            foreach (Preference p in list)
            {
                if (p.ReportName == formularioName && p.Name == propName)
                {
                    id = p.ID;
                }
            }
            return id;
        }

        public void Save()
        {
            string texto = JsonConvert.SerializeObject(values);
            File.WriteAllText(route, texto);
        }

        public void Load()
        {
            try
            {
                string file = File.ReadAllText(route);
                values = JsonConvert.DeserializeObject<List<T>>(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Your preferences could not be read for the following reason: " + ex);
            }
        }

        public void Insert(T newFile)
        {
            values.Add(newFile);
            Save();
        }

        public void Update(Func<T, bool> criterion, T newFile)
        {
            values = values.Select(X =>
            {
                if (criterion(X)) X = newFile;
                return X;
            }).ToList();
            Save();
        }
    }
}
