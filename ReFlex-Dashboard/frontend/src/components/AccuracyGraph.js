import React from 'react'
import { ScatterChart, Scatter, Tooltip, YAxis, XAxis } from 'recharts'

const AccuracyGraph = () => {
  const data = [
    {
      a: 1,
      b: 30,
    },
    {
      a: 2,
      b: 40,
    },
    {
      a: 3,
      b: 50,
    },
    {
      a: 4,
      b: 67,
    },
    {
      a: 5,
      b: 65,
    },
    {
      a: 6,
      b: 70,
    },
    {
      a: 7,
      b: 75,
    },
  ]

  return (
    <div className="card">
      <div className="card-body">
        <h5 className="card-title">Accuracy</h5>
        <ScatterChart
          width={400}
          height={300}
        >
          <Tooltip
            formatter={(value, name) => {
              if (name === 'Accuracy') {
                return `${value}%`
              }
              return value
            }}
            cursor={{ strokeDasharray: '3 3' }}
          />
          <XAxis type="number" dataKey="a" name="Trial" domain={['dataMin', 'dataMax']} />
          <YAxis type="number" dataKey="b" name="Accuracy" domain={[0, 100]} />
          <Scatter name="Accuracy" data={data} fill="#82ca9d" line shape="circle" />
        </ScatterChart>
      </div>
    </div>
  )
}

export default AccuracyGraph
