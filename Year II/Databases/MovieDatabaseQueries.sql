-- PART 1 -- CREATING THE DATABASE TABLES


--Create Production Company Table. I limited the varchars to 30 characters as i feel that it will be enough.
-- I used the int value type for numbers but used numeric for the net worth as it could require decimal numbers
create table Production_Company(
PC_ID int primary key,
_Name varchar(30),
_Address varchar(30),
ZIP_Code varchar(30),
City varchar(30),
Country varchar(30),
Company_Type varchar(30),
Employee_Num int,
Net_Worth numeric
);
-- Create Government Authority table. For the date registered i used the datetime value type as it is most fitting here
-- The references part creates a foreign key that references PC_ID which is the primary key in Production_Company
create table Government_Authority(
GA_ID int primary key,
Date_Registered datetime,
Registration_Fee numeric,
PC_ID int NOT NULL references Production_Company(PC_ID)
);

-- Create Telephone Number table. 
create table Telephone_Number(
T_NumID int primary key,
Number int,
Description varchar(30)
);

--Create Share Holder table
create table Share_Holder(
SH_ID int primary key,
Town_Of_Birth varchar(30),
Mothers_Maiden_Name varchar(30),
Fathers_First_Name varchar(30),
National_Insurance_Num int,
Passport_Num int,
GA_ID int NOT NULL references Government_Authority(GA_ID),
T_NumID int NOT NULL references Telephone_Number(T_NumID)
);

--Create Funding table. The deadline is in the datetime value type.
create table Funding(
Fund_ID int primary key,
Grant_Name varchar(30),
Funding_Body varchar(30),
Max_Grant numeric,
Proposal_Deadline datetime
);

-- Create Fund Application table. Since this is a bridge table it needs a composite primary key which is created in the last line.
create table Fund_Application(
PC_ID int NOT NULL references Production_Company(PC_ID),
Fund_ID int NOT NULL references Funding(Fund_ID),
Amount_Requested numeric,
Outcome varchar(30),
primary key(PC_ID,Fund_ID) -- composite primary key
);

-- Create the Employee table. This table will act as a master table for the sub tables like crew and staff.
create table Employee(
Employee_ID int primary key,
First_Name varchar(30),
Last_Name varchar(30),
Middle_Name varchar(30),
Date_Of_Birth datetime,
Starting_Date datetime,
Email_Address varchar(60), -- Email may require more characters
PC_ID int NOT NULL references Production_Company(PC_ID)
);

-- Create Employee Contact table. This is another bridge table with a composite primary key.
-- This table is needed as one of the requirements are that Employees can have multiple phone numbers.
create table Employee_Contact(
Employee_ID int NOT NULL references Employee(Employee_ID),
T_NumID int NOT NULL references Telephone_Number(T_NumID),
primary key(Employee_ID, T_NumID)
);

-- Create deparment table.
create table Department(
Department_ID int primary key,
Department_Type varchar(30),
Building_Name varchar(30),
_Address varchar(30),
);

--Create staff table.
create table Staff(
Staff_ID int primary key,
Monthly_Salary numeric,
Working_Hours varchar(30),
Employee_ID int NOT NULL references Employee(Employee_ID),
Department_ID int NOT NULL references Department(Department_ID),
);




--Create crew table. This is will act as another master table for sub tables like actor, director etc.
create table Crew(
Crew_ID int primary key,
Hourly_Pay numeric,
Pay_Bonus numeric,
Employee_ID int NOT NULL references Employee(Employee_ID),
);

--Create actor table.
create table Actor(
Actor_ID int primary key,
Daily_Pay numeric,
Scene_Bonus numeric,
Crew_ID int NOT NULL references Crew(Crew_ID)
);

--Create director table.
create table Director(
Director_ID int primary key,
Shooting_Bonus numeric,
Crew_ID int NOT NULL references Crew(Crew_ID)
);

--Create film worker table
create table Film_Worker(
Film_Worker_ID int primary key,
_Role varchar(30),
Shooting_Bonus numeric,
Crew_ID int NOT NULL references Crew(Crew_ID)
);

--Create movie table
create table Movie(
Movie_Code int primary key,
Title varchar(30),
_Year datetime,
Release_Date datetime,
PC_ID int NOT NULL references Production_Company(PC_ID)
);

