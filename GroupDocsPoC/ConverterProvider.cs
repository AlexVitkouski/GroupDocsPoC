using GroupDocsPoC.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupDocsPoC
{
    class ConverterProvider
    {

        readonly CommonConverter commonConverter = new CommonConverter();

        public IConverter GetConverter(string filePath)
        {
            switch (AppChooser.Choose(filePath))
            {
                case SupportedApplications.Word:
                case SupportedApplications.Excel:
                case SupportedApplications.PowerPoint:
                    return commonConverter;
                default:
                    return null;
            }
        }
    }
}
