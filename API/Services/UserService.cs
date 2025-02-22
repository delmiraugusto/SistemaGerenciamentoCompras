using API.Models;
using API.Repositories;

namespace API.Services;

public class UserService
{

    private readonly UserRepository _userRepository;

    public UserService() 
    { 
        _userRepository = new UserRepository();
    }
    public dynamic Insert(UserInsert obj)
    {
        if (obj is null) throw new Exception("Object can't be null");
        if(string.IsNullOrEmpty(obj.email)) throw new Exception("Email can't be null");
        if(string.IsNullOrEmpty(obj.password) || obj.password.Length < 8) throw new Exception("Password can't be null or less than 8");
        if (string.IsNullOrEmpty(obj.name)) throw new Exception("Name can't be null");
        if (obj.roleID <= 0) throw new Exception("Id role invalid");

        obj.password = BCrypt.Net.BCrypt.HashPassword(obj.password);

        return _userRepository.Insert(obj);
    }

    public dynamic Update(UserUpdate obj, long id)
    {
        if (obj is null) throw new Exception("Object can't be null");
        if (string.IsNullOrEmpty(obj.email)) throw new Exception("Email can't be null");
        if (string.IsNullOrEmpty(obj.name)) throw new Exception("Name can't be null");
        
        dynamic exists = _userRepository.SelectById(id);
        if (exists == null || exists.id == -1) throw new KeyNotFoundException("User not found.");
        if (string.IsNullOrWhiteSpace(obj.password))
        {
            obj.password = exists.password;
        }
        else 
        {
            if (obj.password.Length < 8) throw new Exception("Password can't be null or less than 8");

            obj.password = BCrypt.Net.BCrypt.HashPassword(obj.password);
        }

        return _userRepository.UpdateById(obj, id);
    }

    public dynamic Delete(long id)
    {
        try
        {
            if (id <= 0) throw new Exception("Id can't be null");
            dynamic exists = _userRepository.SelectById(id);
            if (exists == null || exists.id == -1) throw new KeyNotFoundException("User not found.");
            return _userRepository.DeleteById(id);
        }
        catch (Microsoft.Data.Sqlite.SqliteException ex) when (ex.SqliteErrorCode == 19)
        {
            throw new InvalidOperationException("Cannot delete user because there are related records in other tables.");
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }
}
