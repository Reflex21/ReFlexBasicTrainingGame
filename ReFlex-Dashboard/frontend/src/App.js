import React from 'react'

import Header from './components/Header'
import SideBar from './components/SideBar'
import MainView from './components/MainView'

const App = () => (
  <>
    <Header />
    <div className="container-fluid w-100 h-100">
      <div className="row">
        <SideBar />
        <MainView />
      </div>
    </div>
  </>
)

export default App
