const state = {
  urlJava: 'http://localhost:8080/',
  urlCSharp: 'https://localhost:44301/api/'
}

const getters = {
  urlJava: (state) => {
    return state.urlJava
  },
  urlCSharp: (state) => {
    return state.urlCsharp
  }
}
export default {
  namespaced: true,
  state,
  getters
}
