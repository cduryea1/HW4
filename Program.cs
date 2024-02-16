//MIS 3033
//HW Student DB Manipulations
//Camille Duryea
//113529005

using c;
using Microsoft.VisualBasic;
using System.Globalization;

Console.WriteLine("HW DB");

string MenuStr = @"
*************************************************************
1.Add a new student’s information
2. Show all the students’ information
3. Show one student’s information for a given Student ID
4. Edit one student’s information for a given Student ID
5. Delete one student’s information for a given Student ID
6. Show the student’s information with highest grade
7. Show the average grade of all the students
Press other keys to exit
*************************************************************
";

StudentDB db;
db = new StudentDB();//

while (true)
{
    Console.WriteLine(MenuStr);

    Console.Write("Input a choice: ");
    string userChoiceStr;
    userChoiceStr = Console.ReadLine();

    if (userChoiceStr == "1")

    {
        //1.Add a new student’s information
        Console.WriteLine("Add a students's information");
        Console.Write("Enter ID: ");
        string idStr = Console.ReadLine();
        Console.Write("Enter Name: ");
        string nameStr = Console.ReadLine();
        Console.Write("Enter Grade: ");
        double gradeDbl;
        string gradeStr = Console.ReadLine();
        gradeDbl = Convert.ToDouble(gradeStr);

        //create a new student in RAM
        Student stu = new Student();
        stu.ID = idStr;
        stu.Name = nameStr;
        stu.Grade = gradeDbl;

        stu.GradeLevel = stu.GetGradeLevel();



        Console.WriteLine(stu);

        //add to the student db table
        db.students.Add(stu);

        //save to the database file, persist
        db.SaveChanges();

        Console.WriteLine("New student info is being added to DB!");
    }
    else if (userChoiceStr == "2")
    {// 2.Show all the students’ information
        Console.WriteLine("All student's Info:");
        List<Student> stuList = db.students.ToList();

        for (int i = 0; i < stuList.Count; i++)
        {
            Console.WriteLine(stuList[i]);
        }

    }
    else if (userChoiceStr == "3")
    {//3. Show one student’s information for a given Student ID
        Console.WriteLine("Show one student's info for a given Studen ID: ");

        Console.Write("Enter ID: ");
        string idStr = Console.ReadLine();

        //query
        List<Student> stuList = db.students.ToList();

        //where always result in a table (collection)
        Student stu = stuList.Where(s => s.ID.ToLower() == idStr.ToLower()).FirstOrDefault();
        if (stu != null)
        {
            Console.WriteLine(stu);
        }
        else
        {
            Console.WriteLine($"ID {idStr} does not exist in the DB!");
        }
    }
    else if (userChoiceStr == "4")
    {//4. Edit one student’s information for a given Student ID
        Console.WriteLine("Edit one student's info for a given student ID: ");
        Console.Write("Enter ID: ");
        string idStr = Console.ReadLine();

        Student stu = db.students.ToList().Where(x => x.ID.ToLower() == idStr.ToLower()).FirstOrDefault();

        if (stu != null)
        {
            Console.WriteLine("The current info:  ");
            Console.WriteLine(stu);


            Console.Write("Enter new name: ");
            string nameStr = Console.ReadLine();

            Console.Write("Enter new grade: ");
            string gradeStr = Console.ReadLine();
            double gradeDbl = Convert.ToDouble(gradeStr);

            stu.Name = nameStr;
            stu.Grade = gradeDbl;

            stu.GradeLevel = stu.GetGradeLevel();

            //save changes to DB
            db.SaveChanges();

            Console.WriteLine("Saved edition successfully!");
            Console.WriteLine("New info is: ");
            Console.WriteLine(stu);
        }
        else
        {
            Console.WriteLine($"ID {idStr} does not exist in the DB!");
        }

    }
    else if (userChoiceStr == "5")
    {//5. Delete one student’s information for a given Student ID
        Console.WriteLine("Delete one student's information for a given Student ID: ");
        Console.Write("Enter ID: ");
        string idStr = Console.ReadLine();

        Student stu = db.students.Where(x => x.ID.ToLower() == idStr.ToLower()).FirstOrDefault();

        if (stu != null)
        {
            //delete from DB
            db.students.Remove(stu); //remove from the RAM

            db.SaveChanges(); //save to the db file
            Console.WriteLine("Student deleted successfully: ");
            Console.WriteLine("The deleted info is: ");
            Console.WriteLine(stu);
            //when we remove, we remove the student id from the collection
            //it is like you drop a coures
        }
        else
        {
            Console.WriteLine($"ID {idStr} does not exist in the DB!");
        }
    }

    else if (userChoiceStr == "6")
    {//6. Show the student’s information with highest grade
        Console.WriteLine("Show the student's info with the highest grade: ");
        Console.WriteLine("The student with the highest grade: ");

        Student stu = db.students.ToList().MaxBy(x => x.Grade);

        Console.WriteLine(stu);

    }
    else if (userChoiceStr == "7")
    {//7. Show the average grade of all the students
        Console.WriteLine("Show the average grade of all students");
        double ave = db.students.ToList().Average(x=>x.Grade);

        Console.WriteLine($"The average grade is: {ave:F2}");

    }
    else
    {
        Console.WriteLine("Thank you and goodbye!");
        break;
    }
}