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
    public class IssueController : ApiController
    {


        // GET api/<controller>/5
        public object[] Get()
        {
            var issues = GlobalData.powerFruitData.Issues.ToArray();

            if (issues != null && issues.Length > 0)
            {
                return issues;
            }
            else
            {
                return new Issue[0]; // Return an empty array if no issues are found
            }
        }


        // POST api/<controller>
        public string Post([FromBody] PowerFruitIssue i)
        {
            if (i != null)
            {
                var issue = new Issue
                {
        
                 FullName = i.FullName,
                 Email = i.Email,
                 Subject = i.Subject,
                 Message = i.Message,
                 Status = i.Status,

                   };
                try
                {
                    GlobalData.powerFruitData.Issues.InsertOnSubmit(issue);
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
        public string Put([FromBody] PowerFruitIssue i)
        {
            if (GlobalData.powerFruitData.Issues.Any(u => u.Id.Equals(i.Id)))
            {


                Issue issue = GlobalData.powerFruitData.Issues.SingleOrDefault(u => u.Id == i.Id);
                issue.FullName = i.FullName;
                issue.Email = i.Email;
                issue.Subject = i.Subject;
                issue.Message = i.Message;
                issue.Status = i.Status;


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
            if (GlobalData.powerFruitData.Issues.Any(u => u.Id.Equals(id)))
            {
                dynamic issue = (from u in GlobalData.powerFruitData.Issues
                                where u.Id.Equals(id)
                                select u).Single();
                GlobalData.powerFruitData.Issues.DeleteOnSubmit(issue);
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