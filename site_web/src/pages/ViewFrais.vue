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
        </q-form>

      </div>
    </div>
    <div v-if="frais.length != 0">
      <q-table
      title="Frais"
      :data="frais"
      row-key="id"
      >
      </q-table>
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
      SECRET_KEY: 'S2EfMEEFUTyW4Mv1hXTOmwYnz3zSrj9P0SrdtqwUSpaX9ZZU8FWqqnrLbT851nQ'
    }
  },
  computed: {
    token () {
      return this.$store.state.token.token
    }
  },
  methods: {
    async getFrais () {
      if (this.token) {
        this.$axios.defaults.headers.common.Authorization = jwt.sign(this.token, this.SECRET_KEY)
        var url = 'https://localhost:44301/api/frais/list/' + this.token.id + '/' + this.annee + '/' + this.mois
        this.$axios.get(url)
          .then((response) => {
            this.frais = response.data
          })
      } else {
        this.$q.notify('Connectez vous').then(this.$router.push('/Connexion'))
      }
    }
  }
}
</script>
