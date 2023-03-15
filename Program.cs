using System;
using System.Collections.Generic;
using System.Threading;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();

            zoo.Run();
        }
    }

    static class RandomMethods
    {
        private static Random random = new Random();

        public static int GenerateRandomNumber(int minimum, int maximum)
        {
            return random.Next(minimum, maximum);
        }
    }

    enum ChoiceMenuCommand
    {
        Lions,
        Wolfs,
        Horses,
        Owls
    }

    class Zoo
    {

        private IPosition _position;
        private List<Cage> _cages;
        private int _choicedCommand;

        public Zoo()
        {
            AnimalFactory animalFactory = new AnimalFactory();
            CageFactory cageFactory = new CageFactory(animalFactory);

            int minimumAnimalCount = 1;
            int maximumAnimalCount = 5;

            _cages = new List<Cage>();
            _cages.Add(cageFactory.CreateLionCage(RandomMethods.GenerateRandomNumber(minimumAnimalCount, maximumAnimalCount + 1)));
            _cages.Add(cageFactory.CreateWolfCage(RandomMethods.GenerateRandomNumber(minimumAnimalCount, maximumAnimalCount + 1)));
            _cages.Add(cageFactory.CreateHorseCage(RandomMethods.GenerateRandomNumber(minimumAnimalCount, maximumAnimalCount + 1)));
            _cages.Add(cageFactory.CreateOwlCage(RandomMethods.GenerateRandomNumber(minimumAnimalCount, maximumAnimalCount + 1)));
            _choicedCommand = 0;
        }

        public void Run()
        {
            bool isWantExit = false;

            Console.WriteLine($"Добро пожаловать в зоопарк, " +
                    $"здесь вы можете посмотреть на животных, " +
                    $"послушать их и узнать интересную информацию о них\n");

            Console.ReadKey();
            Console.Clear();

            while (isWantExit == false)
            {
                DrowCommandsMenu();
                DrowExitCommandMenu();
                ExecuteNavigation(ref isWantExit);
                ShowDescription();

                Console.Clear();
            }
        }

        private void DrowCommandsMenu()
        {
            for (int i = 0; i < _cages.Count; i++)
            {
                if (i == _choicedCommand)
                {
                    ConsoleColor consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(_cages[i].AnimalType + "\n");
                    Console.ForegroundColor = consoleColor;
                }
                else
                {
                    Console.WriteLine(_cages[i].AnimalType + "\n");
                }
            }
        }

        private void DrowExitCommandMenu()
        {
            if (_choicedCommand == _cages.Count)
            {
                ConsoleColor consoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nВыход\n");
                Console.ForegroundColor = consoleColor;
            }
            else
            {
                Console.WriteLine($"\nВыход\n");
            }
        }

        private void ExecuteNavigation(ref bool isWantExit)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.DownArrow:
                    HighlightNextCommant();
                    break;
                case ConsoleKey.UpArrow:
                    HighlightPreviousCommant();
                    break;
                case ConsoleKey.Enter:
                    СomeToAviary(_choicedCommand, ref isWantExit);
                    break;
            }
        }

        private void ShowDescription()
        {
            if ((_position == null) == false)
            {
                Console.Clear();
                _position.ShowDescription(_cages);
                Console.ReadKey();
                _position = null;
            }
        }

        private void СomeToAviary(int command, ref bool isExitCommand)
        {
            switch ((ChoiceMenuCommand)command)
            {
                case ChoiceMenuCommand.Lions:
                    _position = new PositionNearbyLions();
                    break;
                case ChoiceMenuCommand.Wolfs:
                    _position = new PositionNearbyWolfs();
                    break;
                case ChoiceMenuCommand.Horses:
                    _position = new PositionNearbyHorses();
                    break;
                case ChoiceMenuCommand.Owls:
                    _position = new PositionNearbyOwls();
                    break;
                default:
                    isExitCommand = true;
                    break;
            }
        }

        private void HighlightNextCommant()
        {
            int lastCommand = _cages.Count;

            if (_choicedCommand < lastCommand)
                _choicedCommand++;
        }

        private void HighlightPreviousCommant()
        {
            int firstCommand = 0;

            if (_choicedCommand > firstCommand)
                _choicedCommand--;
        }
    }

    interface IPosition
    {
        void ShowDescription(List<Cage> cages);
    }

    abstract class PositionNearbyCage : IPosition
    {
        protected int NumberOfCage;

        public virtual void ShowDescription(List<Cage> cages)
        {
            Console.WriteLine($"В вольере живут {cages[NumberOfCage].AnimalType}\n" +
                $"Особей мужского пола: {cages[NumberOfCage].GetMaleAnimalsCount()}\n" +
                $"Особей женского пола: {cages[NumberOfCage].GetFemaleAnimalsCount()}\n" +
                $"Из вольера доносятся звуки: {cages[NumberOfCage].ListenAlimals()}\n\n" +
                $"{cages[NumberOfCage].Description}\n");
        }
    }

    class PositionNearbyLions : PositionNearbyCage
    {
        public PositionNearbyLions()
        {
            NumberOfCage = 0;
        }
    }

    class PositionNearbyWolfs : PositionNearbyCage
    {
        public PositionNearbyWolfs()
        {
            NumberOfCage = 1;
        }
    }

    class PositionNearbyHorses : PositionNearbyCage
    {
        public PositionNearbyHorses()
        {
            NumberOfCage = 2;
        }
    }

    class PositionNearbyOwls : PositionNearbyCage
    {
        public PositionNearbyOwls()
        {
            NumberOfCage = 3;
        }
    }

    class CageFactory
    {
        private AnimalFactory _animalFactory;
        private AnimalDescriptionConfig _animalDescriptionConfig;
        private AnimalTypeConfig _animalTypeConfig;

        public CageFactory(AnimalFactory animalFactory)
        {
            _animalFactory = animalFactory;
            _animalDescriptionConfig = new AnimalDescriptionConfig();
            _animalTypeConfig = new AnimalTypeConfig();
        }

        public Cage CreateLionCage(int count)
        {
            List<Animal> animals = new List<Animal>();

            for (int i = 0; i < count; i++)
            {
                animals.Add(_animalFactory.CreateLion());
            }

            return new Cage(animals, _animalDescriptionConfig.LionDescription, _animalTypeConfig.Lions);
        }

        public Cage CreateWolfCage(int count)
        {
            List<Animal> animals = new List<Animal>();

            for (int i = 0; i < count; i++)
            {
                animals.Add(_animalFactory.CreateWolf());
            }

            return new Cage(animals, _animalDescriptionConfig.WolfDescription, _animalTypeConfig.Wolfs);
        }

        public Cage CreateHorseCage(int count)
        {
            List<Animal> animals = new List<Animal>();

            for (int i = 0; i < count; i++)
            {
                animals.Add(_animalFactory.CreateHorse());
            }

            return new Cage(animals, _animalDescriptionConfig.HorseDescription, _animalTypeConfig.Horses);
        }

        public Cage CreateOwlCage(int count)
        {
            List<Animal> animals = new List<Animal>();

            for (int i = 0; i < count; i++)
            {
                animals.Add(_animalFactory.CreateOwl());
            }

            return new Cage(animals, _animalDescriptionConfig.OwlDescription, _animalTypeConfig.Owls);
        }
    }

    class AnimalFactory
    {
        public Lion CreateLion()
        {
            return new Lion();
        }

        public Wolf CreateWolf()
        {
            return new Wolf();
        }

        public Horse CreateHorse()
        {
            return new Horse();
        }

        public Owl CreateOwl()
        {
            return new Owl();
        }
    }

    class Cage
    {
        private List<Animal> _animals;

        public Cage(List<Animal> animals, string description, string type)
        {
            _animals = new List<Animal>(animals);
            Description = description;
            AnimalType = type;
        }

        public string AnimalType { get; private set; }
        public string Description { get; private set; }

        public string ListenAlimals()
        {
            return _animals[0].MakeSound();
        }

        public int GetMaleAnimalsCount()
        {
            AnimalGenderConfig animalGenderConfig = new AnimalGenderConfig();

            int maleAlimalsCount = 0;

            foreach (Animal animal in _animals)
            {
                if (animal.Gender == animalGenderConfig.MaleGender)
                {
                    maleAlimalsCount++;
                }
            }

            return maleAlimalsCount;
        }

        public int GetFemaleAnimalsCount()
        {
            AnimalGenderConfig animalGenderConfig = new AnimalGenderConfig();

            int femaleAlimalsCount = 0;

            foreach (Animal animal in _animals)
            {
                if (animal.Gender == animalGenderConfig.FemaleGender)
                {
                    femaleAlimalsCount++;
                }
            }

            return femaleAlimalsCount;
        }
    }

    #region Animals

    abstract class Animal
    {
        private AnimalGenderConfig _genderConfig = new AnimalGenderConfig();

        public int Age { get; private set; }
        public string Gender { get; private set; }

        public Animal()
        {
            int maximumAge = 30;
            int minimumAge = 10;

            Age = RandomMethods.GenerateRandomNumber(minimumAge, maximumAge);
            Gender = _genderConfig.GetRandomGender();
        }

        public abstract string MakeSound();
    }

    class Lion : Animal
    {
        public Lion() : base() { }

        public override string MakeSound()
        {
            return "Рррррр";
        }
    }

    class Wolf : Animal
    {
        public Wolf() : base() { }

        public override string MakeSound()
        {
            return "Ауууууууу";
        }
    }

    class Horse : Animal
    {
        public Horse() : base() { }

        public override string MakeSound()
        {
            return "Иго-го-го-го";
        }
    }

    class Owl : Animal
    {
        public Owl() : base() { }

        public override string MakeSound()
        {
            return "Уу-уу-уу";
        }
    }

    class AnimalDescriptionConfig
    {
        public string LionDescription { get; } = "Лев — вид хищных млекопитающих, один из пяти представителей рода пантер, относящегося к подсемейству больших кошек в составе семейства кошачьих. Львы населяют в основном саванны, но иногда могут перебираться в кустарниковую местность или лес. В отличие от других кошачьих, они живут не поодиночке, а в особых семейных группах — прайдах. Прайд обычно состоит из родственных самок, потомства и нескольких взрослых самцов. Самки охотятся вместе, в большинстве случаев на крупных копытных. Львы не охотятся на людей целенаправленно, но случаи людоедства наблюдаются очень часто. Львы — сверххищники, то есть занимают верхнее положение в пищевой цепи.";
        public string WolfDescription { get; } = "Волк - вид хищных млекопитающих из семейства псовых. В качестве одного из ключевых хищников волки играют очень важную роль в балансе экосистем таких биомов, как леса умеренных широт, тайга, тундра, степи и горные системы. Всего выделяют примерно 32 подвида волка, различающихся размерами и оттенками меха.";
        public string HorseDescription { get; } = "Лошади — травоядное непарнокопытное млекопитающее, вид рода лошадей семейства лошадиных. Млекопитающие средних и крупных размеров. Преимущественно стройного сложения с длинными высокими ногами. Высота в холке диких видов составляет 1—1,6 м, вес 120—350 кг; одомашненные лошади обычно намного крупнее. Количество пальцев на передних конечностях 1, 3 или 4, на задних 1 или 3.";
        public string OwlDescription { get; } = "Сова - ночная хищная птица. Величина тела сов различна: от 40 до 180 см и от 50 г до 3,5 кг. Глаза большие, неподвижные, зато шея очень подвижна, совы могут поворачивать голову на 270 °. Клюв крепкий, с острым изогнутым крючком на конце. Крылья широкие, когти длинные и острые. Хвост обычно короткий. Окраска оперения в основном серого и бурого цвета.";
    }

    class AnimalTypeConfig
    {
        public string Lions { get; } = "Львы";
        public string Wolfs { get; } = "Волки";
        public string Horses { get; } = "Лошади";
        public string Owls { get; } = "Совы";
    }

    class AnimalGenderConfig
    {
        public string MaleGender { get; } = "Male";
        public string FemaleGender { get; } = "Female";

        public string GetRandomGender()
        {
            List<string> genders = new List<string>();
            genders.Add(MaleGender);
            genders.Add(FemaleGender);

            return genders[RandomMethods.GenerateRandomNumber(0, genders.Count)];
        }
    }

    #endregion Animals

}