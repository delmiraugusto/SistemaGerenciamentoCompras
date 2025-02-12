using API.Models;
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
                return i == 0 ? Problem($"Object {id} not updated, {i} rows affected") : Ok();
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
        public override IActionResult UpdateById(UserUpdate obj)
        {
            try
            {
                dynamic i = _service.Update(obj);
                return i == 0 ? Problem($"Object {obj.id} not updated, {i} rows affected") : Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Validate(UserUpdate obj)
        {
            try
            {
                UserLoginResponse user = new()
                {
                    roleID = obj.roleID,
                    name = obj.name,
                    token = _tokenService.CreateToken(obj.name),
                };
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
