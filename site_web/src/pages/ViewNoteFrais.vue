<template>
  <div>
    <div v-if="token.role == 1" class="q-pa-md">
      <q-btn label="Get All Note Frais" color="primary" @click="getNoteFrais()"/>
      <div v-if="noteFrais.length != 0">
        <q-table
          title="Note Frais"
          :data="noteFrais"
          row-key="id"
          selection="single"
          :selected.sync="selectedNoteFrais"
        >
        </q-table>
      </div>
      <div v-if="selectedNoteFrais.length !=0" class="q-pa-md">
        <q-table
          title="Frais"
          :data="frais"
          row-key="id"
          selection="single"
          :selected.sync="selectedFrais"
        >
        </q-table>
        <q-btn label="Validate this Note Frais" color="primary" @click="validateFrais() "/>
        <br><br>
        <q-btn label="Update / Refuse this Note Frais" color="primary" @click="refuseFrais()"/>
        <br><br>
        <q-btn label="Reset modifications" color="primary" @click="getFrais()"/>
      </div>
      <div color="white">
        {{onChangeSelectedFrais}}
      </div>

      <div v-if="popup">
        <q-dialog v-model="popup" style="width: 800px">
          <q-card style="width: 800px">
            <q-card-section>
              <div class="text-h6">Modify</div>
            </q-card-section>

            <q-card-section class="q-pt-none">
              <q-form
                class="q-gutter-md"
                @submit="selectedFrais = []"
              >
                <q-input
                  v-model="selectedFrais[0]['intitule']"
                  label="Intitulé"
                  :rules="[ val => val && val.length > 0 || 'Please type something']">
                </q-input>
                <q-input
                  v-model="selectedFrais[0]['montant']"
                  label="Montant">
                </q-input>
                <q-input
                  v-model="selectedFrais[0]['devise']"
                  label="Devise"
                  :rules="[ val => val && val.length > 0 || 'Please type something']">
                </q-input>
                <q-input
                  v-model="selectedFrais[0]['date']"
                  label="Date"
                  type = "date"
                  :rules="[ val => val && val.length > 0 || 'Please type something']">
                </q-input>
                <q-input
                  v-model="selectedFrais[0]['statut']"
                  label="Statut">
                </q-input>
                <q-input
                  v-model="selectedFrais[0]['motif']"
                  label="Motif">
                </q-input>
                <q-btn flat label="Update" type="submit" color="primary" />
              </q-form>
            </q-card-section>
          </q-card>
        </q-dialog>
      </div>
    </div>
    <div v-if="token.role != 1" class="q-pa-md">
      <h3>Vous n'avez pas les droits de voir les notes de frais</h3>
    </div>
  </div>
</template>

<script>
import jwt from 'jsonwebtoken'

export default {
  name: 'ViewNoteFrais',
  data () {
    return {
      noteFrais: [],
      frais: [],
      selectedNoteFrais: [],
      selectedFrais: [],
      SECRET_KEY: 'S2EfMEEFUTyW4Mv1hXTOmwYnz3zSrj9P0SrdtqwUSpaX9ZZU8FWqqnrLbT851nQ'
    }
  },
  computed: {
    token () {
      return this.$store.state.token.token
    },
    urlCsharp () {
      return this.$store.state.urls.urlCSharp
    },
    popup () {
      if (this.selectedFrais.length > 0) {
        return true
      } else {
        return false
      }
    },
    onChangeSelectedFrais () {
      this.getFrais()
      return this.selectedNoteFrais
    }
  },
  methods: {
    async getNoteFrais () {
      this.noteFrais = []
      if (this.token) {
        this.$axios.defaults.headers.common.Authorization = jwt.sign(this.token, this.SECRET_KEY)
        var url = this.urlCsharp + 'noteFrais/list/'
        await this.$axios.get(url)
          .then((response) => {
            this.noteFrais = response.data
          })
      } else {
        this.$q.notify('Connectez vous')
        this.$router.push('/Connexion')
      }
    },
    async getFrais () {
      this.frais = []
      if (this.token) {
        if (this.selectedNoteFrais.length > 0) {
          this.$axios.defaults.headers.common.Authorization = jwt.sign(this.token, this.SECRET_KEY)
          var url = this.urlCsharp + 'frais/note-frais/' + this.selectedNoteFrais[0].id
          await this.$axios.get(url)
            .then((response) => {
              this.frais = response.data
            })
        }
      } else {
        this.$q.notify('Connectez vous')
        this.$router.push('/Connexion')
      }
    },
    async validateFrais () {
      if (this.token) {
        if (this.selectedNoteFrais.length > 0) {
          this.$axios.defaults.headers.common.Authorization = jwt.sign(this.token, this.SECRET_KEY)
          var url = this.urlCsharp + 'noteFrais/validate/'
          var body = '{"id": ' + this.selectedNoteFrais[0].id +
            ', "montant_total": ' + this.selectedNoteFrais[0].montant_total +
            ', "statut": "' + this.selectedNoteFrais[0].statut +
            '", "date": "' + this.selectedNoteFrais[0].date +
            '"}'
          await this.$axios.post(url, body, { headers: { 'content-type': 'text/json' } })
            .then((response) => this.$q.notify('Statuts update: ' + response.status))
        }
      } else {
        this.$q.notify('Connectez vous')
        this.$router.push('/Connexion')
      }
    },
    async refuseFrais () {
      if (this.token) {
        this.$axios.defaults.headers.common.Authorization = jwt.sign(this.token, this.SECRET_KEY)
        // Récupération de l'annee et du mois pour la requete api
        var annee = this.selectedNoteFrais[0].date.slice(0, 4)
        var mois = this.selectedNoteFrais[0].date.slice(5, 7)
        // Récupération des frais qui ont un statut
        var updatedFrais = this.frais.filter(frais => frais.statut.length > 0)
        var url = this.urlCsharp + 'noteFrais/refuse/' + this.selectedNoteFrais[0].id + '/' + annee + '/' + mois
        var body = updatedFrais
        await this.$axios.post(url, body, { headers: { 'content-type': 'text/json' } })
          .then((response) => this.$q.notify('Statuts update: ' + response.status))
      } else {
        this.$q.notify('Connectez vous')
        this.$router.push('/Connexion')
      }
    }
  }
}
</script>

<style scoped>

</style>
