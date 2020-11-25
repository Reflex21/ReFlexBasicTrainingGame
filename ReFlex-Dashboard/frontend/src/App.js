import React, { useState } from 'react'

import Header from './components/Header'
import SideBar from './components/SideBar'
import InsightView from './components/InsightView'
import TrainingView from './components/TrainingView'

const App = () => {
  const [currentView, setCurrentView] = useState(0)
  return (
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
  )
}

export default App
