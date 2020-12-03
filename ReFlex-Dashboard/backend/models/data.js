const { Schema, Number, model } = require('mongoose')

const dataSchema = new Schema({
  username: { type: String, required: true },
  type: { type: String, required: true },
  trial: [{ type: Number }],
  accuracy: [{ type: Number }],
  time: [{ type: Number }],
})

module.exports = model('Data', dataSchema)
