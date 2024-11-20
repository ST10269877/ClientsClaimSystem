CREATE DATABASE QUESTIONPAPER22;

USE QUESTIONPAPER22;


CREATE TABLE MODULES (
    MODULE_CODE VARCHAR(10) NOT NULL PRIMARY KEY,
    MODULE_NAME VARCHAR(40) NOT NULL
);

CREATE TABLE STUDENTS (
    STUDENT_NUMBER VARCHAR(5) NOT NULL PRIMARY KEY,
    STUDENT_NAME VARCHAR(20) NOT NULL,
    STUDENT_SURNAME VARCHAR(50) NOT NULL,
    DATE_OF_BIRTH SMALLDATETIME NOT NULL
);

CREATE TABLE TEST_RESULTS (
    MODULE_CODE VARCHAR(10) NOT NULL,
    STUDENT_NUMBER VARCHAR(5) NOT NULL,
    TEST_NUMBER SMALLINT NOT NULL,
    TESTMARK SMALLINT NOT NULL,
    PRIMARY KEY (MODULE_CODE, STUDENT_NUMBER, TEST_NUMBER),
    FOREIGN KEY (MODULE_CODE) REFERENCES MODULES(MODULE_CODE),
    FOREIGN KEY (STUDENT_NUMBER) REFERENCES STUDENTS(STUDENT_NUMBER)
);

INSERT INTO MODULES (MODULE_CODE, MODULE_NAME) VALUES
('DATA6212', 'Database Intermediate'),
('INPU221', 'Desktop Publishing'),
('PROG6211', 'Programming 2A'),
('PROG6212', 'Programming 2B'),
('WEDE220', 'Web Development (Intermediate)');

INSERT INTO STUDENTS (STUDENT_NUMBER, STUDENT_NAME, STUDENT_SURNAME, DATE_OF_BIRTH) VALUES
('ST001', 'Dominique', 'Woolridge', '1996-04-19'),
('ST002', 'Nico', 'Baird', '1994-11-19'),
('ST003', 'Derek', 'Moore', '1995-06-24'),
('ST004', 'Neo', 'Petlele', '1996-12-29'),
('ST005', 'Andrew', 'Crouch', '1997-01-30');

INSERT INTO TEST_RESULTS (MODULE_CODE, STUDENT_NUMBER, TEST_NUMBER, TESTMARK) VALUES
('PROG6211', 'ST001', 1, 65),
('WEDE220', 'ST004', 1, 87),
('PROG6211', 'ST001', 2, 68),
('WEDE220', 'ST004', 2, 85),
('INPU221', 'ST005', 1, 39),
('WEDE220', 'ST002', 1, 71),
('WEDE220', 'ST002', 2, 95);

--Create a view named ‘PassWithDistinction’ that contains the Student_Name,
--Student_Surname, Module_Name, Test_Number and TestMark for all students that
--obtained a mark of 75 and more for a specific test.
--2.1
USE QUESTIONPAPER22
GO
CREATE VIEW PassWithDistinction
AS
SELECT 
STUDENTS.STUDENT_NAME,STUDENTS.STUDENT_SURNAME,
MODULES.MODULE_NAME, TEST_RESULTS.TEST_NUMBER,
TEST_RESULTS.TESTMARK
FROM 
    STUDENTS, MODULES, TEST_RESULTS
WHERE 
    STUDENTS.STUDENT_NUMBER = TEST_RESULTS.STUDENT_NUMBER
    AND MODULES.MODULE_CODE = TEST_RESULTS.MODULE_CODE
    AND TEST_RESULTS.TESTMARK >= 75;

	SELECT* FROM PassWithDistinction;

--Create a stored procedure named ‘StudentRecord’ that will display the
--Module_Name, Test_Number, and TestMark of all the tests a specific student has
--written. When executing the stored procedure make use of the Student_Number
--‘ST001’.
--2.2
USE HOSPITALFILE
GO
CREATE PROC StudentRecord
AS
MODULE_NAME,TEST_NUMBER,TESTMARK
FROM  
MODULES,TEST_RESULTS

--Create a query that displays the Module_Code, Module_Name, and whether or not
--any tests have been written for the module. If test results have been captured it must
--display ‘Tests results captured’, or ‘No test results captured’ if no test results have
--been captured for that module. Call the new column ‘Test Result Status’.
--2.3

--Write a query to generate a report indicating the average overall test mark for each
--student. The report should display the Student_Name, Student_Surname, as well as
--the overall average test mark for the student. Arrange the report so that the records
--are ordered in descending order based on the average test mark.
--2.4

--Write a query that will display the Student_Name and Student_Surname of all
--students that have not written any tests.
--2.5