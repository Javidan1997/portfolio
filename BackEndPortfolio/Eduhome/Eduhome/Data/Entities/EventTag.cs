﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Data.Entities
{
    public class EventTag
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int TagId { get; set; }

        public Event Event { get; set; }
        public Tag Tag { get; set; }
        public int TeacherId { get; internal set; }
    }
}
