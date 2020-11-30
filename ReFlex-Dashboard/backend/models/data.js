const { Schema, Number, model } = require('mongoose')

const dataSchema = new Schema({
  username: { type: String, required: true },
  type: { type: String, required: true },
  values: [{ x: Number, y: Number }],
})

module.exports = model('Data', dataSchema)
