const express = require('express')

const isAuthenticated = (req, res, next) => {
  const { username } = req.session

  if (username && username !== '') {
    next()
  } else {
    next(new Error('Unauthorized Sesson'))
  }
}

module.exports = isAuthenticated
