﻿using System;
using MediatR;

namespace NSE.Core.Messages
{
    public class Event : Message, INotification
    {
        public DateTime Timestamp { get; set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
