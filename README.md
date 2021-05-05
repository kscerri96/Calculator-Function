# Calculator Function

The calculator solution consists of 2 projects:  
* A function project: Defines an HttpTrigger function called Calculator. Written in .Net Core 3.1, all calculations are done within this project. 
* Unit Tests project to test the defined function.

A set of instructions will be read from a file (Instructions.txt), the function will calculate the result without taking into consideration the mathematical precedence. The result will be displayed upon success. Each instruction within the file is separated by a new line.

The set of instructions include the keyword which is the mathematical operation and a number to be applied to the operation. The last instruction within the file is the apply, the calculator will be initialised with that number and the previous instructions are applied to it.

Instructions from file:

add 2  
multiply 3  
apply 3

Working Method:

3 + 2 * 3 = 15

The result will then be displayed in the following format, with a status code of 200 OK: 

{
    "Result": 15.0
}

* Should an error occur a message displaying 'An error has occurred will be shown with a status code of 400.
* If the instruction file is not found or it is empty the following message will be displayed 'Instruction File is empty or not found' and the status code is set to 400.
* If the keyword within the instruction file is not valid the following message will be displayed 'Keyword in instruction file is not valid.' and the status code is set to 400.

## Unit Tests

The solution includes the following Unit Tests.

* InvalidRequest_InstructionFileEmpty
* InvlidRequest_KeyWordNotValid
* HandleCapitalKeyword
* ResultsasExpected
* HandleDevideBy0
* HandleInstructionWithNoNumber
* InvalidRequest_InstructionFileNotFound
* ValidRequest
* HandleExtraSapceBetweenKeyworAndNumber
* ApplyKeywordMissing
