<template>
  <div class="q-pa-md">
    <h1>Registration</h1>
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

export default {
  name: 'Registration',
  data () {
    return {
      email: null,
      password: null,
      SECRET_KEY: 'S2EfMEEFUTyW4Mv1hXTOmwYnz3zSrj9P0SrdtqwUSpaX9ZZU8FWqqnrLbT851nQ'
    }
  },
  computed: {
    urlJava () {
      return this.$store.state.urls.urlJava
    }
  },
  methods: {
    onSubmit () {
      this.$axios.get(this.urlJava + 'AddUser?email=' + this.email + '&password=' + this.password)
        .then((response) => {
          if (response.data === 'Adresse mail déjà utilisée') {
            this.$q.notify('Email déjà utilisé')
          } else {
            this.$router.push('/Connexion')
          }
        })
    }
  }
}
</script>

<style scoped>

</style>
