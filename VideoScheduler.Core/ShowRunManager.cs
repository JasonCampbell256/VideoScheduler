using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideoScheduler.Domain;

namespace VideoScheduler.Core
{
    public class ShowRunManager
    {
        private Dictionary<Guid, ShowRun> _runs = new Dictionary<Guid, ShowRun>();
        private VideoLibrary _videoLibrary = new VideoLibrary();
        private string path = "showRuns.json";

        public ShowRunManager(VideoLibrary videoLibrary)
        {
            _videoLibrary = videoLibrary;
        }

        public List<ShowRun> GetRuns()
        {
            LoadRuns();
            return _runs.Values.ToList();
        }

        public void AddOrUpdateShowRun(ShowRun run)
        {
            LoadRuns();
            _runs[run.Guid] = run;
            SaveRuns();
        }

        public ShowRun GetShowRun(Guid guid)
        {
            LoadRuns();
            if (_runs.TryGetValue(guid, out ShowRun run))
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

                _runs = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<Guid, ShowRun>>(json);
                if (_runs != null)
                {
                    foreach (var run in _runs.Values)
                    {
                        run.NextEpisode = _videoLibrary.GetShowEpisode(run.NextEpisodeFilePath);
                    }
                }
                
            }
            if (_runs == null)
            {
                _runs = new Dictionary<Guid, ShowRun>();
            }
            
        }

        public void SaveRuns()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(_runs);

            string tempFile = "temp_showRuns.json";

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
