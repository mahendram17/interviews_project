using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer;
namespace FinalWebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        public IEnumerable<Emp> Get()
        {
            using (EmployeeDataEntities ed = new EmployeeDataEntities())
            {
                return ed.Emps.ToList();
            }
        }
        public Emp Get(int id)
        {
            using (EmployeeDataEntities ed = new EmployeeDataEntities())
            {
                return ed.Emps.FirstOrDefault(e=>e.EmployeeId==id);
            }
        }

        public HttpResponseMessage Post([FromBody] Emp e)
        {
            try
            {
                using (EmployeeDataEntities ed = new EmployeeDataEntities())
                {
                    ed.Emps.Add(e);
                    ed.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.Created, e);
                    msg.Headers.Location = new Uri(Request.RequestUri + e.EmployeeId.ToString());
                    return msg;
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Put(int id,[FromBody] Emp emp)
        {
            using (EmployeeDataEntities ed = new EmployeeDataEntities())
            {
                var entity = ed.Emps.FirstOrDefault(e => e.EmployeeId == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee Id =" + id.ToString());
                }
                else
                {
                    entity.Name = emp.Name;
                    entity.Age = emp.Age;
                    entity.Salary = emp.Salary;
                    entity.Designation = emp.Designation;
                    ed.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {

                using (EmployeeDataEntities ed = new EmployeeDataEntities())
                {
                    var entity = ed.Emps.FirstOrDefault(e => e.EmployeeId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee Id =" + id.ToString());
                    }
                    else
                    {
                        ed.Emps.Remove(ed.Emps.FirstOrDefault(e => e.EmployeeId == id));
                        ed.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }
    }
}
