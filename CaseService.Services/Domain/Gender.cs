using System;
using System.Linq;

namespace CaseService.Services.Domain {
    public class Gender {
        public static readonly string[] types = new []{ "Male", "Female", "Undefined" };

        public readonly static string Male = "Male";
        public readonly static string Female = "Female";
        public readonly static string Undefined = "Undefined";
        public static Boolean isValidType(string type) => types.Contains(type);
    }
}