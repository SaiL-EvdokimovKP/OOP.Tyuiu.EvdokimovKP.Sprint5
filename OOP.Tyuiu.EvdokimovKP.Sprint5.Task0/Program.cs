using System;
using System.Collections.Generic;

namespace AnimalCollections
{
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

            // Список хранит животных разных видов через базовый тип Animal.
            List<Animal> animals = new List<Animal> { dog, cat, bird };
            foreach (Animal animal in animals)
            {
                animal.DisplayInfo();
            }

            // В словаре каждое животное находится по своему номеру.
            Dictionary<string, Animal> animalDictionary = new Dictionary<string, Animal>();
            animalDictionary.Add("A1", dog);
            animalDictionary.Add("A2", cat);
            animalDictionary.Add("A3", bird);

            if (animalDictionary.TryGetValue("A2", out Animal foundAnimal))
            {
                Console.WriteLine("Найденное животное:");
                foundAnimal.DisplayInfo();
            }
            else
            {
                Console.WriteLine("Животное не найдено.");
            }
        }

    }
}
