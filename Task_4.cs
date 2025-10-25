using System;
using System.Collections.Generic;

namespace Task_4
{
    class Person
    {
        public string Id;
        public string Name;
        public string Faculty;

        public Person(string id, string name, string faculty)
        {
            Id = id;
            Name = name;
            Faculty = faculty;
        }
    }

    class Doctor : Person
    {
        public string Subject;
        public Doctor(string id, string name, string faculty, string subject)
            : base(id, name, faculty)
        {
            Subject = subject;
        }
    }

    class Student : Person
    {
        public Student(string id, string name, string faculty)
            : base(id, name, faculty) { }
    }

    class Question
    {
        public string Text;
        public string Type;
        public string CorrectAnswer;
        public int Mark;

        public Question(string text, string type, string correctAnswer, int mark)
        {
            Text = text;
            Type = type;
            CorrectAnswer = correctAnswer;
            Mark = mark;
        }
    }

    class Exam
    {
        public string Subject;
        public List<Question> Questions = new List<Question>();

        public Exam(string subject)
        {
            Subject = subject;
        }

        public void AddQuestion(Question q)
        {
            Questions.Add(q);
        }

        public int TakeExam(Student student)
        {
            Console.WriteLine($"\nStarting Exam for {student.Name} in {Subject}\n");
            int total = 0;
            int score = 0;

            for (int i = 0; i < Questions.Count; i++)
            {
                Question q = Questions[i];
                Console.WriteLine($"Q{i + 1}: {q.Text}");
                Console.Write("Your answer: ");
                string answer = Console.ReadLine();

                if (answer.Trim().ToLower() == q.CorrectAnswer.Trim().ToLower())
                {
                    Console.WriteLine("Correct!\n");
                    score += q.Mark;
                }
                else
                {
                    Console.WriteLine($"Wrong. Correct answer: {q.CorrectAnswer}\n");
                }

                total += q.Mark;
            }

            Console.WriteLine($"Final Score: {score}/{total}\n");
            return score;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Simple Examination System ===");

            while (true)
            {
                Console.WriteLine("\n1) Doctor Mode");
                Console.WriteLine("2) Student Mode");
                Console.WriteLine("0) Exit");
                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                if (choice == "0") break;
                else if (choice == "1") DoctorMode();
                else if (choice == "2") StudentMode();
                else Console.WriteLine("Invalid choice.");
            }
        }

        static Exam lastExam = null;

        static void DoctorMode()
        {
            Console.WriteLine("\n--- Doctor Mode ---");
            Console.Write("Doctor ID: ");
            string id = Console.ReadLine();
            Console.Write("Doctor Name: ");
            string name = Console.ReadLine();
            Console.Write("Faculty: ");
            string faculty = Console.ReadLine();
            Console.Write("Subject: ");
            string subject = Console.ReadLine();

            Doctor dr = new Doctor(id, name, faculty, subject);
            Exam exam = new Exam(subject);

            Console.Write("How many questions? ");
            int count = int.Parse(Console.ReadLine());

            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine($"\nEnter question #{i}:");
                Console.Write("Question text: ");
                string text = Console.ReadLine();

                Console.Write("Type (T/F, Choice, Complete): ");
                string type = Console.ReadLine();

                Console.Write("Correct answer: ");
                string correct = Console.ReadLine();

                Console.Write("Mark: ");
                int mark = int.Parse(Console.ReadLine());

                Question q = new Question(text, type, correct, mark);
                exam.AddQuestion(q);
            }

            lastExam = exam;
            Console.WriteLine("\nExam created successfully!");

            Console.WriteLine("\n--- Switch to Student Mode ---");
            StudentMode();
        }

        static void StudentMode()
        {
            if (lastExam == null)
            {
                Console.WriteLine("\nNo exam available yet. Ask doctor to create one first.");
                return;
            }

            Console.WriteLine("\n--- Student Mode ---");
            Console.Write("Student ID: ");
            string id = Console.ReadLine();
            Console.Write("Student Name: ");
            string name = Console.ReadLine();
            string faculty = lastExam.Subject; 
            Student st = new Student(id, name, faculty);

            lastExam.TakeExam(st);
        }
    }
}