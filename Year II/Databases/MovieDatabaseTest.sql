
-- This query returns everything from the fund application table. When ran, it will show threeo values. In the first two the PC_ID will be the same (0) but the Fund_ID will be 0 and 1 meaning that PC_ID 0 has two funds.
-- The third value shows a PC_ID of 1 and a Fund_ID of 1. PC_ID 0 also has Fund_ID 1 which means production companys with the ID 0 and 1 share the grant with Fund_ID 0
-- This shows that a production company has multiple grants and a grant can be shared between multiple companies which meets the first requirement.
select *
from Fund_Application;

-- This query here will show that Mike Tyson has two phone numbers. One for work and one personal which meets the second requirement
-- I did an INNER JOIN operation with the telephone number and employee contact to be able to get the number and description for the first and last name from the employee table.
select e.First_Name, e.Last_Name, t.Number, t.Description
from Employee e, Employee_Contact c INNER JOIN Telephone_Number t ON c.T_NumID = t.T_NumID
where e.Employee_ID = c.Employee_ID

-- This query here will show the name of the production company Moviestudio aswell as its share holder ID and government agency ID. The share holder IDs are 0 and 1 but the government
-- agency IDs are 0 which shows that the company is registered once but has two share holders which meets the third requirement.
select _Name,SH_ID,GA_ID
from Production_Company,Share_Holder
where _Name ='MovieStudio';

-- This query will show that crew ID 0 is staring in the movie code 100 as a protagonist and 101 as a cameo.
-- This shows that the crew member with ID 0 is staring in two different roles which meets the fifth requirement.
select c.Crew_ID, r.Movie_Code, r.Description
from Crew c, Role r
where c.Crew_ID = r.Crew_ID;