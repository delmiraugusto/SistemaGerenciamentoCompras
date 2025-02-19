using API.Repositories;
using static API.Models.PurchaseItem;

namespace API.Services;

public class PurchaseItemService
{
    private readonly PurchaseItemRepository _repository;
    private readonly ProductRepository _productRepository;
    private readonly PurchaseRepository _purchaseRepository;

    public PurchaseItemService()
    {
        _repository = new PurchaseItemRepository();
        _productRepository = new ProductRepository();
        _purchaseRepository = new PurchaseRepository();
    }

    public dynamic Insert(PurchaseItemInsert obj)
    {
        if (obj.productID <= 0) throw new Exception("Id product invalid");
        if (obj.purchaseID <= 0) throw new Exception("Id purchase invalid");
        if (obj.quantity <= 0) throw new Exception("Quantity invalid");

        dynamic productExists = _productRepository.SelectById(obj.productID);
        if (productExists == null || productExists.id == -1) throw new KeyNotFoundException("product not found.");

        dynamic purchaseExists = _purchaseRepository.SelectById(obj.purchaseID);
        if (purchaseExists == null || purchaseExists.id == -1) throw new KeyNotFoundException("purchase not found.");
        return _repository.Insert(obj);
    }

    public dynamic Update(PurchaseItemUpdate obj, long id)
    {
        if (obj.productID <= 0) throw new Exception("Id product invalid");
        if (obj.purchaseID <= 0) throw new Exception("Id purchase invalid");
        if (obj.quantity <= 0) throw new Exception("Quantity invalid");

        dynamic productExists = _productRepository.SelectById(obj.productID);
        if (productExists == null || productExists.id == -1) throw new KeyNotFoundException("product not found.");

        dynamic purchaseExists = _purchaseRepository.SelectById(obj.purchaseID);
        if (purchaseExists == null || purchaseExists.id == -1) throw new KeyNotFoundException("purchase not found.");

        return _repository.UpdateById(obj, id);
    }


    public dynamic Delete(long id)
    {
        if (id <= 0) throw new Exception("Id purchaseItem invalid");

        dynamic exists = _repository.SelectById(id);
        if (exists == null || exists.id == -1) throw new KeyNotFoundException("PurchaseItem not found.");

        return _repository.DeleteById(id);
    }
}
