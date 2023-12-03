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
      .fetchById(id)
      .then((res) => {
        setQns(res.data)
      })
      .catch((err) => {
        console.log(err)
      })
      .finally(startTimer())

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
    const selectedAnswersId = context.selectedAnswers.map((sa) => sa.selected)

    createAPIEndpoint(ENDPOINTS.answers, context.token)
      .post(
        selectedAnswersId.map((sa) => ({
          userId: context.userId,
          selectedAnswerId: sa,
        }))
      )
      .then((res) => {
        console.log('1/3. Answers saved')
      })
      .catch((err) => {
        console.log(err)
      })

    createAPIEndpoint(ENDPOINTS.answers, context.token)
      .fetch()
      .then((res) => {
        console.log('2/3. Got correct answers')
        const testAnswersId = qns.flatMap((q) =>
          q.answers.map((a) => a.answerID)
        )
        console.log('testAnswersId = ' + testAnswersId)

        const correctAnswersId = res.data
          .map((a) => a.answerID)
          .filter((ca) => testAnswersId.includes(ca))
        console.log('correctAnswersId = ' + correctAnswersId)

        const correctSelectionsId = correctAnswersId.filter((a) =>
          selectedAnswersId.includes(a)
        )
        console.log('correctSelectionsId = ' + correctSelectionsId)

        const calculatedScore =
          (correctSelectionsId.length / correctAnswersId.length) * 100
        //todo
        const dateStub = new Date().toISOString()

        const testHistory = {
          userId: context.userId,
          testId: id,
          startTimestamp: dateStub,
          finishTimestamp: dateStub,
          score: Math.trunc(calculatedScore),
        }

        createAPIEndpoint(ENDPOINTS.history, context.token)
          .post(testHistory)
          .then((res) => {
            console.log('3/3. The test is now history')
            setContext({ timeTaken: 0, selectedAnswers: [] })
            navigate(`/result/${id}`)
          })
          .catch((err) => {
            console.log(err)
          })
      })
      .catch((err) => {
        console.log(err)
      })
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
