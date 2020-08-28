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

    public Statement ConnectionDB(){
        Statement state = null;
        try {
            //Connection à la DB
            Class.forName("com.mysql.jdbc.Driver");
            String urlDB = "jdbc:mysql://localhost:3306/utilisateurs_leeroy";
            String userDB = "root";
            String passwordDB = "";

            Connection conn = DriverManager.getConnection(urlDB, userDB, passwordDB);

            state = conn.createStatement();
        } catch (Exception e) {
            e.printStackTrace();
        }

        return state;
    }

    public String FindUserByEmailPassword(String email, String password){
        String userFinded = null;
        Statement state = ConnectionDB();
        //Requete SQL
        try {
            String request = "SELECT * FROM users WHERE email ='"+ email + "' AND password='"+password+"'";
            ResultSet result = state.executeQuery(request);
            ResultSetMetaData resultMeta = result.getMetaData();

            //Lecture du premier resultat
            if(result.next() == true){
                //Création du JWT
                JwtBuilder builder = Jwts.builder()
                        .setIssuedAt(new Date(System.currentTimeMillis())) //Date d'emission
                        .setExpiration(new Date(System.currentTimeMillis() + 1000 * 60 * 60 * 10)) // Date d'expiration (10 heures après)
                        .setSubject("Authentification")
                        .claim("email", result.getString("email"))
                        .claim("role", result.getString("role"))
                        .signWith(SignatureAlgorithm.HS256, SECRET_KEY); //Signature
                result.close();
                state.close();
                userFinded = builder.compact();
            }
        } catch (Exception e){
            e.printStackTrace();
        }
        return userFinded;
    }

    public String AddUser(String email, String password){
        String erreur = null;
        try {

            Statement state = ConnectionDB();

            //Verification qu'aucun compte ne possède cet email
            String request = "SELECT * FROM users WHERE email ='"+ email +"'";
            ResultSet result = state.executeQuery(request);
            if(result.next() == false){
                //Ajout du nouvel utilisateur en base de données
                request = "INSERT INTO users (email, password, role) VALUES ('" + email + "', '" + password +"', 3)";
                int linesUpdated = state.executeUpdate(request);
            } else {
                erreur = "Adresse mail déjà utilisée";
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return erreur;
    }
}
