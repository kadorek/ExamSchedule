﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.Models
{
    public partial class Arrangement
    {
        public long Id { get; set; }
        public long ScheduleId { get; set; }
        public string Data { get; set; }
    }
}
