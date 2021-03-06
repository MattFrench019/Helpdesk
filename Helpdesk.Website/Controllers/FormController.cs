﻿using System;
using System.Threading.Tasks;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;
using Helpdesk.Website.Models;
using Helpdesk.Website.Services;

namespace Helpdesk.Website.Controllers
{
    [ApiController]
    [Route("api/form")]
    public class FormController : Controller
    {
        [HttpPost]
        public async Task<bool> GetForm([FromHeader] string firebaseJWT, [FromBody] Form form)
        {
            if (firebaseJWT is null || form is null)
            {
                return false;
            }
            
            Console.WriteLine("JWT: " + firebaseJWT);
            Console.WriteLine(form);
            return await FirebaseAuthService.CheckToken(false, firebaseJWT);
        }
    }
}