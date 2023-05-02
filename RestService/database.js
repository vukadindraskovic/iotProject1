const mysql = require('mysql2');

module.exports = mysql.createConnection({
    host: 'localhost',
    user: 'vuci',
    password: 'vuci',
    database: 'iot1db'
});