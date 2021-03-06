﻿using CinemaRest.Helpers;
using CinemaRest.Models;

namespace CinemaRest.Persistence {
    class SchedulePersistence : BasePersistence <ScheduleModel> 
    {
        public override string GetPath () {
            return (Constants.Persistence.Schedules);
        }

        public override string GetFilePrefix()
        {
            return Constants.Persistence.Schedule;
        }
    }
}