using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Preference_json
{
    
    internal class Preferences
    {
        public int DNI;
        public string Input;
        public string State;
        public int BtnRadio;

        public Preferences(int dni, string input, string state, int radio)
        {
            DNI = dni;
            Input = input;
            State = state;
            BtnRadio = radio;
        }
    }

    public class DataBase<T>
    {
        public List<T> values = new List<T>();
        public string route;

        public DataBase(string r)
        {
            route = r;
        }

        public void Save()
        {
            string texto = JsonConvert.SerializeObject(values);
            if (!Directory.Exists($"C:\\Users\\{(WindowsIdentity.GetCurrent().Name).Remove(0, 4)}\\AppData\\Roaming\\BAS-Reporter"))
            {
                Directory.CreateDirectory($"C:\\Users\\{ (WindowsIdentity.GetCurrent().Name).Remove(0, 4)}\\AppData\\Roaming\\BAS-Reporter");
            }
            File.WriteAllText(route, texto);
        }

        public void Load()
        {
            try
            {
                string file = File.ReadAllText(route);
                values = JsonConvert.DeserializeObject<List<T>>(file);
            }
            catch (Exception) { }
        }

        public void Insert(T newFile)
        {
            values.Add(newFile);
            Debug.WriteLine($"output: {newFile}");
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
