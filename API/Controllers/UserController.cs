﻿using API.Models;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseController<UserInsert, UserUpdate>
    {
        private readonly UserService _service;
        private readonly TokenService _tokenService;
        private readonly UserRepository _repository;

        public UserController(TokenService tokenService)
        {
            _tokenService = tokenService;
            _service = new UserService();
            _repository = new UserRepository();
        }

        [AllowAnonymous]
        public override IActionResult Create(UserInsert obj)
        {
            try
            {
                int inserted = _service.Insert(obj);
                return inserted == 0 ? Problem("object not inserted", obj.ToString()) : Created("sucess", obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override IActionResult DeleteById(long id)
        {
            try
            {
                dynamic i = _service.Delete(id);
                return i == 0 ? Problem($"Object {id} not updated, {i} rows affected") : NoContent();
            }
            catch (InvalidOperationException ex) 
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override IActionResult Read()
        {
            try
            {
                List<dynamic> i = _repository.SelectAll();
                return i.Count == 0 ? NotFound() : Ok(i);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override IActionResult Read(long id)
        {
            try
            {
                if (id <= 0) return BadRequest();
                dynamic i = _repository.SelectById(id);
                return i == null ? NotFound() : Ok(i);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public override IActionResult UpdateById(long id, UserUpdate obj)
        {
            try
            {
                dynamic result = _service.Update(obj, id);
                return result == 0
                    ? Problem($"Object {id} not updated, {result} rows affected")
                    : Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Validate(UserLogin login)
        {
            try
            {
                var user = _repository.SelectByEmail(login.email);

                if (login.email == null) { 
                    return Unauthorized("Invalid email or password.");
                }

                if (user == null)
                {
                    return Unauthorized("Invalid email or password.");
                }


                if (!BCrypt.Net.BCrypt.Verify(login.password, user.password))
                {
                    return Unauthorized("Invalid email or password.");
                }

                var token = _tokenService.CreateToken(user.id, user.email, user.roleID);

                UserLoginResponse response = new()
                {
                    roleID = user.roleID,
                    name = user.email,
                    token = token
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
