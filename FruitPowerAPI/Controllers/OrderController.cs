using FruitPowerAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FruitPowerAPI.Controllers
{
    public class OrderController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            dynamic orderInformation = (from o in GlobalData.powerFruitData.Orders
                                       select o);

            if (orderInformation != null)
            {
                return JsonConvert.SerializeObject(orderInformation);
            }
            else
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            dynamic order = (from o in GlobalData.powerFruitData.Orders
                                 where o.Id.Equals(id)
                                 select o);

            if (order != null)
            {
                return JsonConvert.SerializeObject(order);
            }
            else
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        // POST api/<controller>
        public string Post([FromBody]PowerFruitOrder o)
        {
            if (o != null)
            {
                var order = new Order
                {
                    userID = o.userID,
                    Detail = o.Detail,
                    Price = o.Price,
                    Status = o.Status,

                };
                try
                {
                    GlobalData.powerFruitData.Orders.InsertOnSubmit(order);
                    GlobalData.powerFruitData.SubmitChanges();
                    return JsonConvert.SerializeObject(true);
                }
                catch (Exception ex)
                {

                    ex.GetBaseException();
                    return JsonConvert.SerializeObject(false); ;
                }


            }
            else
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        // PUT api/<controller>/5
        public string Put([FromBody] PowerFruitOrder o)
        {
            if (GlobalData.powerFruitData.Orders.Any(u => u.Id.Equals(o.Id)))
            {


                Order order = GlobalData.powerFruitData.Orders.SingleOrDefault(u => u.Id == o.Id);
                order.Detail = o.Detail;
                order.Price = o.Price;
                order.Status = o.Status;


                GlobalData.powerFruitData.SubmitChanges();

                return JsonConvert.SerializeObject(true);
            }
            else
            {

                return JsonConvert.SerializeObject(false);
            }
        }

        // DELETE api/<controller>/5
        public string Delete(int id)
        {
            if (GlobalData.powerFruitData.Orders.Any(u => u.Id.Equals(id)))
            {
                dynamic orderDelete  = (from o in GlobalData.powerFruitData.Orders
                                      where o.Id.Equals(id)
                                      select o).Single();
                GlobalData.powerFruitData.Orders.DeleteOnSubmit(orderDelete);
                GlobalData.powerFruitData.SubmitChanges();
                return JsonConvert.SerializeObject(true);
            }
            else
            {

                return JsonConvert.SerializeObject(false);
            }
        }
    }
}