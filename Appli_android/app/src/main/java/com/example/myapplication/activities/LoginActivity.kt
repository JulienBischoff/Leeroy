package com.example.myapplication.activities

import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.myapplication.R
import com.example.myapplication.SessionManager
import com.example.myapplication.api.RetrofitClient
import com.example.myapplication.models.ApiResponse
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class LoginActivity : AppCompatActivity() {
    private lateinit var sessionManager: SessionManager
    private lateinit var email_input: EditText
    private lateinit var password_input: EditText
    private lateinit var btn_connexion: Button


    override fun onCreate(savedInstanceState: Bundle?){
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)


        sessionManager = SessionManager(this)
        email_input = findViewById(R.id.editTextTextEmailAddress)
        password_input = findViewById(R.id.editTextTextPassword)
        btn_connexion = findViewById(R.id.btn_connexion)
        btn_connexion.setOnClickListener {
            RetrofitClient.instance.login(email_input.text.toString(), password_input.text.toString())
                .enqueue(object: Callback<ApiResponse>{
                    override fun onFailure(call: Call<ApiResponse>, t: Throwable) {
                        Toast.makeText(applicationContext, t.message, Toast.LENGTH_LONG).show()
                    }

                    override fun onResponse(call: Call<ApiResponse>, response: Response<ApiResponse>) {
                        Toast.makeText(applicationContext, response.body()?.token, Toast.LENGTH_LONG).show()
                    }

                })


        }
    }
}