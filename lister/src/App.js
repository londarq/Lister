import { BrowserRouter, Routes, Route } from 'react-router-dom'
import './App.css'
import Login from './components/Login'
import Test from './components/Test'
import Result from './components/Result'
import Layout from './components/Layout'
import TestsList from './components/TestsList'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Login />} />
        <Route path='/' element={<Layout />}>
          <Route path='/tests-list' element={<TestsList />} />
          <Route path='/quiz' element={<Test />} />
          <Route path='/result' element={<Result />} />
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App
