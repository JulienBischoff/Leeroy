package com.example.authentification.web.controller;
import com.example.authentification.DataBaseManager;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

import java.util.Collections;
import java.util.Map;

@CrossOrigin
@RestController
public class UserController {
    DataBaseManager dataBaseManager = new DataBaseManager();

    @RequestMapping(value = "/Authentification", method = RequestMethod.GET, produces = MediaType.APPLICATION_JSON_VALUE)
    public @ResponseBody
    Map authentification(@RequestParam("email") String email, @RequestParam("password") String password){
        return Collections.singletonMap("token",dataBaseManager.FindUserByEmailPassword(email, password));
    }

    @RequestMapping(value = "/AddUser", method = RequestMethod.GET, produces = MediaType.APPLICATION_JSON_VALUE)
    public @ResponseBody
    Map addUser(@RequestParam("email") String email, @RequestParam("password") String password){
        return Collections.singletonMap("response", dataBaseManager.AddUser(email, password));
    }
}
