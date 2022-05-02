using Newtonsoft.Json;
using Notes.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Notes.Services
{
    class FileIOService
    {
        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }

        public List<NoteModel> LoadData()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new List<NoteModel>()
                {
                    new NoteModel(){Title = "test", Text = "blablabla"}
                };
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<NoteModel>>(fileText);
            }
        }

        public void SaveData(List<NoteModel> noteDataList)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(noteDataList);
                writer.Write(output);
            }
        }
    }
}
