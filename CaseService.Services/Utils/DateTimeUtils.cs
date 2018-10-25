using System;

namespace CaseService.Services.Utils {
    public static class DateTimeUtils {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static int ToEpoch(this DateTime date) {
            if (date == null) return int.MinValue;
            TimeSpan epochTimeSpan = date - epoch;
            return (int)epochTimeSpan.TotalSeconds;
        }

        public static DateTime FromUnixTime(long unixTime) {
            return epoch.AddSeconds(unixTime);
        }
    }
}