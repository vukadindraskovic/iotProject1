const express = require('express');
const db = require('./database.js');

const app = express();
app.use(express.json());
const port = 3000;

app.get('/SensorValue', async (req, res) => {
  const results = await db.promise().query('SELECT * FROM SENSOR_VALUES');
  res.status(200).send(results[0]);
})

app.get('/SensorValue/:id', async (req, res) => {
  const results = await db.promise().query(`SELECT * FROM SENSOR_VALUES WHERE ID = '${req.params.id}'`);
  res.status(200).send(results[0]);
})

app.post('/SensorValue', async (req, res) => {
  try {
    const {id, roomId, temp, outIn} = req.body;
    const date = getDateTimeNow();
    await db.promise().query(`INSERT INTO SENSOR_VALUES(id, room_id, noted_date, temp, outin) VALUES('${id}', '${roomId}', '${date}', ${temp}, '${outIn}')`);
    const results = await db.promise().query(`SELECT * FROM SENSOR_VALUES WHERE ID = '${id}'`);
    res.status(201).send(results[0]);
  }
  catch (err) {
    res.status(400).send(err);
  }
})

app.put('/SensorValue', async (req, res) => {
  try {
    const {id, roomId, temp, outIn} = req.body;
    const date = getDateTimeNow();
    await db.promise().query(`UPDATE SENSOR_VALUES SET room_id='${roomId}', noted_date='${date}', temp='${temp}', outin='${outIn}' WHERE id = '${id}'`);
    const results = await db.promise().query(`SELECT * FROM SENSOR_VALUES WHERE ID = '${id}'`);
    res.status(201).send(results[0]);
  }
  catch (err) {
    res.status(400).send(err);
  }
})

app.delete('/SensorValue/:id', async (req, res) => {
  await db.promise().query(`DELETE FROM SENSOR_VALUES WHERE id = '${req.params.id}'`);
  res.status(200).send({ msg: 'Successfully deleted' });
})

app.listen(port, () => {
  console.log(`Example app listening on port ${port}`)
})

function getDateTimeNow() {
  const date = new Date(Date.now());
  var hours = date.getHours();
  var minutes = date.getMinutes();
  minutes = minutes < 10 ? '0' + minutes : minutes;
  var strTime = hours + ':' + minutes + ':00';
  return (date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate() + "  " + strTime);
}