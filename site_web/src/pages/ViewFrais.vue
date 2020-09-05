<template>
  <div>
    <div class="q-pa-md" style="max-width: 300px">
      <div class="q-gutter-md">
        <q-form
          @submit="getFrais()"
          class="q-gutter-md"
        >
          <q-select
            filled
            label="Année"
            v-model="annee"
            :options="annee_options"
            :rules="[ val => val && val.length > 0 || 'Please type something']"
          >
          </q-select>
          <q-select
            filled
            label="Mois"
            v-model="mois"
            :options="mois_options"
            :rules="[ val => val && val.length > 0 || 'Please type something']"
          >
          </q-select>
          <q-btn label="Get Frais" type="submit" color="primary"/>
          <q-btn label="Add Frais" color="primary" @click="newFraisBool = true"/>
        </q-form>

      </div>
    </div>
    <div v-if="frais.length != 0">
      <q-table
      title="Frais"
      :data="frais"
      row-key="id"
      selection="single"
      :selected.sync="selected"
      >
      </q-table>
    </div>
    <div v-if="popup">
      <q-dialog v-if="selected.length > 0" v-model="popup" style="width: 800px">
        <q-card style="width: 800px">
          <q-card-section>
            <div class="text-h6">Modify</div>
          </q-card-section>

          <q-card-section class="q-pt-none">
            <q-form
              class="q-gutter-md"
              @submit="updateSelected()"
            >
              <q-input
                v-model="selected[0]['intitule']"
                label="Intitulé"
                :rules="[ val => val && val.length > 0 || 'Please type something']">
              </q-input>
              <q-input
                v-model="selected[0]['montant']"
                label="Montant">
              </q-input>
              <q-input
                v-model="selected[0]['devise']"
                label="Devise"
                :rules="[ val => val && val.length > 0 || 'Please type something']">
              </q-input>
              <q-input
                v-model="selected[0]['date']"
                label="Date"
                type = "date"
                :rules="[ val => val && val.length > 0 || 'Please type something']">
              </q-input>
              <q-input
                v-if="token.role == 3"
                v-model="selected[0]['statut']"
                label="Statut"
                :rules="[ val => val && val.length > 0 || 'Please type something']">
              </q-input>
              <q-input
                v-model="selected[0]['motif']"
                label="Motif"
                v-if="token.role == 3"
                :rules="[ val => val && val.length > 0 || 'Please type something']">
              </q-input>
              <q-btn v-if="selected[0]['statut'] == 'En attente' || token.role == '3'" flat label="Update" type="submit" color="primary" />
              <q-btn flat label="Close" color="primary" @click="selected = []" />

            </q-form>
          </q-card-section>
        </q-card>
      </q-dialog>
      <q-dialog v-if="newFraisBool" v-model="popup" style="width: 800px">
        <q-card style="width: 800px">
          <q-card-section>
            <div class="text-h6">Add</div>
          </q-card-section>

          <q-card-section class="q-pt-none">
            <q-form
              class="q-gutter-md"
              @submit="addNewFrais()"
            >
              <q-input
                v-model="newFrais['intitule']"
                label="Intitulé"
                :rules="[ val => val && val.length > 0 || 'Please type something']">
              </q-input>
              <q-input
                v-model="newFrais['montant']"
                label="Montant">
              </q-input>
              <q-input
                v-model="newFrais['devise']"
                label="Devise"
                :rules="[ val => val && val.length > 0 || 'Please type something']">
              </q-input>
              <q-input
                v-model="newFrais['date']"
                label="Date"
                type = "date"
                :rules="[ val => val && val.length > 0 || 'Please type something']">
              </q-input>
              <q-btn flat label="Add" type="submit" color="primary" />
              <q-btn flat label="Close" color="primary" @click="newFraisBool = false " />

            </q-form>
          </q-card-section>
        </q-card>
      </q-dialog>
    </div>
  </div>

</template>

<script>
import jwt from 'jsonwebtoken'

export default {
  name: 'ViewFrais',
  data () {
    return {
      annee: null,
      annee_options: ['2020', '2021'],
      mois: null,
      mois_options: ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'],
      frais: [],
      selected: [],
      newFrais: { intitule: '', montant: '', devise: '', date: '', statut: '', motif: '' },
      newFraisBool: false,
      SECRET_KEY: 'S2EfMEEFUTyW4Mv1hXTOmwYnz3zSrj9P0SrdtqwUSpaX9ZZU8FWqqnrLbT851nQ'
    }
  },
  computed: {
    token () {
      return this.$store.state.token.token
    },
    popup () {
      if (this.selected.length > 0 || this.newFraisBool) {
        return true
      } else {
        return false
      }
    }
  },
  methods: {
    async getFrais () {
      this.frais = []
      if (this.token) {
        this.$axios.defaults.headers.common.Authorization = jwt.sign(this.token, this.SECRET_KEY)
        var url = 'https://localhost:44301/api/frais/list/' + this.token.id + '/' + this.annee + '/' + this.mois
        this.$axios.get(url)
          .then((response) => {
            this.frais = response.data
          })
      } else {
        this.$q.notify('Connectez vous')
        this.$router.push('/Connexion')
      }
    },
    async updateSelected () {
      if (this.token) {
        this.$axios.defaults.headers.common.Authorization = jwt.sign(this.token, this.SECRET_KEY)
        var url = 'https://localhost:44301/api/frais/update'
        var body = '{"id": ' + this.selected[0].id +
          ', "employe_id": ' + this.selected[0].employe_id +
          ', "intitule": "' + this.selected[0].intitule +
          '", "montant": ' + this.selected[0].montant +
          ', "devise": "' + this.selected[0].devise +
          '", "date": "' + this.selected[0].date +
          '", "statut": "' + this.selected[0].statut +
          '", "motif": "' + this.selected[0].motif +
          '", "note_frais_id": ' + this.selected[0].note_frais_id +
          '}'
        this.selected = []
        await this.$axios.post(url, body, { headers: { 'content-type': 'text/json' } })
          .then((response) => this.$q.notify('Statuts update: ' + response.status))
      } else {
        this.$q.notify('Connectez vous')
        this.$router.push('/Connexion')
      }
    },
    async addNewFrais () {
      if (this.token) {
        this.$axios.defaults.headers.common.Authorization = jwt.sign(this.token, this.SECRET_KEY)
        var url = 'https://localhost:44301/api/frais/create'
        var body = '{"id": 0, "employe_id": ' + this.token.id +
          ', "intitule": "' + this.newFrais.intitule +
          '", "montant": ' + this.newFrais.montant +
          ', "devise": "' + this.newFrais.devise +
          '", "date": "' + this.newFrais.date +
          '", "statut": "' + this.newFrais.statut +
          '", "motif": "' + this.newFrais.motif +
          '", "note_frais_id": 0}'
        console.log(body)
        this.newFraisBool = false
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
