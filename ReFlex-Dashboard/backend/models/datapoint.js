const { Schema, Number, model } = require('mongoose')

const opts = {
  timestamps: true,
}

const dataPointSchema = new Schema({
  username: { type: String, required: true },
  type: { type: String, required: true },
  trial: [{ type: Number }],
  accuracy: [{ type: Number }],
  time: [{ type: Number }],
}, opts)

module.exports = model('DataPoint', dataPointSchema)
