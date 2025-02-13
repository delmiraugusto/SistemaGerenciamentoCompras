using API.Models;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using static API.Models.Purchase;

namespace API.Controllers;

public class PurchaseController : BaseController<PurchaseInsert, PurchaseUpdate>
{
    private readonly PurchaseService _service;
    private readonly PurchaseRepository _repository;

    public PurchaseController() 
    {
        _service = new PurchaseService();
        _repository = new PurchaseRepository();
    }

    public override IActionResult Create(PurchaseInsert obj)
    {
        try
        {
            int inserted = _service.Insert(obj);
            return inserted == 0 ? Problem("Object not inserted", obj.ToString()) : Created("Sucess", obj);
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

    [HttpGet("user/{id}")]
    public IActionResult ReadByUserId(long id)
    {
        try
        {
            if (id <= 0) return BadRequest();
            dynamic i = _repository.SelectByUserId(id);
            return i == null ? NotFound() : Ok(i);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public override IActionResult UpdateById(long id, PurchaseUpdate obj)
    {
        try
        {
            dynamic i = _service.Update(obj, id);
            return i == 0
                ? Problem($"Object {id} not updated, {i} rows affected")
                : Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}
