CREATE PROCEDURE GetTasks
AS
BEGIN

    SELECT T.* FROM Project P 
inner join Task T on P.Project_Id = T.Project_Id
inner join Employee E on E.Employee_Id = T.Employee_id
order by T.Project_Id, T.Task_id, T.Employee_Id;

END