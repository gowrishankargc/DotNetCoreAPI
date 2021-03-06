
using EmployeeDataAccessLayer.Models;
using EmployeeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CORE6API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee employee_service;
        public EmployeeController(IEmployee employee)
        {
            employee_service = employee;
        }

        private static List<Employee> employee = new List<Employee>() { 
        new Employee{Id = 0, FirstName="sample",LastName="sample", Place="sample",Position="sample"}
        };

        private async void GetTest()
        {
            await Task.Run(() =>
           {
               Thread.Sleep(5000);
               Console.WriteLine("GetTest");
           });

        }

        private  void GetTest2()
        {
            Thread.Sleep(5000);
            Console.WriteLine("GetTest2");
        }

        [HttpGet]
        [Route("GetAsyncMethodTest")]
        public void  GetAsyncCodeTest()
        {
            GetTest();
            GetTest2();
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            var employees = await employee_service.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employee = await employee_service.GetEmployeeById(id);
            if (employee == null)
            {
                return BadRequest("Employee not found");
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            var status = await employee_service.AddEmployee(employee);
            if (status == 0)
                return BadRequest("Employee add failed");
            return Ok("Employee added");
        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee updatedEmployee)
        {
            if(!EmployeeExists(updatedEmployee.Id))
            {
                return NotFound("Employee not found");
            }
            
            var status = await employee_service.UpdateEmployee(updatedEmployee);
            if (status == 0)
                return BadRequest("Employee update failed");
            return Ok("Employee updated");
        }

        private bool EmployeeExists(int id)
        {
            return employee_service.EmployeeExists(id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var status = await employee_service.DeleteEmployee(id);
            if (status == 0)
                return BadRequest("Employee not found");
            return Ok("Employee deleted");
        }
    }
}
