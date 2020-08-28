package com.example.authentification;

import io.jsonwebtoken.Jwt;
import io.jsonwebtoken.JwtBuilder;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;

import java.sql.*;

public class DataBaseManager {

    private String SECRET_KEY = "S2EfMEEFUTyW4Mv1hXTOmwYnz3zSrj9P0SrdtqwUSpaX9ZZU8FWqqnrLbT851nQ";

    public DataBaseManager(){

    };

    public String FindUserByEmailPassword(String email, String password){
        String userFinded = null;
        try {
            //Connection à la DB
            Class.forName("com.mysql.jdbc.Driver");
            String urlDB = "jdbc:mysql://localhost:3306/utilisateurs_leeroy";
            String userDB = "root";
            String passwordDB = "";

            Connection conn = DriverManager.getConnection(urlDB, userDB, passwordDB);

            Statement state = conn.createStatement();

            //Requete SQL
            String request = "SELECT * FROM users WHERE email ='"+ email + "' AND password='"+password+"'";
            ResultSet result = state.executeQuery(request);
            ResultSetMetaData resultMeta = result.getMetaData();

            //Lecture du premier resultat
            if(result.next() == true){
                //Création du JWT
                JwtBuilder builder = Jwts.builder()
                        .setIssuedAt(new Date(System.currentTimeMillis())) //Date d'emission
                        .setExpiration(new Date(System.currentTimeMillis() + 1000 * 60 * 60 * 10)) // Date d'expiration (10 heures après)
                        .setSubject(result.getString("email"))
                        .claim("password", result.getString("password"))
                        .claim("nom", result.getString("nom"))
                        .claim("role", result.getString("role"))
                        .signWith(SignatureAlgorithm.HS256, SECRET_KEY); //Signature
                result.close();
                state.close();
                userFinded = builder.compact();
            }

        } catch (Exception e) {
            e.printStackTrace();
        }
        return userFinded;
    }
}
