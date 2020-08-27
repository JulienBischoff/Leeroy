package com.example.authentification;

import java.sql.*;

public class DataBaseManager {
    public DataBaseManager(){

    };

    public User FindUserByEmailPassword(String email, String password){
        User userFinded = null;
        try {
            Class.forName("com.mysql.jdbc.Driver");
            String urlDB = "jdbc:mysql://localhost:3306/utilisateurs_leeroy";
            String userDB = "root";
            String passwordDB = "";

            Connection conn = DriverManager.getConnection(urlDB, userDB, passwordDB);

            Statement state = conn.createStatement();
            String request = "SELECT * FROM users WHERE email ='"+ email + "' AND password='"+password+"'";
            ResultSet result = state.executeQuery(request);
            ResultSetMetaData resultMeta = result.getMetaData();
            result.first();
            userFinded = new User(result.getString("email"), result.getString("password"), result.getString("nom"), result.getString("role"), result.getDate("date_creation"), result.getDate("date_expiration"));
            result.close();
            state.close();

        } catch (Exception e) {
            e.printStackTrace();
        }
        return userFinded;
    }
}
