using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.NewModels
{
    public partial class ImportedDatum
    {
        public long Id { get; set; }
        public string Date { get; set; }
        public byte[] Data { get; set; }
        public string Mimetype { get; set; }
    }
}
