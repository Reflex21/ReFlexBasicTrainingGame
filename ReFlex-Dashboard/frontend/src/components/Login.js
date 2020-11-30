import React, { useState } from 'react'
import axios from 'axios'

import { Link, useHistory } from 'react-router-dom'

const Login = () => {
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const history = useHistory()

  const signup = async () => {
    const res = await axios.post('/account/login', { username, password })
    if (res.data !== 'User not found') {
      history.push('/')
    } else {
      alert(res.data)
    }
  }

  return (
    <div className="container mx-1">
      <h2 className="font-weight-bold">Log In</h2>
      <div className="form-group">
        <label>Username:</label>
        <input
          id="new-username-form"
          className="form-control w-50"
          onChange={e => setUsername(e.target.value)}
        />
      </div>
      <div className="form-group">
        <label>Password:</label>
        <input
          type="password"
          className="form-control w-50"
          onChange={e => setPassword(e.target.value)}
        />
      </div>
      <div className="form-group">
        <button
          type="button"
          className="btn btn-primary w-50"
          onClick={() => signup(username, password, history)}
        >
          Log In
        </button>
      </div>
      <p>
        Don&apos;t have an account?&nbsp;
        <Link to="/signup">Sign Up!</Link>
      </p>
    </div>
  )
}

export default Login
