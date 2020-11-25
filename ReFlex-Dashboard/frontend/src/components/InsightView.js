import React, { useState, useEffect } from 'react'
import axios from 'axios'
import AccuracyGraph from './AccuracyGraph'

const InsightView = () => {
  const [currentUser, setCurrentUser] = useState('')

  return (
    <div className="col-10">
      <div className="row p-5">
        <div className="col-6">
          <AccuracyGraph />
        </div>

        <div className="col-6">
          <div className="card">
            <div className="card-body">
              <h5 className="card-title">Precision</h5>
            </div>
          </div>
        </div>

      </div>

      <div className="row p-5">

        <div className="col-6">
          <div className="card">
            <div className="card-body">
              <h5 className="card-title">Reaction Time</h5>
            </div>
          </div>
        </div>

        <div className="col-6">
          <div className="card">
            <div className="card-body">
              <h5 className="card-title">Training Progress</h5>
            </div>
          </div>
        </div>

      </div>
    </div>
  )
}

export default InsightView
