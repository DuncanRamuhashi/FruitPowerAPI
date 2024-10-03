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
    public class LoginController : ApiController
    {


        // GET api/<controller>/5
        public object[] Get()
        {
            var userInformation = (from u in GlobalData.powerFruitData.ByoUsers
                                   select u).ToArray(); // Convert to an array

            if (userInformation != null && userInformation.Length > 0)
            {
                return userInformation; // Return the user information array
            }
            else
            {
                return new object[] { false }; // Return an array with false
            }
        }



        // POST api/<controller>
        public string Post([FromBody] PowerFruitUser pfUser)
        {
            if (GlobalData.powerFruitData.ByoUsers.Any(u => u.Email.Equals(pfUser.Email)))
            {
                dynamic userInformation = (from u in GlobalData.powerFruitData.ByoUsers
                                           where u.Email.Equals(pfUser.Email)
                                           select u).Single();
                if (userInformation.Password.Equals(pfUser.Password))
                {
                    return JsonConvert.SerializeObject(userInformation);
                }
                else {
                    return JsonConvert.SerializeObject(false);
                }
            }
            else {
                return JsonConvert.SerializeObject(false);
            }
        }

        // PUT api/<controller>/5
        public string Put([FromBody] PowerFruitUser pfUser)
        {
            if (GlobalData.powerFruitData.ByoUsers.Any(u => u.Id.Equals(pfUser.Id)))
            {
         

                ByoUser byoUserUpdate = GlobalData.powerFruitData.ByoUsers.SingleOrDefault(u => u.Id == pfUser.Id);
                byoUserUpdate.Name = pfUser.Name;
                byoUserUpdate.Email = pfUser.Email;
                byoUserUpdate.Password = pfUser.Password;
                byoUserUpdate.UserType = pfUser.Usertype;
               
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
            if (GlobalData.powerFruitData.ByoUsers.Any(u => u.Id.Equals(id)))
            {
                dynamic userDelete = (from u in GlobalData.powerFruitData.ByoUsers
                                      where u.Id.Equals(id)
                                      select u).Single();
                GlobalData.powerFruitData.ByoUsers.DeleteOnSubmit(userDelete);
                GlobalData.powerFruitData.SubmitChanges();
                return JsonConvert.SerializeObject(true);
            }
            else {

                return JsonConvert.SerializeObject(false) ;
            }
        }
    }
}