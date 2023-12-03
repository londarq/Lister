import {
  Button,
  Card,
  CardContent,
  Typography,
  Accordion,
  AccordionDetails,
  AccordionSummary,
  List,
  ListItem,
} from '@mui/material'
import { Box } from '@mui/system'
import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { useNavigate } from 'react-router'
import { createAPIEndpoint, ENDPOINTS } from '../api'
// import { getFormatedTime } from '../helper'
import useStateContext from '../hooks/useStateContext'
import ExpandCircleDownIcon from '@mui/icons-material/ExpandCircleDown'
import { red, green, blue, grey } from '@mui/material/colors'

export default function Result() {
  const { context } = useStateContext()

  const [qns, setQns] = useState([])
  const [userAnswers, setUserAnswers] = useState([])
  const [correctAnswers, setCorrectAnswers] = useState([])

  const [history, setHistory] = useState({})
  const navigate = useNavigate()
  const [expanded, setExpanded] = useState(false)

  let { id } = useParams()

  const handleChange = (panel) => (event, isExpanded) => {
    setExpanded(isExpanded ? panel : false)
  }

  const markCorrectOrNot = (answerId) => {
    console.log('o')
    console.log('o')
    console.log('o')
    console.log('TestAnswersId = ' + getTestAnswersId())
    console.log('UserAnswersId = ' + getUserAnswersId())
    console.log('CorrectSelectionsId = ' + getCorrectSelectionsId())
    console.log('IncorrectSelectionsId = ' + getIncorrectSelectionsId())
    console.log('CorrectAnswersId = ' + getCorrectAnswersId())
    console.log('o')
    console.log('o')
    console.log('o')

    if (getCorrectSelectionsId().includes(answerId)) {
      return { color: green[500] }
    }

    if (getIncorrectSelectionsId().includes(answerId)) {
      return { color: red[500] }
    }

    if (getCorrectAnswersId().includes(answerId)) {
      return { color: blue[500] }
    }
  }

  const getTestAnswersId = () => {
    return qns.flatMap((q) => q.answers.map((a) => a.answerID))
  }

  const getUserAnswersId = () => {
    return userAnswers
      .map((a) => a.selectedAnswerID)
      .filter((ca) => getTestAnswersId().includes(ca))
  }

  const getCorrectAnswersId = () => {
    return correctAnswers
      .map((a) => a.answerID)
      .filter((ca) => getTestAnswersId().includes(ca))
  }

  const getCorrectSelectionsId = () => {
    return getCorrectAnswersId().filter((a) => getUserAnswersId().includes(a))
  }

  const getIncorrectSelectionsId = () => {
    return getUserAnswersId().filter((a) => !getCorrectAnswersId().includes(a))
  }

  useEffect(() => {
    createAPIEndpoint(ENDPOINTS.history, context.token)
      .fetchById(id)
      .then((res) => {
        setHistory(res.data)
        console.log('Got history')

        createAPIEndpoint(ENDPOINTS.questions, context.token)
          .fetchById(id)
          .then((res) => {
            setQns(res.data)
            console.log('Got questions')
            // const testAnswersId = qns.flatMap((q) =>
            //   q.answers.map((a) => a.answerID)
            // )
            // console.log('testAnswersId = ' + testAnswersId)

            createAPIEndpoint(ENDPOINTS.userAnswers, context.token)
              .fetchById(id)
              .then((res) => {
                setUserAnswers(res.data)
                console.log('Got user answers')
                // const userAnswersId = res.data
                //   .map((a) => a.selectedAnswerID)
                //   .filter((ca) => testAnswersId.includes(ca))
                // console.log('userAnswersId = ' + userAnswersId)

                createAPIEndpoint(ENDPOINTS.answers, context.token)
                  .fetch()
                  .then((res) => {
                    setCorrectAnswers(res.data)
                    console.log('Got correct answers')

                    // const correctAnswersId = res.data
                    //   .map((a) => a.answerID)
                    //   .filter((ca) => testAnswersId.includes(ca))
                    // console.log('correctAnswersId = ' + correctAnswersId)
                    // const correctSelectionsId = correctAnswersId.filter((a) =>
                    //   userAnswersId.includes(a)
                    // )
                    // console.log('correctSelectionsId = ' + correctSelectionsId)
                    // const incorrectSelectionsId = testAnswersId.filter(
                    //   (a) => !correctAnswersId.includes(a)
                    // )
                    // console.log(
                    //   'incorrectSelectionsId = ' + incorrectSelectionsId
                    // )

                    // setSelections({
                    //   correctAnswersId,
                    //   correctSelectionsId,
                    //   incorrectSelectionsId,
                    // })
                  })
                  .catch((err) => {
                    console.log(err)
                  })
              })
              .catch((err) => {
                console.log(err)
              })
          })
          .catch((err) => {
            console.log(err)
          })
      })
      .catch((err) => {
        console.log(err)
      })
  }, [])

  return (
    <>
      {getCorrectAnswersId() &&
        getCorrectSelectionsId() &&
        getIncorrectSelectionsId() && (
          <div>
            <Card
              sx={{
                mt: 5,
                display: 'flex',
                width: '100%',
                maxWidth: 640,
                mx: 'auto',
              }}
            >
              <Box
                sx={{ display: 'flex', flexDirection: 'column', flexGrow: 1 }}
              >
                <CardContent sx={{ flex: '1 0 auto', textAlign: 'center' }}>
                  <Typography variant='h4'>
                    {history.score < 60
                      ? 'It was a bad one :('
                      : 'Congratulations!'}
                  </Typography>
                  <Typography variant='h6'>YOUR SCORE</Typography>
                  <Typography variant='h5' sx={{ fontWeight: 600 }}>
                    <Typography variant='span' color={green[500]}>
                      {getCorrectSelectionsId().length}
                    </Typography>
                    /{getCorrectAnswersId().length}
                  </Typography>
                  <Typography variant='h6'>{history.score}%</Typography>
                  <Button
                    variant='contained'
                    sx={{ mx: 1, mt: 2 }}
                    size='small'
                    onClick={() => navigate('/tests-list')}
                  >
                    Home
                  </Button>
                </CardContent>
              </Box>
            </Card>
            <Box sx={{ mt: 5, width: '100%', maxWidth: 640, mx: 'auto' }}>
              {qns.map((qn, idx) => (
                <Accordion
                  disableGutters
                  key={idx}
                  expanded={expanded === idx}
                  onChange={handleChange(idx)}
                >
                  <AccordionSummary expandIcon={<ExpandCircleDownIcon />}>
                    <Typography sx={{ width: '90%', flexShrink: 0 }}>
                      {qn.questionText}
                    </Typography>
                  </AccordionSummary>
                  <AccordionDetails sx={{ backgroundColor: grey[100] }}>
                    <List>
                      {qn.answers.map((answer, idx) => (
                        <ListItem key={idx}>
                          <Typography sx={markCorrectOrNot(answer.answerID)}>
                            <b>{String.fromCharCode(65 + idx) + '. '}</b>
                            {answer.answerText}
                          </Typography>
                        </ListItem>
                      ))}
                    </List>
                  </AccordionDetails>
                </Accordion>
              ))}
            </Box>
          </div>
        )}
    </>
  )
}
