const mongoose = require('mongoose')
const express = require('express')
const cookieSession = require('cookie-session')
const path = require('path')

const AccountRouter = require('./routes/account')
const ApiRouter = require('./routes/api')

const app = express()
const MONGO_URI = process.env.MONGODB_URI || 'mongodb://localhost:27017/reflex'

mongoose.connect(MONGO_URI, {
  useNewUrlParser: true,
  useUnifiedTopology: true,
})

app.use(express.static('dist'))
app.use(express.json())

app.use(
  cookieSession({
    name: 'local-session',
    keys: ['reflex'],
    maxAge: 24 * 60 * 60 * 1000, // 24 hours
  }),
)

app.use('/api', ApiRouter)
app.use('/account', AccountRouter)

app.get('/favicon.ico', (_, res) => res.status(404).send())
app.get('*', (_, res) => {
  res.sendFile(path.join(__dirname, '../dist/index.html'))
})

app.use((err, req, res, next) => {
  console.error(err.stack)
  res.status(500).send(`${err}`)
})

app.listen(3000, () => {
  console.log('listening on 3000')
})
