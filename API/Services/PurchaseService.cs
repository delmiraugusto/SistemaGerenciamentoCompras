﻿using API.Repositories;
using static API.Models.Purchase;

namespace API.Services;

public class PurchaseService
{
    private readonly PurchaseRepository _purchaseRepository;
    private readonly UserRepository _userRepository;

    public PurchaseService()
    {
        _purchaseRepository = new PurchaseRepository();
        _userRepository = new UserRepository();
    }

    public dynamic Insert(PurchaseInsert obj)
    {
        if (obj.userID <= 0) throw new Exception("User ID inválido");
        if (obj.items == null || obj.items.Count == 0) throw new Exception("Purchase must have at least one item");

        foreach (var item in obj.items)
        {
            if (item.productID <= 0) throw new Exception("Id product invalid");
            if (item.quantity <= 0) throw new Exception("Quantity invalid");
        }

        return _purchaseRepository.Insert(obj);
    }


    public dynamic Update(PurchaseUpdate obj, long id)
    {
        if (obj is null) throw new Exception("Object can't be null");
        if (obj.userID <= 0) throw new Exception("Id user invalid");
        if (obj.total <= 0) throw new Exception("Total must be greater than zero");
        if (obj.orderDate == default) throw new Exception("Invalid order date");

        dynamic userExists = _userRepository.SelectById(obj.userID);
        if (userExists == null || userExists.id == -1) throw new KeyNotFoundException("User not found.");

        return _purchaseRepository.UpdateById(obj, id);
    }

    public dynamic Delete(long id)
    {
        if (id <= 0) throw new Exception("Id purchase invalid");

        dynamic exists = _purchaseRepository.SelectById(id);
        if (exists == null || exists.id == -1) throw new KeyNotFoundException("Purchase not found.");

        return _purchaseRepository.DeleteById(id);
    }
}
