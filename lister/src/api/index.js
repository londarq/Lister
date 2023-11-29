import axios from 'axios'

export const BASE_URL = 'https://localhost:7188/'

export const ENDPOINTS = {
  login: 'login',
  tests: 'tests',
  user: 'user',
  question: 'question',
  getAnswers: 'question/getanswers',
}

export const createAPIEndpoint = (endpoint, token) => {
  let url = BASE_URL + 'api/' + endpoint + '/'

  return {
    fetch: () =>
      axios.get(
        url,
        !token
          ? {}
          : {
              headers: {
                Authorization: 'Bearer ' + token,
              },
            }
      ),
    fetchById: (id) => axios.get(url + id),
    post: (newRecord) => axios.post(url, newRecord),
    put: (id, updatedRecord) => axios.put(url + id, updatedRecord),
    delete: (id) => axios.delete(url + id),
  }
}
