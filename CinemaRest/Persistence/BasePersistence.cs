using System;
using System.Collections.Generic;
using System.IO;
using CinemaRest.Helpers;
using CinemaRest.Models;
using Newtonsoft.Json;

namespace CinemaRest.Persistence
{
    internal abstract class BasePersistence<T>
    {
        public List<T> GetAll()
        {
            var result = new List<T>();

            var fileEntries = Directory.GetFiles(GetPath());

            foreach (var file in fileEntries)
            {
                var user = JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
                result.Add(user);
            }

            return result;
        }

        public PersistenceCodes Add(BaseModel newModel)
        {
            newModel.ID = Guid.NewGuid().ToString();
            var file = GetFilePath(newModel.ID);

            if (File.Exists(file))
            {
                return PersistenceCodes.IdAlreadyUsed;
            }

            var l_Output = JsonConvert.SerializeObject(newModel);
            File.WriteAllText(file, l_Output);

            return PersistenceCodes.Ok;
        }

        public PersistenceCodes Edit(string id, BaseModel model)
        {
            var file = GetFilePath(id);

            if (!File.Exists(file))
            {
                return PersistenceCodes.IdNotFound;
            }

            var oldModel = JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
            model.ID = (oldModel as BaseModel).ID;

            var l_Output = JsonConvert.SerializeObject(model);
            File.WriteAllText(file, l_Output);

            return PersistenceCodes.Ok;
        }

        public PersistenceCodes Remove(string id)
        {
            var file = GetFilePath(id);

            if (!File.Exists(file))
            {
                return PersistenceCodes.IdNotFound;
            }

            File.Delete(file);

            return PersistenceCodes.Ok;
        }

        private string GetFilePath(string id)
        {
            return GetPath() + GetFilePrefix() + "-" + id + Constants.JsonFileExtension;
        }

        public abstract string GetPath();

        public abstract string GetFilePrefix();
    }
}