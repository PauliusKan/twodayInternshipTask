# twodayInternshipTask
 
The database used in this project is SQLite, it is named Zoo.db and can be found in the project folder "twoday_Internship_Task". For easier testing of the application, it only contains the required tables without any data.


I ended up not finishing writing the unit tests for the service classes, because to test them, I would need to mock DbContext to return the required data, and to mock it I would need to create a wrapper class for it, which would require some major code refactoring.