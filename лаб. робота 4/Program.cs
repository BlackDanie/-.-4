
using System;
using System.Collections.Generic;

namespace PetCareApp
{
    // Базовий клас для тварин
    class Pet
    {
        public string Name { get; set; }
        public int FeedingFrequency { get; set; }  // Частота годування

        public Pet(string name, int feedingFrequency)
        {
            Name = name;
            FeedingFrequency = feedingFrequency;
        }

        // Віртуальний метод догляду
        public virtual void Care()
        {
            Console.WriteLine("Догляд за твариною...");
        }

        // Перевантажений метод для розрахунку часу догляду
        public int CalculateCareTime(int minutes, string careType)
        {
            switch (careType.ToLower())
            {
                case "feeding":
                    return minutes * FeedingFrequency;
                case "grooming":
                    return minutes * 2; // Час на грумінг подвоюється
                case "walking":
                    return minutes * 3; // Час на вигул збільшується втричі
                default:
                    return minutes;
            }
        }
    }

    // Клас для собак
    class Dog : Pet
    {
        public int WalksPerDay { get; set; }

        public Dog(string name, int feedingFrequency, int walksPerDay)
            : base(name, feedingFrequency)
        {
            WalksPerDay = walksPerDay;
        }

        public override void Care()
        {
            Console.WriteLine($"{Name}: Вигул {WalksPerDay} раз(и) в день, годування {FeedingFrequency} раз(и) в день.");
        }

        public int CalculateDogCareTime(int minutes)
        {
            return CalculateCareTime(minutes, "feeding") + CalculateCareTime(minutes, "walking");
        }
    }

    // Клас для котів
    class Cat : Pet
    {
        public Cat(string name, int feedingFrequency)
            : base(name, feedingFrequency)
        {
        }

        public override void Care()
        {
            Console.WriteLine($"{Name}: Чистка шерсті, годування {FeedingFrequency} раз(и) в день.");
        }

        public int CalculateCatCareTime(int minutes)
        {
            return CalculateCareTime(minutes, "feeding") + CalculateCareTime(minutes, "grooming");
        }
    }

    // Клас для папуг
    class Parrot : Pet
    {
        public Parrot(string name, int feedingFrequency)
            : base(name, feedingFrequency)
        {
        }

        public override void Care()
        {
            Console.WriteLine($"{Name}: Годування {FeedingFrequency} раз(и) в день, прибирання клітки.");
        }

        public int CalculateParrotCareTime(int minutes)
        {
            return CalculateCareTime(minutes, "feeding") + CalculateCareTime(minutes, "cleaning");
        }
    }

    // Клас для кроликів
    class Rabbit : Pet
    {
        public Rabbit(string name, int feedingFrequency)
            : base(name, feedingFrequency)
        {
        }

        public override void Care()
        {
            Console.WriteLine($"{Name}: Годування {FeedingFrequency} раз(и) в день, чистка клітки.");
        }

        public int CalculateRabbitCareTime(int minutes)
        {
            return CalculateCareTime(minutes, "feeding") + CalculateCareTime(minutes, "cleaning");
        }
    }

    // Клас для хом'яків
    class Hamster : Pet
    {
        public Hamster(string name, int feedingFrequency)
            : base(name, feedingFrequency)
        {
        }

        public override void Care()
        {
            Console.WriteLine($"{Name}: Годування {FeedingFrequency} раз(и) в день, чистка клітки, тренування на колесі.");
        }

        public int CalculateHamsterCareTime(int minutes)
        {
            return CalculateCareTime(minutes, "feeding") + CalculateCareTime(minutes, "cleaning") + CalculateCareTime(minutes, "exercise");
        }
    }

    // Клас для власних доданих тварин
    class CustomPet : Pet
    {
        public string CustomCare { get; set; }

        public CustomPet(string name, int feedingFrequency, string customCare)
            : base(name, feedingFrequency)
        {
            CustomCare = customCare;
        }

        public override void Care()
        {
            Console.WriteLine($"{Name}: {CustomCare}, годування {FeedingFrequency} раз(и) в день.");
        }

        public int CalculateCustomPetCareTime(int minutes)
        {
            return CalculateCareTime(minutes, CustomCare.ToLower());
        }
    }

    class Program
    {
        static List<Pet> customPets = new List<Pet>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nОберіть дію:");
                Console.WriteLine("1. Обрати тип домашньої тварини");
                Console.WriteLine("2. Додати власну тварину");
                Console.WriteLine("3. Вийти");

                int action = int.Parse(Console.ReadLine());

                if (action == 3)
                {
                    Console.WriteLine("Програма завершена.");
                    break;
                }

