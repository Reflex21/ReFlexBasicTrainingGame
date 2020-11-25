import React, { useState, useEffect } from 'react'
import axios from 'axios'
import AccuracyGraph from './AccuracyGraph'

const TrainingView = () => {
  const [currentUser, setCurrentUser] = useState('')

  return (
    <div className="col-10">
      <p> Training! </p>
    </div>
  )
}

export default TrainingView
