const express = require('express')

const User = require('../models/user')
const DataPoint = require('../models/datapoint')
const isAuthenticated = require('../../middlewares/isAuthenticated')

const router = express.Router()

// Get all data for a specific user
router.get('/data/:username/:type', async (req, res) => {
  try {
    const { username, type } = req.params
    console.log(req.params)
    DataPoint.find({ username, type }, (err, data, next) => {
      if (err) {
        next(err)
      }
      if (data) {
        res.send(data)
      }
    })
  } catch (err) {
    res.send(`Failed to GET User Data - ${err}`)
  }
})

// Add an extra piece of data for a specific user
router.post('/data/add', isAuthenticated, async (req, res) => {
  const { data } = req.body
  const { username } = req.session
  console.log(data)
  try {
    if (data.type === 'accuracy') {
      await DataPoint.create({
        username,
        type: data.type,
        trial: data.values.trial,
        accuracy: data.values.accuracy,
      })
      res.send('Data Added')
    } else if (data.type === 'time') {
      await DataPoint.create({
        username,
        type: data.type,
        trial: data.values.trial,
        time: data.values.time,
      })
      res.send('Data Added')
    }
  } catch (err) {
    res.send(`Failed to Add Data - ${err}`)
  }
})

module.exports = router
