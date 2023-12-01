import axios from 'axios'

export const BASE_URL = 'https://localhost:7188/'

export const ENDPOINTS = {
  login: 'login',
  tests: 'tests',
  users: 'users',
  questions: 'questions',
  answers: 'answers',
}

export const createAPIEndpoint = (endpoint, token) => {
  let url = BASE_URL + 'api/' + endpoint + '/'

  const getToken = (token) => {
    return !token
      ? {}
      : {
          headers: {
            Authorization: 'Bearer ' + token,
          },
        }
  }

  return {
    fetch: () => axios.get(url, getToken(token)),
    fetchById: (id) => axios.get(url + id, getToken(token)),
    post: (newRecord) => axios.post(url, newRecord, getToken(token)),
    put: (id, updatedRecord) => axios.put(url + id, updatedRecord),
    delete: (id) => axios.delete(url + id),
  }
}
