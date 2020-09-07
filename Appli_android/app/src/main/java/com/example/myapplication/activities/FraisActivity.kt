package com.example.myapplication.activities

import android.os.Bundle
import android.widget.Button
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.myapplication.R
import com.example.myapplication.SessionManager
import com.example.myapplication.api.ApiCsharp
import com.example.myapplication.api.RetrofitClienCsharp
import com.example.myapplication.models.ApiCsharpFraisResponse
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class FraisActivity : AppCompatActivity() {
    private lateinit var btn_get_frais: Button
    private lateinit var sessionManager: SessionManager

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_frais)

        sessionManager = SessionManager(this)
        btn_get_frais = findViewById(R.id.btn_get_frais)
        btn_get_frais.setOnClickListener{
            RetrofitClienCsharp.instance.getFrais(2, 2020, 7)
                .enqueue(object: Callback<ApiCsharpFraisResponse> {
                    override fun onFailure(call: Call<ApiCsharpFraisResponse>, t: Throwable) {
                        Toast.makeText(applicationContext, t.message, Toast.LENGTH_LONG).show()
                    }

                    override fun onResponse( call: Call<ApiCsharpFraisResponse>,  response: Response<ApiCsharpFraisResponse>) {
                        Toast.makeText(applicationContext, response.body().toString(), Toast.LENGTH_LONG).show()
                    }

                })
            Toast.makeText(applicationContext, "bujur", Toast.LENGTH_LONG).show()
        }

    }
}