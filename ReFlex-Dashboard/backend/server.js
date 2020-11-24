const express = require('express')
const path = require('path')

const app = express()

app.use(express.static('dist'))
app.use(express.json())

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
