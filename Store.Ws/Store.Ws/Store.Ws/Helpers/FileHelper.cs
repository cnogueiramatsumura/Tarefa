using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Store.Ws.Helpers
{
    public class FileHelper
    {
       public static MemoryStream GetFile(byte[] data)
        {
            var memstream = new MemoryStream(data);
            return memstream;
        }
    }
}