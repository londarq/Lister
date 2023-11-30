import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { createAPIEndpoint, ENDPOINTS } from '../api'
import useStateContext from '../hooks/useStateContext'
import { getFormatedTime } from '../helper'
import {
  Card,
  CardContent,
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
import { useNavigate } from 'react-router'

export default function Test() {
  const [qns, setQns] = useState([])
  const [qnIndex, setQnIndex] = useState(1)
  const [timeTaken, setTimeTaken] = useState(0)
  const { context, setContext } = useStateContext()
  const navigate = useNavigate()

  let { id } = useParams()
  let timer

  const startTimer = () => {
    timer = setInterval(() => {
      setTimeTaken((prev) => prev + 1)
    }, [1000])
  }

  useEffect(() => {
    createAPIEndpoint(ENDPOINTS.questions, context.token)
      .fetchById('?testId=' + id)
      .then((res) => {
        setQns(res.data)
        startTimer()
      })
      .catch((err) => {
        console.log(err)
      })

    return () => {
      clearInterval(timer)
    }
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

  const submit = () => {
    let calculatedScore

    createAPIEndpoint(ENDPOINTS.answers, context.token)
      .fetch()
      .then((res) => {
        const testAnswers = qns.flatMap(question => question.answers.map(answer => answer.answerID));
        //get local correct answers from all
        //intersect it with user answers and get ratio
        // или не ratio - подумать
        calculatedScore = 
      })
      .catch((err) => {
        console.log(err)
      })

    
    const dateStub = new Date().getTime().toJSON();
    const testHistory = {
      userId: context.userId,
      testId: id,
      startTime: dateStub,
      endTime: dateStub,
      score: calculatedScore
    }

    createAPIEndpoint(ENDPOINTS.questions, context.token)
      .post(testHistory)
      .then((res) => {
        setQns(res.data)
      })
      .catch((err) => {
        console.log(err)
      })

    const userAnswers = []
    createAPIEndpoint(ENDPOINTS.questions, context.token)
      .post(userAnswers)
      .then((res) => {
        setQns(res.data)
      })
      .catch((err) => {
        console.log(err)
      })

    navigate('/result')
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
        action={<Typography>{getFormatedTime(timeTaken)}</Typography>}
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
              onClick={() => submit()}
            >
              Submit
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
