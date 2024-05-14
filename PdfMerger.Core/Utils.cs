using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfMerger.Core
{
    public static class Utils
    {
        public static bool HasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) 
                    return true;
            }

            return false;
        }
    }
}
