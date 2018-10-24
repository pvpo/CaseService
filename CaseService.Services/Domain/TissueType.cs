using System;
using System.Linq;

namespace CaseService.Services.Domain {
    public class TissueType {
        public static readonly string[] types = new []{ "Breast", "Lung", "Kidney", "Lymph Node", "Skin" };

        public readonly static string Breast = "Breast";
        public readonly static string Lung = "Lung";
        public readonly static string Kidney = "Kidney";
        public readonly static string LymphNode = "Lymph Node";
        public readonly static string Skin = "Skin";

        public static Boolean isValidType(string type) {
            return types.Contains(type);
        }

    }
}