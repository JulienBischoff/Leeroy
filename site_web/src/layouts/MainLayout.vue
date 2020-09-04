<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-btn
          flat
          dense
          round
          icon="menu"
          aria-label="Menu"
          @click="leftDrawerOpen = !leftDrawerOpen"
        />

        <q-toolbar-title>
          Quasar App
        </q-toolbar-title>
        <div>
          <q-btn
            v-if="token==null"
            dense
            color="red"
            icon="perm_identity"
            to="/Connexion"
          >Connexion
          </q-btn>
          <q-btn
            v-if="token!=null"
            dense
            color="red"
            icon="perm_identity"
            @click="deconnexion ()">
            ( {{token.email}} ) Deconnexion
          </q-btn>
        </div>
      </q-toolbar>
    </q-header>

    <q-drawer
      v-model="leftDrawerOpen"
      show-if-above
      bordered
      content-class="bg-grey-1"
    >
      <q-list>
        <q-item-label
          header
          class="text-grey-8"
        >
          Essential Links
        </q-item-label>
        <EssentialLink
          v-for="link in essentialLinks"
          :key="link.title"
          v-bind="link"
        />
      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import EssentialLink from 'components/EssentialLink.vue'

const linksData = [
  {
    title: 'Home',
    caption: '',
    icon: 'home',
    link: '/'
  },
  {
    title: 'View Frais',
    caption: 'Have a view of frais',
    icon: 'backup_table',
    link: '/ViewFrais'
  },
  {
    title: 'View Note Frais',
    caption: 'Have a view of note frais',
    icon: 'backup_table',
    link: '/ViewNoteFrais'
  }
]

export default {
  name: 'MainLayout',
  components: { EssentialLink },
  data () {
    return {
      leftDrawerOpen: false,
      essentialLinks: linksData
    }
  },
  computed: {
    token () {
      return this.$store.state.token.token
    }
  },
  methods: {
    deconnexion () {
      this.$store.commit('token/updateToken', null)
    }
  }
}
</script>
