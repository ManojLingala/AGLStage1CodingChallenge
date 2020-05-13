# AGLStage1CodingChallenge

##  1. .NET Core API

This application connects with external API '[http://agl-developer-test.azurewebsites.net/people.json](http://agl-developer-test.azurewebsites.net/people.json) to get list of owners with pets. Then it transforms data (using LINQ) and returns JSON response with gender of owners with list of cat names in alphabetical order.

##### Output JSON Response:
[{"ownerGender":"Male","catNames":["Garfield","Jim","Max","Tom"]},{"ownerGender":"Female","catNames":["Garfield","Simba","Tabby"]}]


To run this application on local machine:

 - Clone this solution and run using VS 2019.
 - Application configured to be running on - [http://localhost:63264](http://http://localhost:63264) 
 - Navigate to URL to get JSON response with gender of owners with list of cat names   - [http://localhost:63264/api/pets](http://http://localhost:63264/api/pets)
 - Xunit is used for testing.

 ## Acknowledgments

- The people of AGL and the  PeopleBank Recruitment Consultancy  for the opportunity.

