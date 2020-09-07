package com.example.myapplication.models

import com.google.gson.annotations.SerializedName
import java.sql.Date

data class Frais (
    @SerializedName("id")
    var id: Int,

    @SerializedName("employe_id")
    var employe_id: Int,

    @SerializedName("intitule")
    var intitule: String,

    @SerializedName("montant")
    var montant: Float,

    @SerializedName("devise")
    var devise: String,

    @SerializedName("date")
    var date: Date,

    @SerializedName("statut")
    var statut: String,

    @SerializedName("motif")
    var motif: String,

    @SerializedName("note_frais_id")
    var note_frais_id: Int
    )