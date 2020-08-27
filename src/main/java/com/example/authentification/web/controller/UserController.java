package com.example.authentification.web.controller;
import com.example.authentification.DataBaseManager;
import com.example.authentification.User;
import org.springframework.web.bind.annotation.*;

import java.sql.*;

@RestController
public class UserController {
    DataBaseManager dataBaseManager = new DataBaseManager();

    @RequestMapping(value = "/Authentification", method = RequestMethod.GET)
    public @ResponseBody
    User getUser(@RequestParam("email") String email, @RequestParam("password") String password){
        User user = dataBaseManager.FindUserByEmailPassword(email, password);
        return user;
    }
}
