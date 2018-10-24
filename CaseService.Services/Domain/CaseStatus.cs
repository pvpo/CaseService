using System;
using System.Linq;

namespace CaseService.Services.Domain {
    public class CaseStatus {

        public static readonly string[] types = new []{ "Open", "Closed" };

        public readonly static string Open = "Open";
        public readonly static string Closed = "Closed";
        public static Boolean isValidType(string type) {
            return types.Contains(type);
        }

    }
}