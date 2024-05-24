#include<stdio.h>

int main()
{
	// Declaring variables that i will use
	int ID;
	float salary;
	float tax;
	float netPay;
	char grade;
	float increment;

	//Asking and  getting employee ID
	printf("Enter Employee ID: ");
	scanf_s("%d", &ID);

	//Asking and  getting salary
	printf("Enter Salary: ");
	scanf_s("%f", &salary);

	// Checking that the salary is not below 12000 or above 15000 

	if (salary < 12000 || salary> 15000)
	{
		printf("Error: salary %f  is out of range\n", salary);
		return; // This exits out of the program and does not continue the rest of the code
	}

	//Displaying some dashes to mark the end of the imput area and the employee payroll. I use multiple \n to make more space
	printf("------------------------------ \n \n \n");
	printf("------- Employee Payroll ----- \n \n");

	//Calculating tax and net pay values

	tax = salary * 0.25;
	netPay = salary - tax;

	//Displaying the employee ID, salary, tax and net pay

	printf("Employee ID: %d \n", ID);
	printf("Gross salary: %f \n", salary);
	printf("Tax: %f \n", tax);
	printf("Net Pay: %f \n", netPay);

	// Displaying some more dashes to mark the end of the display section

	printf("------------------------------ \n \n \n");

	//Asking to input Grade

	printf("Enter Grade: ");
	scanf_s(" %c", &grade,1);

	//Assigning increment value based on the grade. I check for both lowercase and uppercase letters because i want them both to work.

	if (grade == 'a' || grade == 'A')
	{
		increment = 0.025; // 2.5 %

	}
	else if (grade == 'b' || grade == 'B')
	{
		increment = 0.015; // 1.5 %
	}
	else if(grade == 'c' || grade == 'C')
	{
		increment = 0;
	}
	else
	{
		//If any other value than A B or C is entered the program will stop.

		printf("Error: wrong grade entered. Only A, B or C can be entered!\n");
		return;
	}

	//Displaying the increment
	printf("The increment is : %f\n", increment);

	printf("Increment and tax over the five years \n");

	//Calculating the increased salary and new tax and net pay for year 1
	salary = salary * (1 + increment);
	tax = salary * 0.25f;
	netPay = salary - tax;

	printf("Year 1 tax: = %f , net pay = %f\n", tax, netPay);

	//Calculating the increased salary and new tax and net pay for year 2
	salary = salary * (1 + increment);
	tax = salary * 0.25f;
	netPay = salary - tax;

	printf("Year 2 tax: = %f , net pay = %f\n", tax, netPay);

	//Calculating the increased salary and new tax and net pay for year 3
	salary = salary * (1 + increment);
	tax = salary * 0.25f;
	netPay = salary - tax;

	printf("Year 3 tax: = %f , net pay = %f\n", tax, netPay);

	//Calculating the increased salary and new tax and net pay for year 4
	salary = salary * (1 + increment);
	tax = salary * 0.25f;
	netPay = salary - tax;

	printf("Year 4 tax: = %f , net pay = %f\n", tax, netPay);

	//Calculating the increased salary and new tax and net pay for year 5
	salary = salary * (1 + increment);
	tax = salary * 0.25f;
	netPay = salary - tax;

	printf("Year 5 tax: = %f , net pay = %f\n", tax, netPay);

	
	return 0;


}