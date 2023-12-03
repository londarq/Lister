import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'
import { createAPIEndpoint, ENDPOINTS } from '../api'
import useStateContext from '../hooks/useStateContext'
import { Card, CardContent, CardHeader, Typography } from '@mui/material'

export default function TestsList() {
  const [tests, setTests] = useState([])
  const { context } = useStateContext()
  const navigate = useNavigate()

  useEffect(() => {
    createAPIEndpoint(ENDPOINTS.tests, context.token)
      .fetchById(context.userId)
      .then((res) => {
        setTests(res.data)
      })
      .catch((err) => console.log(err))
  }, [])

  return (
    <>
      <Typography variant='h4' align='center' sx={{ p: 3 }}>
        Test yourself
      </Typography>

      <div>
        {tests
          .filter(
            (t) =>
              !t.userTestHistory.some((uth) => uth.UserID !== context.userId)
          )
          .map((test, idx) => {
            return (
              <TestCard
                test={test}
                key={idx}
                onClick={() => navigate(`/test/${test.testId}`)}
              />
            )
          })}
      </div>

      <div>
        <Typography variant='h4' align='center' sx={{ p: 3 }}>
          Your results
        </Typography>

        {tests
          .filter((t) =>
            t.userTestHistory.some((uth) => uth.UserID !== context.userId)
          )
          .map((test, idx) => {
            return (
              <TestCard
                test={test}
                key={idx}
                onClick={() => navigate(`/result/${test.testId}`)}
              />
            )
          })}
      </div>
    </>
  )
}

function TestCard({ test, idx, onClick }) {
  return (
    <Card
      key={idx}
      onClick={onClick}
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
}
