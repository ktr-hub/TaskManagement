create procedure GenerateDifferentTasksByProjectId(@Id int)

as 

begin

	select count(Task_Id) as Task_Id, count(Employee_Id) as Employee_Id, t.Task_Description, t.Start_Date, t.Due_Date, t.Project_Id
	from task t 
	group by t.Task_Description, t.Start_Date, t.Due_Date, t.Project_Id
	having t.Project_Id = @id
	order by Start_Date;

end