                if (action == 1)
                {
                    ChoosePet();
                }
                else if (action == 2)
                {
                    AddCustomPet();
                }
            }
        }

        static void ChoosePet()
        {
            Console.WriteLine("\nОберіть тип домашньої тварини:");
            Console.WriteLine("1. Собака");
            Console.WriteLine("2. Кіт");
            Console.WriteLine("3. Папуга");
            Console.WriteLine("4. Кролик");
            Console.WriteLine("5. Хом'як");
            Console.WriteLine("6. Власна тварина");

            int choice = int.Parse(Console.ReadLine());

            Pet pet = null;

            switch (choice)
            {
                case 1:
                    pet = CreateDog();
                    break;
                case 2:
                    pet = CreateCat();
                    break;
                case 3:
                    pet = CreateParrot();
                    break;
                case 4:
                    pet = CreateRabbit();
                    break;
                case 5:
                    pet = CreateHamster();
                    break;
                case 6:
                    pet = ChooseCustomPet();
                    break;
                default:
                    Console.WriteLine("Невірний вибір.");
                    return;
            }

            if (pet != null)
            {
                pet.Care();
                Console.WriteLine($"Введіть кількість хвилин на один цикл догляду:");
                int minutes = int.Parse(Console.ReadLine());

                int totalCareTime = CalculatePetCareTime(pet, minutes);

                Console.WriteLine($"Час догляду за {pet.Name}: {totalCareTime} хвилин.");
            }
        }

        static int CalculatePetCareTime(Pet pet, int minutes)
        {
            if (pet is Dog)
                return ((Dog)pet).CalculateDogCareTime(minutes);
            else if (pet is Cat)
                return ((Cat)pet).CalculateCatCareTime(minutes);
            else if (pet is Parrot)
                return ((Parrot)pet).CalculateParrotCareTime(minutes);
            else if (pet is Rabbit)
                return ((Rabbit)pet).CalculateRabbitCareTime(minutes);
            else if (pet is Hamster)
                return ((Hamster)pet).CalculateHamsterCareTime(minutes);
            else if (pet is CustomPet)
                return ((CustomPet)pet).CalculateCustomPetCareTime(minutes);
            else
                return minutes;
        }

        static void AddCustomPet()
        {
            Console.WriteLine("Введіть назву вашої тварини:");
            string name = Console.ReadLine();

            Console.WriteLine("Введіть кількість годувань:");
            int feeding = int.Parse(Console.ReadLine());

            Console.WriteLine("Введіть опис догляду (наприклад, годування, вигул, чистка клітки):");
            string customCare = Console.ReadLine();

            CustomPet customPet = new CustomPet(name, feeding, customCare);
            customPets.Add(customPet);

            Console.WriteLine($"{name} було додано до програми.");
        }

        static CustomPet ChooseCustomPet()
        {
            if (customPets.Count == 0)
            {
                Console.WriteLine("Немає доданих власних тварин.");
                return null;
            }

            Console.WriteLine("Оберіть вашу тварину:");
            for (int i = 0; i < customPets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {customPets[i].Name}");
            }

            int choice = int.Parse(Console.ReadLine());
            if (choice > 0 && choice <= customPets.Count)
            {
                return (CustomPet)customPets[choice - 1];
            }

            Console.WriteLine("Невірний вибір.");
            return null;
        }

        // Існуючі методи для створення стандартних тварин
        static Dog CreateDog()
        {
            Console.WriteLine("Використати стандартні параметри? (так/ні)");
            string answer = Console.ReadLine().ToLower();

            if (answer == "так")
            {
                return new Dog("Собака", 2, 2);
            }
            else
            {
                Console.WriteLine("Введіть кількість годувань:");
                int feeding = int.Parse(Console.ReadLine());
                Console.WriteLine("Введіть кількість вигулів:");
                int walks = int.Parse(Console.ReadLine());

                if (feeding <= 0 || walks <= 0)
                {
                    Console.WriteLine("Невірні параметри. Використовую стандартні.");
                    return new Dog("Собака", 2, 2);
                }

                return new Dog("Собака", feeding, walks);
            }
        }

        static Cat CreateCat()
        {
            Console.WriteLine("Використати стандартні параметри? (так/ні)");
            string answer = Console.ReadLine().ToLower();

            if (answer == "так")
            {
                return new Cat("Кіт", 2);
            }
            else
            {
                Console.WriteLine("Введіть кількість годувань:");
                int feeding = int.Parse(Console.ReadLine());

                if (feeding <= 0)
                {
                    Console.WriteLine("Невірні параметри. Використовую стандартні.");
                    return new Cat("Кіт", 2);
                }

                return new Cat("Кіт", feeding);
            }
        }

        static Parrot CreateParrot()
        {
            Console.WriteLine("Використати стандартні параметри? (так/ні)");
            string answer = Console.ReadLine().ToLower();

            if (answer == "так")
            {
                return new Parrot("Папуга", 2);
            }
            else
            {
                Console.WriteLine("Введіть кількість годувань:");
                int feeding = int.Parse(Console.ReadLine());

                if (feeding <= 0)
                {
                    Console.WriteLine("Невірні параметри. Використовую стандартні.");
                    return new Parrot("Папуга", 2);
                }

                return new Parrot("Папуга", feeding);
            }
        }

        static Rabbit CreateRabbit()
        {
            Console.WriteLine("Використати стандартні параметри? (так/ні)");
            string answer = Console.ReadLine().ToLower();

            if (answer == "так")
            {
                return new Rabbit("Кролик", 2);
            }
            else
            {
                Console.WriteLine("Введіть кількість годувань:");
                int feeding = int.Parse(Console.ReadLine());

                if (feeding <= 0)
                {
                    Console.WriteLine("Невірні параметри. Використовую стандартні.");
                    return new Rabbit("Кролик", 2);
                }

                return new Rabbit("Кролик", feeding);
            }
        }

        static Hamster CreateHamster()
        {
            Console.WriteLine("Використати стандартні параметри? (так/ні)");
            string answer = Console.ReadLine().ToLower();

            if (answer == "так")
            {
                return new Hamster("Хом'як", 2);
            }
            else
            {
                Console.WriteLine("Введіть кількість годувань:");
                int feeding = int.Parse(Console.ReadLine());

                if (feeding <= 0)
                {
                    Console.WriteLine("Невірні параметри. Використовую стандартні.");
                    return new Hamster("Хом'як", 2);
                }

                return new Hamster("Хом'як", feeding);
            }
        }
    }
}
