import React, { useState, useEffect } from 'react'
import axios from 'axios'
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  useHistory,
} from 'react-router-dom'

import HomePage from './components/HomePage'
import Login from './components/Login'
import SignUp from './components/SignUp'

const App = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false)
  const [currentUser, setCurrentUser] = useState('User')

  const whoIsActive = async () => {
    const res = await axios.get('/account/active')
    return res
  }

  useEffect(() => {
    const intervalID = setInterval(() => {
      whoIsActive().then(res => {
        if (res.data === '') {
          setIsLoggedIn(false)
        } else {
          setIsLoggedIn(true)
          setCurrentUser(res.data)
        }
      })
    }, 100)

    return () => clearInterval(intervalID)
  }, [])


  return (
    <Router>
      <Switch>
        <Route path="/signup">
          <SignUp setIsLoggedIn={setIsLoggedIn} />
        </Route>
        <Route path="/login">
          <Login setIsLoggedIn={setIsLoggedIn} />
        </Route>
        <Route path="/">
          <HomePage currentUser={currentUser} isLoggedIn={isLoggedIn} />
        </Route>
      </Switch>
    </Router>
  )
}

export default App
