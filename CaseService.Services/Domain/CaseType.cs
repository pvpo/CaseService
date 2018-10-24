using System;
using System.Linq;

namespace CaseService.Services.Domain {

    public class CaseType {
        public static readonly string[] types = new []{ "Cytology", "Histology" };

        public readonly static string Cytology = "Cytology";
        public readonly static string Histology = "Cytology";
        public static Boolean isValidType(string type) {
            return types.Contains(type);
        }

        public static String GetCode(String type) {
            if(isValidType(type)) {
                if(type.Equals(Histology)) {
                    return "18H";
                } else {
                    return "18C";
                }
            } else {
                throw new ArgumentException("The provided type is invalid.");
            }
        }

        
        
    }
}