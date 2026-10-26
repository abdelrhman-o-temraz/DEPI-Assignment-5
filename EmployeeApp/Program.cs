// Name: Abdelrhman Osama Hamed
// Group: SWD5_S4
// Assignment 5 - OOP Assignment 2 - Part 01
// Purpose: Employee system with HiringDate class, Gender and SecurityLevel enums

using System;

// Section: Enums
// Requirement 3: Gender can only be M or F.
public enum Gender
{
    M,
    F
}

// Requirement 4: SecurityLevel is a flags enum for combined privileges.
[Flags]
public enum SecurityLevel
{
    Guest = 1,
    Developer = 2,
    Secretary = 4,
    DBA = 8
}

// Section: HiringDate
// Requirement 2: HiringDate saves the day, month and year and validates the date.
public class HiringDate
{
    private int day;
    private int month;
    private int year;

    public HiringDate()
    {
        Day = 1;
        Month = 1;
        Year = 2000;
    }

    public HiringDate(int day, int month, int year)
    {
        Day = day;
        Month = month;
        Year = year;
    }

    // Requirement 2: Day must be valid for the chosen month and year.
    public int Day
    {
        get { return day; }
        set
        {
            if (value < 1 || value > 31)
            {
                throw new ArgumentException("Day must be between 1 and 31.");
            }

            int maxDay = DateTime.DaysInMonth(Year, Month);
            if (value > maxDay)
            {
                throw new ArgumentException("Day is not valid for this month.");
            }

            day = value;
        }
    }

    // Requirement 2: Month must be between 1 and 12.
    public int Month
    {
        get { return month; }
        set
        {
            if (value < 1 || value > 12)
            {
                throw new ArgumentException("Month must be between 1 and 12.");
            }

            month = value;
        }
    }

    // Requirement 2: Year must be in a sensible range.
    public int Year
    {
        get { return year; }
        set
        {
            if (value < 1900 || value > 2100)
            {
                throw new ArgumentException("Year must be between 1900 and 2100.");
            }

            year = value;
        }
    }

    public override string ToString()
    {
        return day + "/" + month + "/" + year;
    }
}

// Section: Employee
// Requirement 1: Employee stores all required personal and job data.
public class Employee
{
    private int id;
    private string name;
    private SecurityLevel securityLevel;
    private decimal salary;
    private HiringDate hireDate;
    private Gender gender;

    public Employee()
    {
        ID = 0;
        Name = "Unknown";
        SecurityLevel = SecurityLevel.Guest;
        Salary = 0m;
        HireDate = new HiringDate();
        Gender = Gender.M;
    }

    public Employee(int id, string name, SecurityLevel securityLevel, decimal salary, HiringDate hireDate, Gender gender)
    {
        ID = id;
        Name = name;
        SecurityLevel = securityLevel;
        Salary = salary;
        HireDate = hireDate;
        Gender = gender;
    }

    // Requirement 1: ID cannot be negative.
    public int ID
    {
        get { return id; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("ID cannot be negative.");
            }

            id = value;
        }
    }

    // Requirement 1: Name cannot be empty.
    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            name = value.Trim();
        }
    }

    // Requirement 1: Security level holds access privileges.
    public SecurityLevel SecurityLevel
    {
        get { return securityLevel; }
        set { securityLevel = value; }
    }

    // Requirement 1: Salary cannot be negative.
    public decimal Salary
    {
        get { return salary; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Salary cannot be negative.");
            }

            salary = value;
        }
    }

    // Requirement 1: Hire date cannot be null.
    public HiringDate HireDate
    {
        get { return hireDate; }
        set
        {
            if (value == null)
            {
                throw new ArgumentException("Hire date cannot be null.");
            }

            hireDate = value;
        }
    }

    // Requirement 3: Gender can only be M or F.
    public Gender Gender
    {
        get { return gender; }
        set
        {
            if (value != Gender.M && value != Gender.F)
            {
                throw new ArgumentException("Gender must be M or F.");
            }

            gender = value;
        }
    }

    // Requirement 5: Employee details as a string with money format.
    public override string ToString()
    {
        return "ID: " + ID +
               ", Name: " + Name +
               ", Security Level: " + SecurityLevel +
               ", Salary: " + String.Format("{0:C}", Salary) +
               ", Hire Date: " + HireDate +
               ", Gender: " + Gender;
    }
}

// Section: Main
// Requirement 6: Read three employees and show them.
public class Program
{
    static void Main(string[] args)
    {
        Employee[] EmpArr = new Employee[3];

        Console.WriteLine("Enter employee 1 data:");
        EmpArr[0] = new Employee(
            ReadInt("ID: "),
            ReadName("Name: "),
            SecurityLevel.DBA,
            ReadDecimal("Salary: "),
            ReadHiringDate("Hire Date:"),
            ReadGender("Gender (M/F): ")
        );

        Console.WriteLine();
        Console.WriteLine("Enter employee 2 data:");
        EmpArr[1] = new Employee(
            ReadInt("ID: "),
            ReadName("Name: "),
            SecurityLevel.Guest,
            ReadDecimal("Salary: "),
            ReadHiringDate("Hire Date:"),
            ReadGender("Gender (M/F): ")
        );

        Console.WriteLine();
        Console.WriteLine("Enter employee 3 data:");
        EmpArr[2] = new Employee(
            ReadInt("ID: "),
            ReadName("Name: "),
            SecurityLevel.Guest | SecurityLevel.Developer | SecurityLevel.Secretary | SecurityLevel.DBA,
            ReadDecimal("Salary: "),
            ReadHiringDate("Hire Date:"),
            ReadGender("Gender (M/F): ")
        );

        Console.WriteLine();
        Console.WriteLine("All employees:");
        for (int i = 0; i < EmpArr.Length; i++)
        {
            Console.WriteLine(EmpArr[i].ToString());
        }
    }

    // Requirement 6: Read a valid integer without runtime errors.
    static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (int.TryParse(input, out int value))
            {
                if (value < 0)
                {
                    Console.WriteLine("ID cannot be negative.");
                    continue;
                }

                return value;
            }

            Console.WriteLine("Invalid number. Try again.");
        }
    }

    // Requirement 6: Read a valid decimal salary.
    static decimal ReadDecimal(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (decimal.TryParse(input, out decimal value))
            {
                if (value < 0)
                {
                    Console.WriteLine("Salary cannot be negative.");
                    continue;
                }

                return value;
            }

            Console.WriteLine("Invalid salary. Try again.");
        }
    }

    // Requirement 6: Read a valid non-empty name.
    static string ReadName(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("Name cannot be empty.");
        }
    }

    // Requirement 6: Read a valid gender using enum parsing.
    static Gender ReadGender(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (Enum.TryParse<Gender>(input, true, out Gender value))
            {
                return value;
            }

            Console.WriteLine("Invalid gender. Enter M or F.");
        }
    }

    // Requirement 6: Read a valid hiring date with repeated checks.
    static HiringDate ReadHiringDate(string message)
    {
        Console.WriteLine(message);

        while (true)
        {
            try
            {
                int day = ReadInt("Day: ");
                int month = ReadInt("Month: ");
                int year = ReadInt("Year: ");
                return new HiringDate(day, month, year);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid date. Try again.");
            }
        }
    }
}