-- Bridge table between Movie and crew.
create table Role(
Crew_ID int NOT NULL references Crew(Crew_ID),
Movie_Code int NOT NULL references Movie(Movie_Code),
Description varchar(30),
primary key(Crew_ID,Movie_Code)
);

-- PART 2 - TESTING IF THE TABLES MEET THE REQUIREMENTS

-- The first requirement is to see if a company can receive multiple grants. To test this i will create a company

-- Here im inserting some made up values into the company. The ID for this company is 0
insert into Production_Company
values(0, 'MovieStudio','5 Grove Road', 'PO8 5FG', 'New York', 'USA', 'Non-Profit', 25, 100000);


-- Here im inserting two values into the funding table. The IDS for these tables are 0 and 1
insert into Funding
values(0,'MovieGrant','Funds Co',50000, '2023-01-01')

insert into Funding
values(1,'MovieGrant','Funds Co',55000, '2023-01-02')

-- Now i insert the ID of the company and the IDS of the funding tables into the fund application.
insert into Fund_Application
values(0,0,50000,'In Progress');

insert into Fund_Application
values(0,1,55000,'In Progress');

-- When i select everything from the fund application you can see that the production company ID 0 has a FUND ID of
-- 0 and 1 meaning that this one company has multiple funds which means the requirement is met. I could add more funds if i wanted to aswell.
select *
from Fund_Application;

-- The second requirement is that an employee can have more than one telephone numbers and descriptions associatied with them

-- To test i first create and Employee
insert into Employee
values(0,'Mike','Tyson','Iron','1978-05-22','2022-12-01','mike88@gmail.com',0);

select *
from Employee;

-- Then i create 2 phone numbers
insert into Telephone_Number
values(0,445875263,'Personal')

insert into Telephone_Number
values(1,445875264,'Work')

-- Now i insert 2 values into employee contact table. The employee ID will remain the same but the numb id will be different
-- meaning that one employee id will have 2 different number ids which is two different numbers and descriptions

insert into Employee_Contact
values(0,0);

insert into Employee_Contact
values(0,1);

-- This query here will show that Mike Tyson has two phone numbers. One for work and one personal which means that this requirement is met.
select e.First_Name, e.Last_Name, t.Number, t.Description
from Employee e, Employee_Contact c INNER JOIN Telephone_Number t ON c.T_NumID = t.T_NumID
where e.Employee_ID = c.Employee_ID



-- Now i will test to see if a production company can have many share hodlers.
-- Since i already have a production company created i wont create another one. Instead i will create 2 share holders


-- Since share holders and production company are linked via government authority i will create a table for government authority
insert into Government_Authority
values(0,'2022-12-01',100,0);

--Each share holder needs a telephone number so i insert those too
insert into Telephone_Number
values(2,445875269,'Work');


insert into Share_Holder
values(0,'London','Ella','Virgil',154896,555248,0,2);

insert into Telephone_Number
values(3,445975269,'Work');


insert into Share_Holder
values(1,'London','Ella','James',154826,425248,0,3);

-- If i run this query to show everything from production company and share holders you will see that there will be SH_ID 0 and 1 for GA_ID 0. Meaning that there
-- are two share holders for this one company which means this requirement is met aswell.
select * from Production_Company,Share_Holder;


-- Now i will test to see if a crew member can be a part of one or more movies

-- First i create a crew member. His employee ID is 0 which is the previously created employee
insert into Crew
values(0,50,500,0);

-- Now i create 2 movies

insert into Movie
values(100,'The big one','2008','2008-01-01',0)

insert into Movie
values(101,'The bigger one','2009','2009-01-01',0)

-- Since movie and crew member are linked by a role i will create two roles. They will have the same crew ID but different movie codes

insert into Role
values(0,100,'Protagonist')

insert into Role
values(0,101,'Cameo')


-- This query will show that crew ID 0 is staring in the movie code 100 as a protagonist and 101 as a cameo. Meaning that the crew member can indeed have a role in different movies meaning this requirement is met.
select c.Crew_ID, r.Movie_Code, r.Description
from Crew c, Role r
where c.Crew_ID = r.Crew_ID;



