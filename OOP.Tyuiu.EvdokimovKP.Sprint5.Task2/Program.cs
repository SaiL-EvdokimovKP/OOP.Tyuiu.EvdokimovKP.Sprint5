using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimalCollections
{
    public delegate void DisplayDelegate();

    public class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Animal(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Имя: {Name}, возраст: {Age}");
        }
        public delegate void AnimalHandler(string message);
        public event AnimalHandler AnimalUpdated;

        public void UpdateName(string newName)
        {
            Name = newName;
            if (AnimalUpdated != null)
            {
                AnimalUpdated("Новое имя животного: " + Name);
            }
        }
    }

    public class Dog : Animal
    {
        public Dog(string name, int age) : base(name, age) { }

        public override void DisplayInfo()
        {
            Console.WriteLine("Собака");
            base.DisplayInfo();
        }
    }

    public class Cat : Animal
    {
        public Cat(string name, int age) : base(name, age) { }

        public override void DisplayInfo()
        {
            Console.WriteLine("Кошка");
            base.DisplayInfo();
        }
    }

    public class Bird : Animal
    {
        public Bird(string name, int age) : base(name, age) { }

        public override void DisplayInfo()
        {
            Console.WriteLine("Птица");
            base.DisplayInfo();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dog dog = new Dog("Шарик", 3);
            Cat cat = new Cat("Мурка", 2);
            Bird bird = new Bird("Кеша", 1);

            // Делегаты хранят методы, которые можно вызвать позднее.
            DisplayDelegate displayDog = dog.DisplayInfo;
            DisplayDelegate displayCat = cat.DisplayInfo;
            DisplayDelegate displayBird = bird.DisplayInfo;
            displayDog();
            displayCat();
            displayBird();

            // Лямбда задаёт условие выбора животных из списка.
            List<Animal> animals = new List<Animal> { dog, cat, bird };
            Console.WriteLine("Животные старше одного года:");
            var olderAnimals = animals.Where(animal => animal.Age > 1);
            foreach (Animal animal in olderAnimals)
            {
                animal.DisplayInfo();
            }

            // Подписываем обработчик. Изменение имени вызывает событие.
            dog.AnimalUpdated += OnAnimalUpdated;
            cat.AnimalUpdated += OnAnimalUpdated;
            bird.AnimalUpdated += OnAnimalUpdated;
            dog.UpdateName("Бобик");
            cat.UpdateName("Муся");
            bird.UpdateName("Гоша");
        }

        static void OnAnimalUpdated(string message)
        {
            Console.WriteLine(message);
        }
    }
}
