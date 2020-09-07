package com.example.myapplication.api

import com.example.myapplication.models.ApiCsharpFraisResponse
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.Query

interface ApiCsharp {
    @GET("frais/list/")
    fun getFrais(@Query("id") id: Int, @Query("annee") annee: Int, @Query("mois") mois: Int): Call<ApiCsharpFraisResponse>
}