import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'
import { createAPIEndpoint, ENDPOINTS, BASE_URL } from '../api'
import { List } from '@mui/material'

export default function TestsList() {
  const [tests, setTests] = useState([])
  const navigate = useNavigate()

  useEffect(() => {
    createAPIEndpoint(ENDPOINTS.tests)
      .fetch()
      .then((res) => {
        setTests(res.data)
        console.log(tests)
      })
      .catch((err) => console.log(err))
  }, [])

  return <div>Testy</div>
}
