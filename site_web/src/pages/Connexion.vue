<template>
  <div class="q-pa-md">
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
  name: 'Connexion',
  data () {
    return {
      email: null,
      password: null
    }
  },
  methods: {
    onSubmit () {
    }
  }
}
</script>

<style scoped>

</style>
