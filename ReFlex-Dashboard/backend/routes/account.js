const express = require('express')

const User = require('../models/user')
const isAuthenticated = require('../../middlewares/isAuthenticated')

const router = express.Router()

router.post('/signup', (req, res) => {
  const { username, password } = req.body

  User.findOne({ username, password }, async (err, user, next) => {
    if (err) {
      next(err)
    }

    if (user) {
      res.send('User Already Exists')
    } else {
      try {
        await User.create({
          username,
          password,
        })
        res.send('User Created')
      } catch (e) {
        res.send(`Failed to Create User - ${e}`)
      }
    }
  })
})

router.post('/login', (req, res) => {
  const { username, password } = req.body

  User.findOne({ username, password }, (err, user, next) => {
    if (err) {
      next(err)
    }
    if (user) {
      req.session.username = username
      req.session.password = password
      console.log(req.session)
      res.send(req.session.username)
    } else {
      res.send('User not found')
    }
  })
})

router.post('/logout', isAuthenticated, (req, res) => {
  req.session.username = ''
  req.session.password = ''
  res.send('Logged Out')
})

router.get('/active', (req, res) => {
  res.send(req.session.username)
})

module.exports = router
