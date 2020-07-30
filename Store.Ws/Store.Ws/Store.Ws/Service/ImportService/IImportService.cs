using Store.Data.Models;
using System.Collections.Generic;
using System.Web;

namespace StoreWS.Service
{
    public interface IImportService
    {
        List<Order> Example(byte[] file);
    }
}
