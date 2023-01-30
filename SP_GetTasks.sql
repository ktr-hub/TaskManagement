USE [TaskManagementDB]
GO
/****** Object:  StoredProcedure [dbo].[GetTasks]    Script Date: 27-01-2023 7:06:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetTasks]
AS
BEGIN

    SELECT * FROM Task T
order by T.Project_Id, T.Start_Date, T.Employee_Id;

END