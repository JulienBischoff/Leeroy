package com.example.authentification;

import java.sql.Date;

public class User {

    public String email;
    public String password;
    public String nom;
    public String role;
    public Date date_creation;
    public Date date_expiration;

    public User(String email, String password, String nom, String role, Date date_creation, Date date_expiration){
        this.email = email;
        this.password = password;
        this.nom = nom;
        this.role = role;
        this.date_creation = date_creation;
        this.date_expiration = date_expiration;
    }
}
