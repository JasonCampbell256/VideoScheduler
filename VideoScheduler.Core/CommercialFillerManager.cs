using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoScheduler.Domain;

namespace VideoScheduler.Core
{
    public class CommercialFillerManager
    {
        private Dictionary<Guid, CommercialFiller> _commercials = new Dictionary<Guid, CommercialFiller>();
        string path = "commercialFillers.json";

        public void AddOrUpdateCommercial(CommercialFiller commercial)
        {
            LoadCommercials();
            _commercials[commercial.Guid] = commercial;
            SaveCommercials();
        }

        //public void AddNewCommercial(CommercialFiller commercial)
        //{
        //    LoadCommercials();
        //    _commercials.Add(commercial.Guid, commercial);
        //    SaveCommercials();
        //}

        public List<CommercialFiller> GetCommercialFillers()
        {
            LoadCommercials();
            return _commercials.Values.ToList();
        }

        public CommercialFiller GetCommercial(Guid guid)
        {
            LoadCommercials();
            if (_commercials.TryGetValue(guid, out CommercialFiller commercial))
            {
                return commercial;
            }
            return null;
        }

        public void LoadCommercials()
        {
            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText("commercialFillers.json");

                _commercials = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<Guid, CommercialFiller>>(json);
            }
            if (_commercials == null)
            {
                _commercials = new Dictionary<Guid, CommercialFiller>();
            }
            
        }

        public void SaveCommercials()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(_commercials);

            string tempFile = "temp_commercialFillers.json";

            string actualFile = "commercialFillers.json";

            try
            {
                System.IO.File.WriteAllText(tempFile, json);
                if (!System.IO.File.Exists(actualFile))
                {
                    using (System.IO.File.Create(actualFile))
                    {
                    }
                }
                System.IO.File.Delete(actualFile);
                System.IO.File.Move(tempFile, actualFile);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    }
}
