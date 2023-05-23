using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.IO;

namespace UniqPassword
{
    public class UniqPassword
    {
        public static string GenPas(int size)
        {
            string str = "", pas = "";
            for (int i = 33; i < 126; i++)
            {
                str += (char)i;
            }
            var random = new Random();
            for (int i = 0; i < size; i++)
            {
                int randomNumber = random.Next(1, 100);
                pas += str[randomNumber % str.Length];
            }
            return pas;
        }
    }

    public class Write
    {
        public void GenerateAndWritePasswords(int number_of_passwords, string filePath)
        {
            string passwordForUser = "";

            for (int i = 0; i < number_of_passwords; i++)
            {
                passwordForUser += $"{i + 1})" + UniqPassword.GenPas(10) + "\n";
                Thread.Sleep(100);
            }

            File.WriteAllText(filePath, passwordForUser);
        }

        public void WriteToFile(int number_of_users, int number_of_passwords)
        {
            Console.WriteLine("Ждите, идёт процесс генерации...");
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            List<Thread> threads = new List<Thread>();

            for (int j = 0; j < number_of_users; j++)
            {
                string filePath = $"{path}\\Пользователь {j + 1}.txt";

                Thread thread = new Thread(() => GenerateAndWritePasswords(number_of_passwords, filePath));
                threads.Add(thread);
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine($"Пароли сгенерированы и записаны в файлы, путь к файлам: {path}\\Пользователь XXX.txt");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Write write = new Write();
            Console.Write("Для какого количества пользователей нужно сгенерировать пароли: ");
            int num_user = int.Parse(Console.ReadLine());
            Console.Write("По сколько паролей для каждого пользователя сгенерировать: ");
            int num_pas = int.Parse(Console.ReadLine());
            write.WriteToFile(num_user, num_pas);
        }
    }
}
