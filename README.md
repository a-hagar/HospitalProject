# HospitalProject
A Hospital Project for HTTP5204
-remember to create app_data folder when cloning the project

**Smital Christian**
> Date 29-07-2021
- Create Models such as User, Patient ,PatientLog and Feedback 
- Perform Migration and database update


**Ahmed Hagar**
- Wrote the Models for Users, Jobs, and Submissions
- Created & updated the database with Code-First Migrations
- Made the Data Controllers for handling requests regarding job postings and applications
- Created Views to read, update, add, and delete job postings and applicant submissions
- TinyMCE editor for writing job description
- Created API Documentation to list all APIs used for the website features


**Navaneeth Ashok**
- Added for IdentityUsers, Volunteers, Department
- Created EF Migrations for the same
- Datacontrollers and Actioncontrollers created for Volunteer MVP
- ***DataFlow***
1. User registers an account with the portal using Identity registration.
1. The extended IdentityUser will hold extra details of user like DOB, Blood Group, etc
1. The logged-in user will access https://localhost:44361/Volunteers/Join for joining as a volunteer, the following view will be shown. ( A pre-requisite is that departments table should contain some department entries)
1. Volunteers can select the department they want to volunteer for by clicking on the checkboxes. Once they Apply, they'll be redirected to the Details page showing the current status of application.
1. By default the status will be set as PendingApproval which the admin will be able to change (I'll use user role to validate it)
1. https://localhost:44361/Volunteers/List Admin can see all the volunteer request from this page and can click on the Edit link to select a particular request.
1. A drop-down selector is provided here, and the admin can update the status of each department from this page.
1. Due to lack of time, I couldn't move all the data processing code to data controller. so some of my action controller is fetching and processing data from DB. I'm migrating it one at a time, and I'll ensure all the data access is done by the datacontrollers on the final submission.


**ERIC WICKHAM**
- created models for Donor/Donation Feature, linked with ASPNET User Table
- basic crud functionality implemented for feature. 
- Donation table functions to record behaviour of every site user, logs amount, time of donation and the identity of the user
- due to build issues couldn't test functionality of features


**Haroon Shaffiulla**
- Created models and viewmodels for Doctor's Details and linked with Department
- Created & updated the database with Code-First Migrations
- CRUD functionality for DoctorDetails
- Controllers and datacontrollers for both department and Doctorsdetails
- Added role based authentication
