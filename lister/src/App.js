import { BrowserRouter, Routes, Route } from 'react-router-dom'
import './App.css'
import Login from './components/Login'
import Test from './components/Test'
import Result from './components/Result'
import Layout from './components/Layout'
import TestsList from './components/TestsList'
import Authenticate from './components/Authenticate'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Login />} />
        <Route element={<Authenticate />}>
          <Route path='/' element={<Layout />}>
            <Route path='/tests-list' element={<TestsList />} />
            <Route path='/test/:id' element={<Test />} />
            <Route path='/result/:id' element={<Result />} />
          </Route>
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App
