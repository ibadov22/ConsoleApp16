using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace AdminNamespace
{
    public class Admin
    {
        public Guid ID { get; set; }
        public string Username { get; set; } = "admin";
        public string Password { get; set; } = "admin";
        public static List<string> Posts { get; set; } = new List<string>();
        public static List<string> Notifications { get; set; } = new List<string>();

        public void AdminPanel()
        {
            bool isLoggedIn = false;
            while (!isLoggedIn)
            {
                Console.Clear();
                Console.WriteLine("=== Admin Girişi ===");
                Console.Write("İstifadəçi adını daxil edin: ");
                string inputUsername = Console.ReadLine();

                Console.Write("Şifrəni daxil edin: ");
                string inputPassword = Console.ReadLine();

                if (inputUsername.Equals(Username, StringComparison.OrdinalIgnoreCase) &&
                    inputPassword.Equals(Password, StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                    Console.WriteLine("Giriş uğurla tamamlandı!");
                    isLoggedIn = true;
                }
                else
                {
                    Console.WriteLine("\nİstifadəçi adı və ya şifrə yanlışdır!");
                    Console.WriteLine("Davam etmək üçün Enter düyməsini basın.");
                    Console.ReadKey();
                }
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Admin Paneli ===");
                Console.WriteLine("1. Post əlavə et");
                Console.WriteLine("2. Bildirişlərə bax");
                Console.WriteLine("3. Postlara bax");
                Console.WriteLine("4. Admin panelindən çıx");
                Console.Write("\nSeçiminizi edin (1-4): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("=== Post Əlavə Et ===");
                            Console.WriteLine("0 - Geri qayıt");
                            Console.Write("\nPost haqqında: ");
                            string post = Console.ReadLine();

                            if (post == "0")
                            {
                                break;
                            }

                            if (string.IsNullOrWhiteSpace(post))
                            {
                                Console.WriteLine("Post boş ola bilməz!");
                                Console.WriteLine("Davam etmək üçün Enter düyməsini basın.");
                                Console.ReadKey();
                                continue;
                            }

                            Posts.Add(post);

                            var options = new JsonSerializerOptions
                            {
                                WriteIndented = true
                            };

                            var postsJson = JsonSerializer.Serialize(Posts, options);
                            File.WriteAllText(@"C:\Users\ibado_lm24\Documents\ConsoleApp16\ConsoleApp16\posts.json", postsJson);

                            Console.WriteLine("Post əlavə olundu!");
                            Console.WriteLine("Davam etmək üçün Enter düyməsini basın.");
                            Console.ReadKey();
                        }
                        break;

                    case "2":
                        Console.WriteLine("\nBildirişlər:");
                        if (Notifications.Count > 0)
                            Notifications.ForEach(Console.WriteLine);
                        else
                            Console.WriteLine("Bildiriş yoxdur.");
                        Console.WriteLine("Davam etmək üçün Enter düyməsini basın.");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("=== Postlar ===");

                        var options2 = new JsonSerializerOptions
                        {
                            WriteIndented = true
                        };

                        var readData = File.ReadAllText(@"C:\Users\ibado_lm24\Documents\ConsoleApp16\ConsoleApp16\posts.json");
                        Posts = JsonSerializer.Deserialize<List<string>>(readData, options2) ?? new List<string>();

                        if (Posts.Count > 0)
                        {
                            for (int i = 0; i < Posts.Count; i++)
                            {
