package com.example.myapplication.api

import com.example.myapplication.Constants
import com.example.myapplication.models.ApiResponse
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.Query

interface Api {
    @GET(Constants.LOGIN_URL_AUTH)
    fun login(@Query("email") email: String, @Query("password") password: String): Call<ApiResponse>
}