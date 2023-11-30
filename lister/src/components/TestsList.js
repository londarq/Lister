import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'
import { createAPIEndpoint, ENDPOINTS, BASE_URL } from '../api'
import useStateContext from '../hooks/useStateContext'
import { List, Card, CardContent, CardHeader, Typography } from '@mui/material'

export default function TestsList() {
  const [tests, setTests] = useState([])
  const { context, setContext } = useStateContext()
  const navigate = useNavigate()

  useEffect(() => {
    createAPIEndpoint(ENDPOINTS.tests, context.token)
      .fetch()
      .then((res) => {
        setTests(res.data)
      })
      .catch((err) => console.log(err))
  }, [])

  return (
    <>
      {tests.map((test, idx) => {
        return (
          <Card
            key={idx}
            onClick={() => navigate(`/test/${test.testId}`)}
            sx={{
              maxWidth: 640,
              mx: 'auto',
              mt: 5,
              '& .MuiCardHeader-action': { m: 0, alignSelf: 'center' },
            }}
          >
            <CardHeader title={test.name} />
            <CardContent>
              <Typography variant='h6'>{test.description}</Typography>
            </CardContent>
          </Card>
        )
      })}
    </>
  )
}
