const mysql = require('mysql');
const express = require('express');
var app = express();

const bodyParser = require('body-parser');
app.use(bodyParser.json());

var mysqlConnection = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: 'mcshiva',
    database: 'students',
    multipleStatements: true
});

mysqlConnection.connect((err) => {
    if (!err)
        console.log('DB connection succeded.');
    else
        console.log('DB connection failed \n Error : ' + JSON.stringify(err, undefined, 2));
});


app.listen(3000, () => console.log('Express server is runnig at port no : 3000'));


app.get('/students', (req, res) => {
    mysqlConnection.query('SELECT * FROM student', (err, rows, fields) => {
        if (!err)
            res.send(rows);
        else
            console.log(err);
    })
});

//Get students
app.get('/students/:id', (req, res) => {
    mysqlConnection.query('SELECT * FROM student WHERE StudentID = ?', [req.params.id], (err, rows, fields) => {
        if (!err)
            res.send(rows);
        else
            console.log(err);
    })
});

//Delete students
app.delete('/students/:id', (req, res) => {
    mysqlConnection.query('DELETE FROM student WHERE StudentID = ?', [req.params.id], (err, rows, fields) => {
        if (!err)
            res.send('Deleted successfully.');
        else
            console.log(err);
    })
});

//Insert students
app.post('/students', (req, res) => {
    let student = req.body;
    var sql = "SET @StudentID = ?;SET @StudentName = ?;SET @Dep = ?;SET  \
    CALL StudentAddOrEdit(@StudentID,@StudentName,@Dep);";
    mysqlConnection.query(sql, [student.StudentID, student.StudentName, student.Dep], (err, rows, fields) => {
        if (!err)
            rows.forEach(element => {
                if(element.constructor == Array)
                res.send('Inserted Student ID : '+element[0].StudentID);
            });
        else
            console.log(err);
    })
});

//Update students
app.put('/students', (req, res) => {
    let emp = req.body;
    var sql = "SET @StudentID = ?;SET @StudentName = ?;SET @Dep = ?;SET \
    CALL StudentAddOrEdit(@StudentID,@StudentName,@Dep);";
    mysqlConnection.query(sql, [student.StudentID, student.StudentName, student.Dep], (err, rows, fields) => {
        if (!err)
            res.send('Updated successfully');
        else
            console.log(err);
    })
});

