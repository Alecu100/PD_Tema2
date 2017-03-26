using CinemaRest.Helpers;
using CinemaRest.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CinemaRest.Persistence {
    abstract class BasePersistence <T> {

        public List <T> GetAll () {
            List <T> result = new List<T> ();

            string [] fileEntries = Directory.GetFiles (GetPath ());

            foreach (string file in fileEntries) {
                T user = JsonConvert.DeserializeObject<T> (File.ReadAllText (file));
                result.Add (user);
            }

            return result;
        }

        public PersistenceCodes Add (BaseModel newModel) {
            newModel.ID = System.Guid.NewGuid ().ToString ();
            string file = GetFilePath (newModel.ID);

            if (File.Exists (file)) {
                return PersistenceCodes.IdAlreadyUsed;
            }

            string l_Output = JsonConvert.SerializeObject (newModel);
            File.WriteAllText (file, l_Output);

            return PersistenceCodes.Ok;
        }

        public PersistenceCodes Edit (string id, BaseModel model) {
            string file = GetFilePath (id);

            if (!File.Exists (file)) {
                return PersistenceCodes.IdNotFound;
            }

            T oldModel = JsonConvert.DeserializeObject<T> (File.ReadAllText (file));
            model.ID = (oldModel as BaseModel).ID;

            string l_Output = JsonConvert.SerializeObject (model);
            File.WriteAllText (file, l_Output);

            return PersistenceCodes.Ok;
        }

        public PersistenceCodes Remove (string id) {
            string file = GetFilePath (id);

            if (!File.Exists (file)) {
                return PersistenceCodes.IdNotFound;
            }

            File.Delete (file);

            return PersistenceCodes.Ok;
        }

        private string GetFilePath (string id) {
            return (GetPath () + id);
        }

        public abstract string GetPath ();
    }
}
