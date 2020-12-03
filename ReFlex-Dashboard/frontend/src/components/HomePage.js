import React, { useState, useEffect } from 'react'
import axios from 'axios'
import { Redirect, useHistory } from 'react-router-dom'
import Header from './Header'
import SideBar from './SideBar'
import InsightView from './InsightView'
import TrainingView from './TrainingView'

const HomePage = ({ isLoggedIn }) => {
  const [currentView, setCurrentView] = useState(0)
  const [currentUser, setCurrentUser] = useState('User')

  const whoIsActive = async () => {
    const res = await axios.get('/account/active')
    return res
  }

  useEffect(() => {
    const intervalID = setInterval(() => {
      whoIsActive().then(res => {
        if (res.data !== '') {
          setCurrentUser(res.data)
        }
      })
    }, 100)

    return () => clearInterval(intervalID)
  }, [])

  return (
    <>
      <Header />
      {(isLoggedIn) && (
      <>
        <div className="container-fluid w-100 h-100">
          <div className="row">
            <SideBar
              currentUser={currentUser}
              setCurrentView={setCurrentView}
            />
            {
              (currentView === 0) && (<InsightView currentUser={currentUser} />)
            }
            {
              (currentView === 1) && (<TrainingView />)
            }
          </div>
        </div>
      </>
      )}
      {(!isLoggedIn) && (
        <Redirect to="/login" />
      )}
    </>
  )
}

export default HomePage
