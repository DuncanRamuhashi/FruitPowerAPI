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
    public class FruitController : ApiController
    {
 

        // GET api/<controller>/5
        public object[] Get()
        {

            var fruitList = new List<object>();

            // Assuming GlobalData.powerFruitData.Fruits is a collection
            foreach (var fruit in GlobalData.powerFruitData.Fruits)
            {
                fruitList.Add(fruit);
            }

            
            if (fruitList != null)
            {
               

                return fruitList.ToArray();
            }
            else
            {
                return null;
            }
        }

        // POST api/<controller>
        public string Post([FromBody] PowerFruit p)
        {
            if (p != null)
            {
                var fruit = new Fruit
                {
                    Name = p.Name,
                    Price = p.Price,
                    Photo = ImageConverter.ConvertImageToBinary(p.Photo),
                  
                };
                try
                {
                    GlobalData.powerFruitData.Fruits.InsertOnSubmit(fruit);
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
        public string Put([FromBody] PowerFruit p)
        {
            if (GlobalData.powerFruitData.Fruits.Any(u => u.Id.Equals(p.Id)))
            {


                Fruit pfUpdate = GlobalData.powerFruitData.Fruits.SingleOrDefault(u => u.Id == p.Id);
                pfUpdate.Name = p.Name;
                pfUpdate.Price = p.Price;
                pfUpdate.Photo = ImageConverter.ConvertImageToBinary(p.Photo);
               

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
            if (GlobalData.powerFruitData.Fruits.Any(u => u.Id.Equals(id)))
            {
                dynamic fruit = (from u in GlobalData.powerFruitData.Fruits
                                      where u.Id.Equals(id)
                                      select u).Single();
                GlobalData.powerFruitData.Fruits.DeleteOnSubmit(fruit);
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