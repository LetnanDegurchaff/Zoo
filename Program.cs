using System;
using System.Collections.Generic;

namespace Zoo
{
    ///<summary>
    ///Пользователь запускает приложение и перед ним находится меню, 
    ///в котором он может выбрать, к какому вольеру подойти. 
    ///При приближении к вольеру, пользователю выводится информация о том, что это за вольер, 
    ///сколько животных там обитает, их пол и какой звук издает животное.
    ///Вольеров в зоопарке может быть много, в решении нужно создать минимум 4 вольера.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            zoo.Run();
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries;

        public void Run()
        {

        }
    }

    class Aviary
    {
        private List<IAnimal> _animals;

        public void ListenAnimals()
        {
            if (_animals.Count > 0)
            {
                foreach (IAnimal animal in _animals)
                {
                    animal.MakeSound();
                }
            }
            else
            {
                Console.WriteLine("В этом вольере нет животных");
            }
        }

        private void SetDescriptions()
        {
            foreach(IAnimal animal in _animals)
            {
                
            }
        }
    }

    #region Animals

    interface IAnimal
    {
        string Sex { get; }

        void MakeSound();
    }

    class Wolf : IAnimal
    {
        private const string Voice = "Ауууууууу";

        public Wolf()
        {

        }

        public string Sex { get; private set; }
        public string Description { get; private set; }

        public void MakeSound()
        {
            Console.WriteLine(Voice);
        }
    }

    class Lion : IAnimal
    {
        private const string Voice = "Рррррр";

        public Lion()
        {

        }

        public string Sex { get; private set; }

        public void MakeSound()
        {
            Console.WriteLine(Voice);
        }
    }

    class Horse : IAnimal
    {
        private const string Voice = "Иго-го-го-го";

        public Horse()
        {

        }

        public string Sex { get; private set; }

        public void MakeSound()
        {
            Console.WriteLine(Voice);
        }
    }

    class Owl : IAnimal
    {
        private const string Voice = "Уу-уу-уу";

        public Owl()
        {

        }

        public string Sex { get; private set; }

        public void MakeSound()
        {
            Console.WriteLine(Voice);
        }
    }

    class AnimalDescriptionConfig
    {
        public string WolfDescription { get; } = "Волк - вид хищных млекопитающих из семейства псовых. В качестве одного из ключевых хищников волки играют очень важную роль в балансе экосистем таких биомов, как леса умеренных широт, тайга, тундра, степи и горные системы. Всего выделяют примерно 32 подвида волка, различающихся размерами и оттенками меха.";
        public string LionDescription { get; } = "Лев — вид хищных млекопитающих, один из пяти представителей рода пантер, относящегося к подсемейству больших кошек в составе семейства кошачьих. Львы населяют в основном саванны, но иногда могут перебираться в кустарниковую местность или лес. В отличие от других кошачьих, они живут не поодиночке, а в особых семейных группах — прайдах. Прайд обычно состоит из родственных самок, потомства и нескольких взрослых самцов. Самки охотятся вместе, в большинстве случаев на крупных копытных. Львы не охотятся на людей целенаправленно, но случаи людоедства наблюдаются очень часто. Львы — сверххищники, то есть занимают верхнее положение в пищевой цепи.";
        public string HorseDescription { get; } = "Лошади — травоядное непарнокопытное млекопитающее, вид рода лошадей семейства лошадиных. Млекопитающие средних и крупных размеров. Преимущественно стройного сложения с длинными высокими ногами. Высота в холке диких видов составляет 1—1,6 м, вес 120—350 кг; одомашненные лошади обычно намного крупнее. Количество пальцев на передних конечностях 1, 3 или 4, на задних 1 или 3.";
        public string OwlDescription { get; } = "Сова - ночная хищная птица. Величина тела сов различна: от 40 до 180 см и от 50 г до 3,5 кг. Глаза большие, неподвижные, зато шея очень подвижна, совы могут поворачивать голову на 270 °. Клюв крепкий, с острым изогнутым крючком на конце. Крылья широкие, когти длинные и острые. Хвост обычно короткий. Окраска оперения в основном серого и бурого цвета.";
    }

    #endregion Animals

}
