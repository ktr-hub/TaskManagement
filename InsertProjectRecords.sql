/****** Script for Inserting rows ******/

INSERT INTO [TaskManagementDB].[dbo].[Project](Project_Name) VALUES ('ipad');
INSERT INTO [TaskManagementDB].[dbo].[Project](Project_Name) VALUES ('iphone');
INSERT INTO [TaskManagementDB].[dbo].[Project](Project_Name) VALUES ('adecco');

INSERT INTO [TaskManagementDB].[dbo].[Project](Project_Name) VALUES ('boat');
INSERT INTO [TaskManagementDB].[dbo].[Project](Project_Name) VALUES ('cat');
INSERT INTO [TaskManagementDB].[dbo].[Project](Project_Name) VALUES ('Cars24');


/****** Script for SelectTopNRows ******/


SELECT TOP (10) [Project_ID]
      ,[Project_Name]
  FROM [TaskManagementDB].[dbo].[Project]

