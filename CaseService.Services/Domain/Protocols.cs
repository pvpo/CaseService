using System;
using System.Linq;

namespace CaseService.Services.Domain {
    public class Protocols {
        public static readonly string[] types = new []{ "Ki67", "p53", "ER", "PR", "her2", "bcl2" };

        public readonly static string Ki67 = "Ki67";
        public readonly static string p53 = "p53";
        public readonly static string ER = "ER";
        public readonly static string PR = "PR";
        public readonly static string her2 = "her2";
        public static Boolean isValidType(string type) {
            return types.Contains(type);
        }
    }
}