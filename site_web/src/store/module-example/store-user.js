const state = {
  token: null
}
const mutations = {
  updateToken (state, val) {
    state.token = val
  }
}
const actions = {
  updateToken ({ commit }, payload) {
    commit('updateToken', payload)
  }
}
const getters = {
  token: (state) => {
    return state.token
  }
}
export default {
  namespaced: true,
  state,
  mutations,
  actions,
  getters
}
