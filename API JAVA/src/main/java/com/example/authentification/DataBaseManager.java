package com.example.authentification;
import org.apache.commons.codec.binary.Hex;

import io.jsonwebtoken.Jwt;
import io.jsonwebtoken.JwtBuilder;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;

import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.PBEKeySpec;
import javax.crypto.spec.SecretKeySpec;
import javax.xml.bind.DatatypeConverter;
import java.nio.charset.Charset;
import java.security.Key;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.KeySpec;
import java.sql.*;
import java.util.UUID;

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
        String userFound = null;
        Statement state = ConnectionDB();
        //Requete SQL
        try {
            String request = "SELECT * FROM users WHERE email ='"+ email + "'";
            ResultSet result = state.executeQuery(request);

            //Lecture du premier resultat
            if(result.next() == true){

                byte[]salt = result.getString("salt").getBytes();
                String hashed_password = hashPassword(salt, password);

                if(hashed_password.equals(result.getString("password"))) {
                    //Création du JWT
                    JwtBuilder builder = Jwts.builder()
                            .setIssuedAt(new Date(System.currentTimeMillis())) //Date d'emission
                            .setExpiration(new Date(System.currentTimeMillis() + 1000 * 60 * 60 * 10)) // Date d'expiration (10 heures après)
                            .setSubject("Authentification")
                            .claim("email", result.getString("email"))
                            .claim("role", result.getString("role"))
                            .claim("id", result.getInt("id"))
                            .signWith(SignatureAlgorithm.HS256, SECRET_KEY.getBytes("UTF-8")); //Signature
                    result.close();
                    state.close();
                    userFound = builder.compact();
                } else {
                    return result.getString("password") + " / " + hashed_password;
                }
            }
        } catch (Exception e){
            e.printStackTrace();
        }
        return userFound;
    }

    public String AddUser(String email, String password){
        String error = null;
        try {

            Statement state = ConnectionDB();

            //Verification qu'aucun compte ne possède cet email
            String request = "SELECT * FROM users WHERE email ='"+ email +"'";
            ResultSet result = state.executeQuery(request);
            if(result.next() == false){


                //String salted_password = hach(password+result.salt)
                SecureRandom random = new SecureRandom();
                String salt = UUID.randomUUID().toString().replaceAll("-", "");
                String hashed_password = hashPassword(salt.getBytes(), password);

                //Ajout du nouvel utilisateur en base de données
                request = "INSERT INTO users (email, password, salt, role) VALUES ('" + email + "', '" + hashed_password +"', '" + salt + "', 3)";
                int linesUpdated = state.executeUpdate(request);
            } else {
                error = "Adresse mail déjà utilisée";
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return error;
    }

    public String hashPassword(byte[] salt, String password) throws NoSuchAlgorithmException {
        try {
            KeySpec spec = new PBEKeySpec(password.toCharArray(), salt, 65536, 128);
            SecretKeyFactory factory = SecretKeyFactory.getInstance("PBKDF2WithHmacSHA1");
            byte[]hashed_password = factory.generateSecret(spec).getEncoded();

            return Hex.encodeHexString(hashed_password);
        } catch (InvalidKeySpecException e) {
            e.printStackTrace();
        }
        return null;
    }
}
