using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupDocsPoC
{
    static class AppChooser
    {
        private static readonly string[] WordExtensions =
        {
            ".doc",
            ".docm",
            ".docx",
            ".dot",
            ".dotm",
            ".dotx",
            ".odt",
            ".rtf",
        };

        private static readonly string[] ExcelExtensions =
        {
            ".xls",
            ".xlsb",
            ".xlsm",
            ".xlsx",
            ".xlt",
            ".xltm",
            ".xltx",
        };

        private static readonly string[] PowerPointExtensions =
        {
            ".pot",
            ".potm",
            ".potx",
            ".pps",
            ".ppsm",
            ".ppsx",
            ".ppt",
            ".pptm",
            ".pptx",
        };

        public static SupportedApplications Choose(string filePath)
        {
            var ext = Path.GetExtension(filePath);
            if (IsSupported(WordExtensions, ext)) return SupportedApplications.Word;
            if (IsSupported(ExcelExtensions, ext)) return SupportedApplications.Excel;
            if (IsSupported(PowerPointExtensions, ext)) return SupportedApplications.PowerPoint;
            return SupportedApplications.Unsupported;
        }
        private static bool IsSupported(IEnumerable<string> supportedExtensions, string ext)
        {
            var res = supportedExtensions.Any(s => string.Equals(s, ext, StringComparison.InvariantCultureIgnoreCase));
            return res;
        }
    }
}
