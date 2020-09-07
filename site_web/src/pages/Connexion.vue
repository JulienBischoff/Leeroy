<template>
  <div class="q-pa-md">
    <h1>Connexion</h1>
    <q-form
    @submit="onSubmit"
    class="q-gutter-md"
    >
      <q-input
      v-model="email"
      label="Enter your email"
      hint="Email"
      :rules="[ val => val && val.length > 0 || 'Please type something']">
      </q-input>
      <q-input
      v-model="password"
      type="password"
      label="Enter your password"
      hint="Password"
      :rules="[ val => val && val.length > 0 || 'Please type something']">
      </q-input>
      <div>
      <q-btn label="Submit" type="submit" color="primary"/>
      </div>
    </q-form>
  </div>
</template>

<script>
import jwt from 'jsonwebtoken'

export default {
  name: 'Connexion',
  data () {
    return {
      email: null,
      password: null,
      SECRET_KEY: 'S2EfMEEFUTyW4Mv1hXTOmwYnz3zSrj9P0SrdtqwUSpaX9ZZU8FWqqnrLbT851nQ'
    }
  },
  methods: {
    onSubmit () {
      this.$axios.get('http://localhost:8080/Authentification?email=' + this.email + '&password=' + this.password)
        .then((response) => {
          this.$store.commit('token/updateToken', jwt.verify(response.data.token, this.SECRET_KEY))
        }).then(() => this.redirect())
    },
    redirect () {
      if (this.$store.state.token.token) {
        this.$router.push('/')
      } else {
        this.$q.notify('Erreur de connexion')
      }
    }
  }
}
</script>

<style scoped>

</style>
