using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace VideoScheduler.Data
{
    public class YamlRepository
    {
        private readonly string _dataDir;
        private readonly IDeserializer _deserializer;
        private readonly ISerializer _serializer;

        public LibraryData Library { get; private set; }
        public TemplateData Templates { get; private set; }
        public ScheduleData Schedule { get; private set; }
        public AppStateData AppState { get; private set; }

        public YamlRepository(string dataDir)
        {
            _dataDir = dataDir;
            if (!Directory.Exists(_dataDir))
            {
                Directory.CreateDirectory(_dataDir);
            }

            _deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            _serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            LoadAll();
        }

        public void LoadAll()
        {
            Library = Load<LibraryData>("library.yaml") ?? new LibraryData();
            Templates = Load<TemplateData>("templates.yaml") ?? new TemplateData();
            Schedule = Load<ScheduleData>("schedule.yaml") ?? new ScheduleData();
            AppState = Load<AppStateData>("appstate.yaml") ?? new AppStateData();
        }

        public void SaveLibrary() => Save("library.yaml", Library);
        public void SaveTemplates() => Save("templates.yaml", Templates);
        public void SaveSchedule() => Save("schedule.yaml", Schedule);
        public void SaveAppState() => Save("appstate.yaml", AppState);

        private T Load<T>(string filename)
        {
            var path = Path.Combine(_dataDir, filename);
            if (!File.Exists(path)) return default;

            var yaml = File.ReadAllText(path);
            return _deserializer.Deserialize<T>(yaml);
        }

        private void Save<T>(string filename, T data)
        {
            var path = Path.Combine(_dataDir, filename);
            var yaml = _serializer.Serialize(data);
            File.WriteAllText(path, yaml);
        }
    }
}
