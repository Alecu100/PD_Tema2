using CinemaRest.Helpers;
using CinemaRest.Models;
using System.Collections.Generic;

namespace CinemaRest.Persistence {
    class SchedulePersistence 
    {
        public static List <ScheduleModel> GetSchedules () {
            List<ScheduleModel> result = new List<ScheduleModel> ();

            return result;
        }

        public static PersistenceCodes AddSchedule (ScheduleModel newSchedule) {
            return PersistenceCodes.Ok;
        }

        public static PersistenceCodes EditSchedule (string scheduleID, ScheduleModel schedule) {
            return PersistenceCodes.Ok;
        }

        public static PersistenceCodes RemoveSchedule (string scheduleID) {
            return PersistenceCodes.Ok;
        }
    }
}