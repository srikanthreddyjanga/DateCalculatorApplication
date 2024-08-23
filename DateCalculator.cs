using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DateCalculatorApplication
{
    internal class DateCalculator
    {
        public static void Main()
        {
            int day, month, year;

            // Loop until a valid date in the correct format is entered
            while (true)
            {
                try
                {
                    Console.Write("Enter the date in dd/mm/yyyy format: ");
                    string inputDate = Console.ReadLine();

                    if (IsValidDateFormat(inputDate, out day, out month, out year) && IsValidDate(day, month, year))
                    {
                        break; // Exit the loop if the date format is valid and the date itself is valid
                    }
                    else
                    {
                        Console.WriteLine("Invalid date or format. Please enter the date in dd/mm/yyyy format.");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Input format error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: {ex.Message}");
                }
            }

            // Prompt user to enter the number of days to add
            int daysToAdd;
            while (true)
            {
                try
                {
                    Console.Write("Enter the number of days to add: ");
                    if (int.TryParse(Console.ReadLine(), out daysToAdd))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: {ex.Message}");
                }
            }

            // Add the specified number of days to the date
            var result = AddDays(day, month, year, daysToAdd);

            Console.WriteLine($"New Date: {result.Item1:D2}/{result.Item2:D2}/{result.Item3}");
        }

        /// <summary>
        /// Validates the date format and parses the date into day, month, and year components.
        /// </summary>
        /// <param name="dateInput">The date input string in dd/mm/yyyy format.</param>
        /// <param name="day">The day component parsed from the date input.</param>
        /// <param name="month">The month component parsed from the date input.</param>
        /// <param name="year">The year component parsed from the date input.</param>
        /// <returns>True if the date format is valid, otherwise false.</returns>
        public static bool IsValidDateFormat(string dateInput, out int day, out int month, out int year)
        {
            // Regular expression to match dd/mm/yyyy format
            string pattern = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/(\d{4})$";
            if (Regex.IsMatch(dateInput, pattern))
            {
                string[] dateParts = dateInput.Split('/');
                day = int.Parse(dateParts[0]);
                month = int.Parse(dateParts[1]);
                year = int.Parse(dateParts[2]);
                return true; // Date format is valid
            }

            // Return false if the date format is invalid
            day = month = year = 0;
            return false;
        }

        /// <summary>
        /// Checks if the given day, month, and year form a valid date.
        /// </summary>
        /// <param name="day">The day to validate.</param>
        /// <param name="month">The month to validate.</param>
        /// <param name="year">The year to validate.</param>
        /// <returns>True if the date is valid, otherwise false.</returns>
        public static bool IsValidDate(int day, int month, int year)
        {
            int[] daysInMonth = { 31, IsLeapYear(year) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            // Check if day is valid for the given month and year
            return day > 0 && day <= daysInMonth[month - 1];
        }

        /// <summary>
        /// Adds a specified number of days to a given date.
        /// </summary>
        /// <param name="day">The day component of the date.</param>
        /// <param name="month">The month component of the date.</param>
        /// <param name="year">The year component of the date.</param>
        /// <param name="daysToAdd">The number of days to add to the date.</param>
        /// <returns>A tuple containing the updated day, month, and year after adding the specified number of days.</returns>
        public static Tuple<int, int, int> AddDays(int day, int month, int year, int daysToAdd)
        {
            int[] daysInMonth = { 31, IsLeapYear(year) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            day += daysToAdd;

            // Adjust month and year as the days are added
            while (day > daysInMonth[month - 1])
            {
                day -= daysInMonth[month - 1];
                month++;
                if (month > 12)
                {
                    month = 1;
                    year++;
                    daysInMonth[1] = IsLeapYear(year) ? 29 : 28; // Adjust February if the year changes
                }
            }

            return Tuple.Create(day, month, year);
        }

        /// <summary>
        /// Determines whether a given year is a leap year.
        /// </summary>
        /// <param name="year">The year to check for leap year status.</param>
        /// <returns>True if the year is a leap year, otherwise false.</returns>
        public static bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }
    }
}
