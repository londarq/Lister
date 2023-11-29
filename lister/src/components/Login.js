import { TextField, Button, Card, CardContent, Typography } from '@mui/material'
import Box from '@mui/system/Box'
import Center from './Center'
import useForm from '../hooks/useForm'
import { ENDPOINTS, createAPIEndpoint } from '../api'
import useStateContext from '../hooks/useStateContext'
import { useNavigate } from 'react-router-dom'

const getFreshModel = () => ({
  nickname: '',
  password: '',
})

export default function Login() {
  const { context, setContext } = useStateContext()
  const navigate = useNavigate()
  const { values, setValues, errors, setErrors, handleInputChange } =
    useForm(getFreshModel)

  const login = (e) => {
    e.preventDefault()

    if (validate())
      createAPIEndpoint(ENDPOINTS.login)
        .post(values)
        .then((res) => {
          setContext({ token: res.data })
          navigate('/tests-list')
        })
        .catch((err) => console.log(err))
  }

  const validate = () => {
    let temp = {}
    temp.nickname = values.nickname != '' ? '' : 'This field is required.'
    temp.password = values.password != '' ? '' : 'This field is required.'
    setErrors(temp)
    return Object.values(temp).every((x) => x == '')
  }

  return (
    <Center>
      <Card sx={{ width: '400px' }}>
        <CardContent sx={{ textAlign: 'center' }}>
          <Typography variant='h3' sx={{ my: 3 }}>
            Lister
          </Typography>
          <Box
            sx={{
              '& .MuiTextField-root': {
                m: 1,
                width: '90%',
              },
            }}
          >
            <form noValidate onSubmit={login}>
              <TextField
                label='Nickname'
                name='nickname'
                variant='outlined'
                value={values.nickname}
                onChange={handleInputChange}
                {...(errors.nickname && {
                  error: true,
                  helperText: errors.nickname,
                })}
              />
              <TextField
                label='Password'
                name='password'
                variant='outlined'
                value={values.password}
                password
                onChange={handleInputChange}
                {...(errors.password && {
                  error: true,
                  helperText: errors.password,
                })}
              />
              <Button
                sx={{ width: '90%' }}
                type='submit'
                variant='contained'
                size='large'
              >
                Login
              </Button>
            </form>
          </Box>
        </CardContent>
      </Card>
    </Center>
  )
}
