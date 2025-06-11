using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using AdminNamespace;

namespace UserNamespace
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> LikedPosts { get; set; } = new List<string>();

        private static List<User> users = new List<User>();

        public void UserSignUp()
        {
            while (true)
            {
                try
                {

                    var options = new JsonSerializerOptions();
                    options.WriteIndented = true;

                    if (File.Exists(@"C:\Users\ibado_lm24\Documents\ConsoleApp16\ConsoleApp16\users.json"))
                    {
                        var readData = File.ReadAllText(@"C:\Users\ibado_lm24\Documents\ConsoleApp16\ConsoleApp16\users.json");
                        users = JsonSerializer.Deserialize<List<User>>(readData, options);
                    }

                    string inputName = "";
                    string inputSurname = "";
                    string inputPassword = "";
                    int inputAge = 0;
                    string inputEmail = "";


                    while (true)
                    {
                        Console.Write("Name daxil edin: ");
                        inputName = Console.ReadLine();
                        if (inputName.Length < 3)
                        {
                            Console.WriteLine("Daxil etdiyin ad cox qisadir");
                            continue;
                        }
                        break;
                    }


                    Console.Write("Surname daxil edin: ");
                    inputSurname = Console.ReadLine();


                    while (true)
                    {
                        Console.Write("Password daxil edin: ");
                        inputPassword = Console.ReadLine();
                        if (inputPassword.Length < 6)
                        {
                            Console.WriteLine("Sifren cox qisa ve sadedir");
                            continue;
                        }
                        break;
                    }


                    while (true)
                    {
                        Console.Write("Age daxil edin: ");
                        string ageInput = Console.ReadLine();
                        if (!int.TryParse(ageInput, out inputAge))
                        {
                            Console.WriteLine("Yas reqem olmalidir !");
                            continue;
                        }
                        if (inputAge < 15)
                        {
                            Console.WriteLine("Yasiniz uygun deyil !");
                            continue;
                        }
                        break;
                    }


                    while (true)
                    {
                        Console.Write("Email daxil edin: ");
                        inputEmail = Console.ReadLine();
                        if (inputEmail.Length < 11)
                        {
                            Console.WriteLine("Mail cox qisadir");
                            continue;
                        }
                        if (inputEmail.Substring(inputEmail.Length - 10, 10) != "@gmail.com")
                        {
                            Console.WriteLine("Mail yalnisdir gmail ile yaz !");
                            continue;
                        }


                        if (users.Exists(u => u.Email == inputEmail))
                        {
                            Console.WriteLine("Bu email artiq qeydiyyatdan kecib !");
                            continue;
                        }
                        break;
                    }


                    User newUser = new User
                    {
                        Name = inputName,
                        Surname = inputSurname,
                        Age = inputAge,
                        Email = inputEmail,
                        Password = inputPassword
                    };

                    users.Add(newUser);

                    var usersJson = JsonSerializer.Serialize(users, options);
                    File.WriteAllText(@"C:\Users\ibado_lm24\Documents\ConsoleApp16\ConsoleApp16\users.json", usersJson);

                    Console.WriteLine("Qeydiyyat tamamlandi !");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Yeniden et ! ");
                }
            }
        }

        public User? UserLogin()
        {
            try
            {
                var options = new JsonSerializerOptions();
                options.WriteIndented = true;

                var readData = File.ReadAllText(@"C:\Users\ibado_lm24\Documents\ConsoleApp16\ConsoleApp16\users.json");
                users = JsonSerializer.Deserialize<List<User>>(readData, options);

                Console.Write("Email daxil et: ");
                string inputEmail = Console.ReadLine();

                Console.Write("Password daxil et: ");
                string inputPassword = Console.ReadLine();

                var user = users.Find(u => u.Email == inputEmail && u.Password == inputPassword);
                if (user != null)
                {
                    Console.WriteLine($"Xos gelmisen, {user.Name} {user.Surname}!");
                    return user;
                }
                else
                {
                    Console.WriteLine("Bele bir istifadeci yoxdur!");
                    Console.WriteLine("Davam etmek ucun enteri bas");
                    Console.ReadKey();
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Davam etmek ucun enteri bas");
                Console.ReadKey();
                return null;
            }
        }

        public void UserPanel(User currentUser)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine($"=== User Panel ===");
                    Console.WriteLine("1. Postlara bax");
                    Console.WriteLine("2.User panelden cix");
                    Console.Write("\nSeciminizi edin (1-2): ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("\nPostlar:");

                                var options = new JsonSerializerOptions();
                                options.WriteIndented = true;

                                var readData = File.ReadAllText(@"C:\Users\ibado_lm24\Documents\ConsoleApp16\ConsoleApp16\posts.json");
                                AdminNamespace.Admin.Posts = JsonSerializer.Deserialize<List<string>>(readData, options);

                                if (AdminNamespace.Admin.Posts.Count > 0)
                                {
                                    for (int i = 0; i < AdminNamespace.Admin.Posts.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {AdminNamespace.Admin.Posts[i]}");
                                    }
                                    Console.WriteLine("\n0 - Geri qayit");
                                    Console.Write("\nBeyenmek istediyin postun nomresini daxil et: ");
                                    if (int.TryParse(Console.ReadLine(), out int postIndex))
                                    {
                                        if (postIndex == 0) break;
                                        if (postIndex > 0 && postIndex <= AdminNamespace.Admin.Posts.Count)
                                        {
                                            string selectedPost = AdminNamespace.Admin.Posts[postIndex - 1];
                                            if (!currentUser.LikedPosts.Contains(selectedPost))
                                            {
                                                currentUser.LikedPosts.Add(selectedPost);
                                                string notification = $"{currentUser.Name} {currentUser.Surname} postu beyendi: {selectedPost}";
                                                AdminNamespace.Admin.Notifications.Add(notification);

                                                var usersJson = JsonSerializer.Serialize(users, options);
                                                File.WriteAllText(@"C:\Users\ibado_lm24\Documents\ConsoleApp16\ConsoleApp16\users.json", usersJson);
                                                Console.WriteLine("Post beyenildi!");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Bu postu artiq beyenmisiniz!");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Yanlis post nomresi!");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Yanlis ! Reqem daxil et");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Post yoxdu");
                                    break;
                                }
                                Console.WriteLine("\nDavam etmek ucun enteri bas");
                                Console.ReadKey();
                            }
                            break;

                        case "2":
                            Console.WriteLine("\nUser panelinden cixildi ");
                            return;

                        default:
                            Console.WriteLine("\nYanlis secim !");
                            Console.WriteLine("Davam etmek ucun enteri bas");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Davam etmek ucun enteri bas");
                    Console.ReadKey();
                }
            }
        }
    }
}