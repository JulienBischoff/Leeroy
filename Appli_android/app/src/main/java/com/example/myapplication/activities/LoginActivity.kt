package com.example.myapplication.activities

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.myapplication.R
import com.example.myapplication.SessionManager
import com.example.myapplication.api.RetrofitClientJava
import com.example.myapplication.models.ApiJavaResponse
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
            RetrofitClientJava.instance.login(email_input.text.toString(), password_input.text.toString())
                .enqueue(object: Callback<ApiJavaResponse>{
                    override fun onFailure(call: Call<ApiJavaResponse>, t: Throwable) {
                        Toast.makeText(applicationContext, t.message, Toast.LENGTH_LONG).show()
                    }

                    override fun onResponse(call: Call<ApiJavaResponse>, response: Response<ApiJavaResponse>) {
                        response.body()?.token?.let { it -> sessionManager.saveAuthToken(it) }
                        Toast.makeText(applicationContext, sessionManager.fetchAuthToken(), Toast.LENGTH_LONG).show()

                        changeActivity()

                    }

                })


        }
    }

    fun changeActivity(){
        val intent = Intent(this, FraisActivity::class.java)
        startActivity(intent)
    }
}