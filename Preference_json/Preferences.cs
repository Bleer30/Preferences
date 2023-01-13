using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        public string ruta;

        public DataBase(string r)
        {
            ruta = r;
        }

        public void Save()
        {
            string texto = JsonConvert.SerializeObject(values);
            File.WriteAllText(ruta, texto);
        }

        public void Load()
        {
            try
            {
                string archivo = File.ReadAllText(ruta);
                values = JsonConvert.DeserializeObject<List<T>>(archivo);
            }
            catch (Exception) { }
        }

        public void Insertar(T nuevo)
        {
            values.Add(nuevo);
            Debug.WriteLine($"output: {nuevo}");
            Save();
        }

        public List<T> Buscar(Func<T, bool> criterio)
        {
            return values.Where(criterio).ToList();
        }

        public void Eliminar (Func<T, bool> criterio)
        {
            values = values.Where(x => !criterio(x)).ToList();
        }

        public void Actualizar(Func<T, bool> criterio, T nuevo)
        {
            values = values.Select(X =>
            {
                if (criterio(X)) X = nuevo;
                return X;
            }).ToList();
            Save();
        }
    }
}
