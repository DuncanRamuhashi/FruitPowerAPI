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
    public class RegisterController : ApiController
    {


        // POST api/<controller>
      
        public string Post([FromBody] PowerFruitUser pfUser)
        {
            if (!GlobalData.powerFruitData.ByoUsers.Any(u => u.Email.Equals(pfUser.Email)))
            {
                var user = new ByoUser
                {
                    Name = pfUser.Name,
                    Email = pfUser.Email,
                    Password = pfUser.Password,
                    UserType = pfUser.Usertype,
                };
                try
                {
                    GlobalData.powerFruitData.ByoUsers.InsertOnSubmit(user);
                    GlobalData.powerFruitData.SubmitChanges();
                    return  JsonConvert.SerializeObject(true);
                }
                catch (Exception ex) {

                    ex.GetBaseException();
                    return JsonConvert.SerializeObject(false); ;
                }


            }
            else {
                return JsonConvert.SerializeObject(false);
            }

        }


    }
}