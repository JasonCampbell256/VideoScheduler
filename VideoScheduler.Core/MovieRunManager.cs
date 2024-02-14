using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideoScheduler.Domain;

namespace VideoScheduler.Core
{
    public class MovieRunManager
    {
        private Dictionary<Guid, SchedulableMovie> _movies = new Dictionary<Guid, SchedulableMovie> ();
        private string path = "movieRuns.json";

        public MovieRunManager()
        {
        }

        public List<SchedulableMovie> GetRuns()
        {
            LoadRuns();
            return _movies.Values.ToList();
        }

        public void AddOrUpdateMovieRun(SchedulableMovie run)
        {
            LoadRuns();
            if (_movies.ContainsKey(run.Guid))
            {
                _movies[run.Guid] = run;
            } else
            {
                _movies.Add(run.Guid, run);
            }
            SaveRuns();
        }

        public SchedulableMovie GetMovieRun(Guid guid)
        {
            LoadRuns();
            if (_movies.TryGetValue(guid, out SchedulableMovie run))
            {
                return run;
            }
            return null;
        }

        public void LoadRuns()
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);

                var movies = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<Guid, SchedulableMovie>>(json);
                if (movies != null)
                {
                    _movies = movies;
                }
            }
        }

        public void SaveRuns()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(_movies);

            string tempFile = "temp_movieRuns.json";

            try
            {
                File.WriteAllText(tempFile, json);
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                File.Replace(tempFile, path, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
