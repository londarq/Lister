import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { createAPIEndpoint, ENDPOINTS, BASE_URL } from '../api'
import useStateContext from '../hooks/useStateContext'
import {
  Card,
  CardContent,
  CardMedia,
  CardHeader,
  List,
  ListItemButton,
  Typography,
  Box,
  Stack,
  LinearProgress,
  Button,
} from '@mui/material'
import { ArrowForward, ArrowBack, DoneOutline } from '@mui/icons-material'
// import { getFormatedTime } from '../helper'
import { useNavigate } from 'react-router'

export default function Test() {
  const [qns, setQns] = useState([])
  const [qnIndex, setQnIndex] = useState(1)
  const [timeTaken, setTimeTaken] = useState(0)
  const { context, setContext } = useStateContext()
  const navigate = useNavigate()

  let { id } = useParams()

  //   let timer

  //   const startTimer = () => {
  //     timer = setInterval(() => {
  //       setTimeTaken((prev) => prev + 1)
  //     }, [1000])
  //   }

  useEffect(() => {
    createAPIEndpoint(ENDPOINTS.questions, context.token)
      .fetchById('?testId=' + id)
      .then((res) => {
        setQns(res.data)
        console.log(res.data)
      })
      .catch((err) => {
        console.log(err)
      })
  }, [])

  const toggleAnswer = (qnId, answerIdx) => {
    let temp = [...context.selectedAnswers]
    let selectedAnswer = {
      qnId,
      selected: answerIdx,
    }

    console.log(selectedAnswer)

    if (
      temp.some(
        (item) =>
          item.qnId === selectedAnswer.qnId &&
          item.selected === selectedAnswer.selected
      )
    ) {
      console.log('before splice:' + temp)
      temp = temp.filter(
        (item) =>
          !(
            item.qnId === selectedAnswer.qnId &&
            item.selected === selectedAnswer.selected
          )
      )
      console.log('after splice:' + temp)
    } else {
      console.log('push:' + selectedAnswer)
      temp.push(selectedAnswer)
    }

    setContext({ selectedAnswers: temp })
  }

  return qns.length !== 0 ? (
    <Card
      sx={{
        maxWidth: 640,
        mx: 'auto',
        mt: 5,
        '& .MuiCardHeader-action': { m: 0, alignSelf: 'center' },
      }}
    >
      <CardHeader
        title={'Question ' + qnIndex + ' of ' + qns.length}
        //action={<Typography>{getFormatedTime(timeTaken)}</Typography>}
      />
      <Box>
        <LinearProgress
          variant='determinate'
          value={(qnIndex * 100) / qns.length}
        />
      </Box>
      <CardContent>
        <Typography variant='h6'>{qns[qnIndex - 1].questionText}</Typography>
        <List>
          {qns[qnIndex - 1].answers.map((answer, idx) => (
            <ListItemButton
              key={idx}
              sx={
                context.selectedAnswers.some(
                  (item) =>
                    item.qnId === qnIndex && item.selected === answer.answerID
                )
                  ? {
                      border: 1,
                      borderRadius: 1,
                      borderColor: 'secondary.main',
                      mt: 1,
                    }
                  : { mt: 1 }
              }
              onClick={() => toggleAnswer(qnIndex, answer.answerID)}
            >
              <div>
                <b>{String.fromCharCode(65 + idx) + ' . '}</b>
                {answer.answerText}
              </div>
            </ListItemButton>
          ))}
        </List>
        <Stack direction='row' justifyContent='space-between' sx={{ pt: 2 }}>
          <Button
            variant='contained'
            size='small'
            startIcon={<ArrowBack />}
            disabled={qnIndex === 1}
            onClick={() => setQnIndex(qnIndex - 1)}
          >
            Go Back
          </Button>
          {qnIndex === qns.length ? (
            <Button
              variant='contained'
              size='small'
              startIcon={<DoneOutline />}
              onClick={() => navigate('/result')}
            >
              Finish
            </Button>
          ) : (
            <Button
              variant='contained'
              size='small'
              endIcon={<ArrowForward />}
              onClick={() => setQnIndex(qnIndex + 1)}
            >
              Next
            </Button>
          )}
        </Stack>
      </CardContent>
    </Card>
  ) : null
}
