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
    String authentification(@RequestParam("email") String email, @RequestParam("password") String password){
        String token = dataBaseManager.FindUserByEmailPassword(email, password);
        return token;
    }
}
