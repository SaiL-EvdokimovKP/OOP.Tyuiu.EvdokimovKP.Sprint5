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
        // Животные одного вида с одинаковым именем и возрастом считаются равными.
        public override bool Equals(object obj)
        {
            if (obj is Animal animal)
            {
                return GetType() == animal.GetType() && Name == animal.Name && Age == animal.Age;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetType(), Name, Age);
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
            HashSet<Animal> animals = new HashSet<Animal>();
            animals.Add(new Dog("Шарик", 3));
            animals.Add(new Cat("Мурка", 2));
            animals.Add(new Bird("Кеша", 1));

            // Отдельный объект с теми же данными повторно не добавится.
            bool isAdded = animals.Add(new Dog("Шарик", 3));
            Console.WriteLine("Добавлен повтор: " + isAdded);
            foreach (Animal animal in animals)
            {
                animal.DisplayInfo();
            }
        }

    }
}
