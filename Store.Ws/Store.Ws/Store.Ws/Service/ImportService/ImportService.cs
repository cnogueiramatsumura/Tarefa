using Store.Data.Models;
using Store.Ws.Helpers;
using Store.Ws.Models;
using StoreWS.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StoreWS.Service
{
    public class ImportService : IImportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientService _clienteService;
        private readonly IProductService _produtctService;
        public ImportService(IUnitOfWork unitOfWork, IClientService clienteService, IProductService produtctService)
        {
            _unitOfWork = unitOfWork;
            _clienteService = clienteService;
            _produtctService = produtctService;
        }

        public List<Order> Example(byte[] data)
        {
            var memstream = FileHelper.GetFile(data);
            var streamReader = new StreamReader(memstream);
            string[] lines = streamReader.ReadToEnd().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var filelistItens = new List<Items>();
            var listPedidos = new List<Order>();
            for (int i = 1; i < lines.Length; i++)
            {
                var ordem = lines[i].Split(';');
                var item = new Items
                {
                    ClientID = new Guid(ordem[0]),
                    ProductID = new Guid(ordem[1]),
                    Quantity = int.Parse(ordem[2])
                };
                filelistItens.Add(item);
            }

            var clients = filelistItens.GroupBy(x => x.ClientID).Distinct();
            foreach (var item in clients)
            {
                var cliente = _clienteService.Get().Where(x => x.ClientId == item.Key).FirstOrDefault();
                var order = new Order()
                {
                    OrderId = Guid.NewGuid(),
                    ClientId = item.Key,
                    CreatedOn = DateTime.Now,
                    Client =  cliente
                };
                var orderItems = item.GroupBy(x => x.ProductID).Distinct();
                foreach (var orderitem in orderItems)
                {
                    var produto = _produtctService.Get().Where(x => x.ProductId == orderitem.Key).FirstOrDefault();
                    var itemorder = new OrderItem
                    {
                        OrderItemId = new Guid(),
                        OrderId = order.OrderId,
                        ProductId = orderitem.Key,
                        Quantity = orderitem.Sum(x => x.Quantity),
                        Products = produto
                    };
                   order.OrderItems.Add(itemorder);
                }
                listPedidos.Add(order);
            }
            return listPedidos;             
        }
    }
}