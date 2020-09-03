package com.example.authentification.web.controller;
import com.example.authentification.DataBaseManager;
import org.springframework.web.bind.annotation.*;

@CrossOrigin
@RestController
public class UserController {
    DataBaseManager dataBaseManager = new DataBaseManager();

    @RequestMapping(value = "/Authentification", method = RequestMethod.GET)
    public @ResponseBody
    String authentification(@RequestParam("email") String email, @RequestParam("password") String password){
        String token = dataBaseManager.FindUserByEmailPassword(email, password);
        return token;
    }

    @RequestMapping(value = "/AddUser", method = RequestMethod.GET)
    public @ResponseBody
    String addUser(@RequestParam("email") String email, @RequestParam("password") String password){
        return dataBaseManager.AddUser(email, password);
    }
}
