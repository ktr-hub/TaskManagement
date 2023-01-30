/****** Script for Inserting rows ******/


INSERT INTO [TaskManagementDB].[dbo].[Employee](MID, Employee_Name) VALUES ('M1001075','Rahul Dev');
INSERT INTO [TaskManagementDB].[dbo].[Employee](MID, Employee_Name) VALUES ('M1001089','Apoorva R');
INSERT INTO [TaskManagementDB].[dbo].[Employee](MID, Employee_Name) VALUES ('M1001099','Anjan G');
INSERT INTO [TaskManagementDB].[dbo].[Employee](MID, Employee_Name) VALUES ('M1011049','Bhusan S');
INSERT INTO [TaskManagementDB].[dbo].[Employee](MID, Employee_Name) VALUES ('M1011029','Bantu');



/****** Script for SelectTopNRows ******/
SELECT TOP (10) [Employee_ID]
      ,[MID]
      ,[Employee_Name]
  FROM [TaskManagementDB].[dbo].[Employee]