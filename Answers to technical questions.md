

## Technical questions**

Please answer the following questions in a markdown file called `Answers to technical questions.md`.

- How long did you spend on the coding test? 
Ans: around 2.30 hours.
- What would you add to your solution if you had more time? 
Ans: 	Yes there are couple of things that I would like to do further given more time
		1. like removing the data reader in ClientRepository
		2. Using AutoMapper and introducing proper viewModel, Dto for user inputs and transformation of that to Entities.
		3. I would have structured and placed the files properly in folders
		4. I would have used Data Annotation in a request object to make validation like for firstname, surname, email, dob  intrinsic to Models.
		5. I would have used logging and exception handling correctly. 
		6. I would have implemented the repository pattern for Database access here. UserDataAccess class is marked as static which is not a good practice. 
		7. I would have written test cases for Services also. 
-  
- If you didn't spend much time on the coding test then use this as an opportunity to explain what you would add.
- What was the most useful feature that was added to the latest version of C#? 
- Ans:           
-          With C# 12 launched there are several new features got introduced and one of them is Primary Constructor. Earlier primary constructor was limited to only record Type but now we can use it in any class or struct.  
- Please include a snippet of code that shows how you've used it.
    				
	        public class User(int id, string name)
            {
               public int Id => id;
               public string Name => name.Trim();
            }
- How would you track down a performance issue in production? 
-Ans: 		Here are some steps I would like to follows 

		1) Understand what constitutes poor performance for our application. It could be slow response times, high CPU or memory usage, excessive database queries, etc.
		2) Monitor system metrics such as CPU usage, memory usage, disk I/O, network usage, and response times.
		3) Utilize performance monitoring tools like Application Performance Management (APM) solutions (e.g., New Relic, AppDynamics, Dynatrace), which provide insights into application performance, including response times, error rates, and resource consumption. we can also use Use built-in performance monitoring tools in our hosting environment, such as Azure Application Insights or AWS CloudWatch.
		4) Perform code profiling to identify performance bottlenecks. Profilers like dotTrace, ANTS Performance Profiler, or Visual Studio Profiler can help pinpoint areas of code that are consuming the most resources or taking the longest to execute.
		Analyze database queries using tools like SQL Server Profiler or Entity Framework Profiler to identify inefficient queries.
		5) Implement comprehensive logging and tracing in our application to capture detailed information about the execution flow, including timestamps, method calls, and any relevant contextual data. Using structured logging frameworks like Serilog or Microsoft.Extensions.Logging to log important events and performance metrics.
		6) Reviewing our codebase for common performance issues such as excessive object allocations, inefficient algorithms, and unnecessary blocking operations. Check external dependencies (libraries, APIs, databases) for performance issues or potential bottlenecks.
		7) Conduct load testing to simulate real-world traffic and identify how the application behaves under different levels of load.
		Tools like Apache JMeter, Gatling, or Visual Studio Load Test can help automate load testing scenarios.
		8) Once we've identified performance bottlenecks, optimize critical sections of code, database queries, or configurations.
		Consider caching strategies (e.g., in-memory caching, distributed caching) to reduce database or API calls.
		9) Optimize client-side resources such as images, scripts, and stylesheets to improve page load times.
		Continuous Monitoring and Optimization:

		10) Evaluate the scalability of our application architecture to ensure it can handle increasing traffic and load.
		Implement horizontal scaling (adding more instances) or vertical scaling (upgrading hardware) based on performance requirements.
- Have you ever had to do this?
Ans: Yes
- How would you track down an issue with this in production, assuming this api would be deployed as an app service in Azure.
Ans: We will use logging and azure app insight to understand the root cause and monitoring system resources or looking at event logs could be also helpful. 
