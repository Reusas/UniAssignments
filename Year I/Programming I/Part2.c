#include<stdio.h>
#include<stdbool.h>


// function to calculate tax. the tax is the amount times the tax rate
float calculate_tax(float amount, float tax_rate)
{
	float calculatedTax;
	calculatedTax= amount * tax_rate;
	return calculatedTax; // this function will return this value
}

//function to calculate netpay. The netpay is the amount minus the tax
float calculate_netpay(float amount, float tax)
{
	float calculatedNetpay;
	calculatedNetpay = amount - tax;
	return calculatedNetpay;
}

// function to incremet salary. The incremented salry is the default salary times the increment rate precentage. I add the 1 because 1 is 100%. 
float increment_salary(float amount, float increment_rate)
{
	float calculatedSalary;
	calculatedSalary = amount * (1 + increment_rate);
	return calculatedSalary;
}


int main()
{
	// Declaring variables that i will use
	int ID;
	float salary;
	float tax;
	float netPay;
	char grade;
	float increment;
	char continueIndex;
	// this integer will be used when i want to loop specific parts of the code. I will set it to 1 at specific times to loop or 0 to not
	int loopIndex; 

	// THis is the main program loop. while true is always true thus the code will loop.
	while (true) 
	{


		//Asking and  getting employee ID
		printf("Enter Employee ID: ");
		scanf_s("%d", &ID);


		// i set the loopIndex to 1 because i want to loop the following code
		loopIndex = 1; 

		while (loopIndex == 1)
		{

			//Asking and  getting salary
			printf("Enter Salary: ");
			scanf_s("%f", &salary);

			// Checking that the salary is not below 12000 or above 15000 

			if (salary < 12000 || salary> 15000)
			{
				// If its not the right value the code will ask the user to try again and loop this part
				printf("Error: salary %f  is out of range. Try again\n", salary); 
				
			}
			else
			{
				// if the value is correct the loopindex will be set to 0 and thus the while loop will stop.
				loopIndex = 0; 
			}

		}

		//Displaying some dashes to mark the end of the imput area and the employee payroll. I use multiple \n to make more space
		printf("------------------------------ \n \n \n");
		printf("------- Employee Payroll ----- \n \n");

		//Calculating tax and net pay values using the functions i made above

		tax = calculate_tax(salary, 0.25);
		netPay = calculate_netpay(salary, tax);

		//Displaying the employee ID, salary, tax and net pay

		printf("Employee ID: %d \n", ID);
		printf("Gross salary: %f \n", salary);
		printf("Tax: %f \n", tax);
		printf("Net Pay: %f \n", netPay);

		// Displaying some more dashes to mark the end of the display section

		printf("------------------------------ \n \n \n");

		// Setting another loop
		loopIndex = 1;

		while (loopIndex == 1)
		{
			//Asking to input Grade
			printf("Enter Grade: ");
			scanf_s(" %c", &grade, 1);

			//Assigning increment value based on the grade. I check for both lowercase and uppercase letters because i want them both to work.



			if (grade == 'a' || grade == 'A')
			{
				increment = 0.025; // 2.5 %
				loopIndex = 0;

			}
			else if (grade == 'b' || grade == 'B')
			{
				increment = 0.015; // 1.5 %
				loopIndex = 0;
			}
			else if (grade == 'c' || grade == 'C')
			{
				increment = 0;
				loopIndex = 0;
			}
			else
			{
				//If any other value than A B or C the loop will continue and ask the user to enter the grade again.

				printf("Error: wrong grade entered. Only A, B or C can be entered! Try again.\n");
				
			}

		}

		//Displaying the increment
		printf("The increment is : %f\n", increment);


		printf("Increment and tax over the five years \n");


		int yearIndex = 1;
		// while less then six because i start the yearindex at 1 
		while (yearIndex < 6) 
		{
			salary = increment_salary(salary, increment);
			tax = salary * 0.25f;
			netPay = salary - tax;
			// i insert the yearIndex number here, which is why i started it at 1 since there is no year 0
			printf("Year %d tax: = %f , net pay = %f\n", yearIndex, tax, netPay); 

			yearIndex++;
		}

		// make a space
		printf("\n"); 

		loopIndex = 1;

		while (loopIndex == 1)
		{
		printf("Do you want to continue? (y/n)");
		scanf_s(" %c", &continueIndex, 1);




			if (continueIndex == 'n')
			{
				// this ends the program so the loop will not repeat anymore
				return 0; 
			}
			else if (continueIndex == 'y')
			{
				// this will quit out of this loop and will continue on which will return to the start of the code.
				loopIndex = 0;

			}
			else
			{
				printf("You entered invalid value. Only (y/n) may be entered. Try again.\n"); // This will loop this part of the code and ask the user to enter y or n again.
			}

		}
	}




}


