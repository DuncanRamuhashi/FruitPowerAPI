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
    public class CartController : ApiController
    {
        // GET api/<controller>

        // GET api/<controller>/5
        public string Get(int id)
        {
            dynamic fruitCart = (from f in GlobalData.powerFruitData.Fruits
                                        where f.Id.Equals(id)
                                        select f);

            if (fruitCart != null)
            {
                return JsonConvert.SerializeObject(fruitCart);
            }
            else
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        // POST api/<controller>
        public string Post([FromBody] PowerFruitCart p)
        {
            if (p != null)
            {
                var cart = new Cart
                {
                    FruitNumber = p.FruitNumber,
                    Detail = p.Detail,
                    PRICE = p.Price,

                };
                try
                {
                    GlobalData.powerFruitData.Carts.InsertOnSubmit(cart);
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
        public string Put([FromBody] PowerFruitCart p)
        {
            if (GlobalData.powerFruitData.Carts.Any(u => u.Id.Equals(p.Id)))
            {


                Cart cart = GlobalData.powerFruitData.Carts.SingleOrDefault(u => u.Id == p.Id);
                cart.FruitNumber = p.FruitNumber;
                cart.Detail = p.Detail;
                cart.PRICE = p.Price; 


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
            if (GlobalData.powerFruitData.Carts.Any(u => u.Id.Equals(id)))
            {
                dynamic cart = (from u in GlobalData.powerFruitData.Carts
                                      where u.Id.Equals(id)
                                      select u).Single();
                GlobalData.powerFruitData.Carts.DeleteOnSubmit(cart);
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