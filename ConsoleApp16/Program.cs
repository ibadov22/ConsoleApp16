using System;
using UserNamespace;
using AdminNamespace;

namespace AdminandUserPost
{
    class Program
    {
        static void Main(string[] args)
        {
            Admin admin = new Admin();
            UserNamespace.User user = new UserNamespace.User();
            bool isRunning = true;

            string[] menuItems = { "Admin girişi", "İstifadəçi girişi", "Qeydiyyat", "Çıxış" };
            int selectedIndex = 0;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("=== Ana Menyu ===");

                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine($"{(i + 1)}. {menuItems[i]}");
                    Console.ResetColor();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + menuItems.Length) % menuItems.Length;
                        break;

                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % menuItems.Length;
                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        switch (selectedIndex)
                        {
                            case 0:
                                admin.AdminPanel();
                                break;

                            case 1:
                                UserNamespace.User loggedInUser = user.UserLogin();
                                if (loggedInUser != null)
                                {
                                    user.UserPanel(loggedInUser);
                                }
                                break;

                            case 2:
                                user.UserSignUp();
                                Console.WriteLine("\nDavam etmək üçün Enter düyməsini basın.");
                                Console.ReadKey();
                                break;

                            case 3:
                                Console.WriteLine("\nProqramdan çıxıldı!");
                                isRunning = false;
                                break;
                        }
                        break;
                }
            }
        }
    }
}
