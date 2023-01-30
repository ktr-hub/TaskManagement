$(document).ready(function () {
    // code to fetch data and initialize dynamic dropdown values
    fetch('/tasks/GetProjectsData')
        .then(response => response.json())
        .then(data => {

            var projectSelect = document.getElementById("project-select");
            var option = document.createElement("option");
            option.value = 0;
            option.text = "All Projects";
            projectSelect.add(option);

            data.forEach(project => {
                option = document.createElement("option");
                option.value = project["projectId"]
                option.text = project["projectName"];
                projectSelect.add(option);
            });
        })
        .catch(error => console.error(error));
});


function generateProjectData(id) {

    $(document).ready(function () {
    console.log("Fetching GetProjectsDataById");

    // Initialize the data from the controller
    fetch('/tasks/GetProjectById?id=' + id)
        .then(response => response.json())
        .then(data => {

            var tableProject = document.getElementById("task-project-select");
            var bodyProject = document.createElement("tbody");
            var rowProject = document.createElement("tr");
            var td1 = document.createElement("td");
            td1.innerText = "Project Name : ";
            rowProject.appendChild(td1);

            data.forEach(task => {
                var td2 = document.createElement("td");
                td2.innerHTML = task["projectName"];
                rowProject.appendChild(td2);
                rowProject.style.fontWeight = "bold";
            });
            generateTaskDescriptions(id, tableProject, rowProject, bodyProject);
        })
            .catch(error => console.error(error));
    });


}


function generateTaskDescriptions(id, tableProject, rowProject, bodyProject) {

    $(document).ready(function () {
        console.log("Fetching GenerateDifferentTasksByProjectId");

        // Initialize the data from the controller
        fetch('/tasks/GenerateDifferentTasksByProjectId?id=' + id)
            .then(response => response.json())
            .then(data => {
                console.log(data);
                if (data.length > 0) {

                    bodyProject.appendChild(rowProject);
                    tableProject.appendChild(bodyProject);

                    data.forEach(task => {

                        var row1 = document.createElement("tr");
                        var td1 = document.createElement("td");
                        td1.innerText = "Task Description : ";

                        var td2 = document.createElement("td");
                        td2.innerHTML = task["taskDescription"];
                        row1.appendChild(td1);
                        row1.appendChild(td2);

                        var row2 = document.createElement("tr");
                        td1 = document.createElement("td");
                        td1.innerText = "Task Start Date : ";

                        td2 = document.createElement("td");
                        td2.innerHTML = new Date(task["startDate"]).toISOString().slice(0, 10);
                        row2.appendChild(td1);
                        row2.appendChild(td2);

                        var row3 = document.createElement("tr");
                        td1 = document.createElement("td");
                        td1.innerText = "Task End Date : ";

                        td2 = document.createElement("td");
                        td2.innerText = new Date(task["dueDate"]).toISOString().slice(0, 10);
                        row3.appendChild(td1);
                        row3.appendChild(td2);

                        bodyProject.appendChild(row1);
                        bodyProject.appendChild(row2);
                        bodyProject.appendChild(row3);

                        //
                        var employeeTable = document.createElement("table");
                        var employeeHeader = document.createElement("thead");
                        var employeeBody = document.createElement("tbody");

                        ["MID", "EmployeeName"].forEach(item => {
                            var th = document.createElement("th");
                            th.innerText = item;
                            employeeHeader.appendChild(th);
                        });

                        //employee values

                        employeeBody.appendChild(employeeHeader);
                        employeeTable.appendChild(employeeBody);
                        //

                        tableProject.appendChild(bodyProject);
                        tableProject.appendChild(employeeTable);
                    });
                    }
                else {
                    //data not found
                    console.log("No assigned tasks in the projects");
                }
            })
            .catch(error => console.error(error));
    });
}

function getTasksData(id) {
    generateProjectData(id);
}






            /*
             * function createTaskTable(data) {
    var table = document.createElement("table");
    var thead = document.createElement("thead");
    var tbody = document.createElement("tbody");
    var headRow = document.createElement("tr");
    ["Name", "Description", "Due Date"].forEach(value => {
        var th = document.createElement("th");
        th.innerText = value;
        headRow.appendChild(th);
    });
    thead.appendChild(headRow);
    table.appendChild(thead);
    data.forEach(task => {
        var row = document.createElement("tr");
        ["Name", "Description", "DueDate"].forEach(key => {
            var td = document.createElement("td");
            td.innerText = task[key];
            row.appendChild(td);
        });
        tbody.appendChild(row);
    });
    table.appendChild(tbody);
    document.body.appendChild(table);
}

             * 
             * 

                // Initialize the data from the controller
            fetch('/tasks/GetTasksData')
                .then(response => response.json())
                .then(data => {
                    // Initialize the dynamic dropdown

                    var taskSelect = document.getElementById("task-project-select");
                    data.forEach(task => {
                        console.log(data);
                        var option = document.createElement("td");
                        option.value = task["taskId"]
                        option.text = task["taskName"];
                        taskSelect.add(option);
                    });
                })
                .catch(error => console.error(error));
                
        }*/

        /*
        funciton onChange(id){

            console.log("dropdown value selected onChange() " + id);
            console.log("fetching /tasks/viewtasks/"+id);

            //document.getElementById("projects-view").innerHTML = dropdown.value;

            fetch('/tasks/viewtasks/'+id, { method: 'GET' })
                        .then(response => {
                            console.log("Response in fetch");
                            if (!response.ok) {
                                throw new Error(response.statusText);
                            }

                    console.log("Return Response in fetch" + response);
                    //console.log("Return Response in fetch" + response.json());
                            return response.json();
                        })
                        .then(data => {
                            // do something with the data, for example:
                            var tasksList = document.getElementById("projects-view");
                            tasksList.innerHTML = "";
                            data.forEach(project => {
                                tasksList.innerHTML += `<li>${project.TaskDescription}</li>`;
                            });
                            
                            console.log("Return Response in data");
                        })
                        .catch(error => console.error(error));

                        /*
                fetch('/tasks/viewtasks/${projectId}', 
                {
                    method: 'GET',
                    body: JSON.stringify({
                        id: selectedValue,
                            viewModel: modelJS
                        })
                }).then
                (response => 
                {
                    // Handle the response here
                });
        }
        */