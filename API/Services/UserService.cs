﻿using API.Models;
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

        return _userRepository.Insert(obj);
    }

    public dynamic Update(UserUpdate obj)
    {
        if (obj is null) throw new Exception("Object can't be null");
        if (obj.id <= 0) throw new Exception("Id invalid");
        if (string.IsNullOrEmpty(obj.email)) throw new Exception("Email can't be null");
        if (string.IsNullOrEmpty(obj.name)) throw new Exception("Name can't be null");
        if (string.IsNullOrEmpty(obj.password) || obj.password.Length < 8) throw new Exception("Password can't be null or less than 8");
        
        dynamic exists = _userRepository.SelectById(obj.id);
        if (exists == null || exists.id == -1) throw new KeyNotFoundException("User not found.");
        return _userRepository.UpdateById(obj);
    }

    public dynamic Delete(long id)
    {
        if (id <= 0) throw new Exception("Id can't be null");
        dynamic exists = _userRepository.SelectById(id);
        if (exists == null || exists.id == -1) throw new KeyNotFoundException("User not found.");
        return _userRepository.DeleteById(id);
    }
}
