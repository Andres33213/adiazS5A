using adiazS5A.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adiazS5A
{
    public class PersonRepository
    {
        string dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository (string dbPath1)
        {
            dbPath = dbPath1;
        }
        public void AddNewPerson(string nombre)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Nombre Requerido");
                Persona persona = new Persona() { Name = nombre };
                result = conn.Insert(persona);
                StatusMessage = string.Format("Filas agregadas", result, nombre);
               
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error al insertar", nombre, ex.Message);
            }
        }
        public void UpdatePerson(int personId, string newName)
        {
            try
            {
                Init();
                Persona personaToUpdate = conn.Table<Persona>().FirstOrDefault(p => p.Id == personId);

                if (personaToUpdate != null)
                {
                    personaToUpdate.Name = newName;
                    conn.Update(personaToUpdate);
                    StatusMessage = string.Format("Persona actualizada: ID {0}, Nuevo nombre: {1}", personId, newName);
                }
                else
                {
                    throw new Exception("Persona no encontrada");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al actualizar: {0}", ex.Message);
            }
        }

        public void DeletePerson(int personId)
        {
            try
            {
                Init();
                Persona personaToDelete = conn.Table<Persona>().FirstOrDefault(p => p.Id == personId);

                if (personaToDelete != null)
                {
                    conn.Delete(personaToDelete);
                    StatusMessage = string.Format("Persona eliminada: ID {0}", personId);
                }
                else
                {
                    throw new Exception("Persona no encontrada");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al eliminar: {0}", ex.Message);
            }
        }

        public List<Persona> GetAllPerole()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error al mostrar los datos", ex.Message);
            }
            return new List<Persona>();
        }
    }
}
