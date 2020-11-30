import React, { useState, useEffect } from 'react'
import axios from 'axios'
import { Redirect, useHistory } from 'react-router-dom'
import Header from './Header'
import SideBar from './SideBar'
import InsightView from './InsightView'
import TrainingView from './TrainingView'

const HomePage = () => {
  const [currentView, setCurrentView] = useState(0)
  const [isLoggedIn, setIsLoggedIn] = useState(true)

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
        }
      })
    }, 1000)

    return () => clearInterval(intervalID)
  }, [])

  return (
    <>
      {(isLoggedIn) && (
      <>
        <Header />
        <div className="container-fluid w-100 h-100">
          <div className="row">
            <SideBar
              setCurrentView={setCurrentView}
            />
            {
              (currentView === 0) && (<InsightView />)
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
      <Header />
      <div className="container-fluid w-100 h-100">
        <div className="row">
          <SideBar
            setCurrentView={setCurrentView}
          />
          {
            (currentView === 0) && (<InsightView />)
          }
          {
            (currentView === 1) && (<TrainingView />)
          }
        </div>
      </div>
    </>
  )
}

export default HomePage
