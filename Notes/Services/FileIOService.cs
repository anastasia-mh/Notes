using Newtonsoft.Json;
using Notes.Models;
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

        public BindingList<NoteModel> LoadData()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<NoteModel>()
                {
                    new NoteModel(){Title = "test", Text = "blablabla"}
                };
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<NoteModel>>(fileText);
            }
        }

        public void SaveData(BindingList<NoteModel> noteDataList)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(noteDataList);
                writer.Write(output);
            }
        }
    }
}
