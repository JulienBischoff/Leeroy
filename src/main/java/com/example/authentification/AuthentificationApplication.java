package com.example.authentification;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.Statement;

@SpringBootApplication
public class AuthentificationApplication {

    public static void main(String[] args) {

        SpringApplication.run(AuthentificationApplication.class, args);
    }

}
