using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxesWithBalls
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
        }
    }

    class Box
    {
        public List<Balls> ballsInBox;

        public static List<Balls> MergeBallsInBoxes(List<Balls> lb1, List<Balls> lb2)
        {
            var mergedBallsInBox = new List<Balls>();
            foreach (Balls balls1 in lb1)
            {
                foreach (Balls balls2 in lb2)
                {
                    if (balls1.color == balls2.color)
                    {
                        mergedBallsInBox.Add(new Balls(balls1.number + balls2.number, balls1.color));
                        break;
                    }
                }
            }

            foreach (var balls1 in lb1)
            {
                var flag = false;
                foreach (var balls3 in mergedBallsInBox)
                {
                    if (balls3.color == balls1.color)
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag != true)
                {
                    mergedBallsInBox.Add(new Balls(balls1.number, balls1.color));
                }
            }

            foreach (var balls2 in lb2)
            {
                var flag = false;
                foreach (var balls3 in mergedBallsInBox)
                {
                    if (balls3.color == balls2.color)
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag != true)
                {
                    mergedBallsInBox.Add(new Balls(balls2.number, balls2.color));
                }
            }

            return mergedBallsInBox;
        }

        public static List<Balls> DifferenceBallsInBoxes(List<Balls> lb1, List<Balls> lb2)
        {
            var differenceBallsInBox = new List<Balls>();
            var matches = false; //нашли совпадение совпадение
            foreach (var balls2 in lb2)
            {
                matches = false;
                foreach (var balls1 in lb1)
                {
                    if (balls2.color == balls1.color)
                    {
                        matches = true;
                        break;
                    }
                }
                if (matches == false)
                {
                    Console.WriteLine("Error in the difference operation!");    //в первом листе нет шаров подходящего цвета
                }
            }
            foreach (var balls1 in lb1)
            {
                matches = false;
                foreach (var balls2 in lb2)
                {
                    if (balls1.color == balls2.color && balls1.number > balls2.number)
                    {
                        differenceBallsInBox.Add(new Balls(balls1.number - balls2.number, balls1.color));
                        matches = true;
                        break;
                    }
                    else if (balls1.color == balls2.color && balls1.number < balls2.number)
                    {
                        Console.WriteLine("Error in the difference operation!");   //пытаемся вычесть слишком большое количество шаров
                    }
                }
                if (matches == false)
                {
                    differenceBallsInBox.Add(new Balls(balls1.number, balls1.color));
                }
            }
            return differenceBallsInBox;
        }

        public static bool ContainsEqualsBallsInBoxes(List<Balls> lb1, List<Balls> lb2)
        {
            var isListEquals = true;
            var flag = false;
            foreach (Balls balls1 in lb1)
            {
                flag = false;
                foreach (Balls balls2 in lb2)
                {
                    if (balls1.color == balls2.color)
                    {
                        if (balls1.number != balls2.number)
                        {
                            isListEquals = false;
                            break;
                        }
                        else
                            flag = true;    //нашли совпадение по цвету и количеству
                    }
                }
                if (flag == false)  //совпадений не найдено
                {
                    isListEquals = false;
                    break;
                }
            }
            return isListEquals;
        }

        public Box()    
        {
            ballsInBox = new List<Balls>();
        }

        public Box(List<Balls> lb)
        {
            ballsInBox = lb;
        }

        public Box(Box box)
        {
            ballsInBox = box.ballsInBox;
        }

        public static Box operator + (Box box1, Box box2)
        {
            return new Box(MergeBallsInBoxes(box1.ballsInBox, box2.ballsInBox));
        }

        public static Box operator - (Box box1, Box box2)
        {
            return new Box(DifferenceBallsInBoxes(box1.ballsInBox, box2.ballsInBox));
        }

        public static bool operator == (Box box1, Box box2)
        {
            //если первый равен второму И второй первому
            if (ContainsEqualsBallsInBoxes(box1.ballsInBox, box2.ballsInBox) == true && ContainsEqualsBallsInBoxes(box2.ballsInBox, box1.ballsInBox) == true)
                return true;
            else
                return false;
        }

        public static bool operator != (Box box1, Box box2)
        {
            if (ContainsEqualsBallsInBoxes(box1.ballsInBox, box2.ballsInBox) == true && ContainsEqualsBallsInBoxes(box2.ballsInBox, box1.ballsInBox) == true)
                return false;
            else
                return true;
        }

        public static bool containsBoxes(Box box1, Box box2)    //проверка на включение одного контейнера в другой
        {
            if (ContainsEqualsBallsInBoxes(box2.ballsInBox, box2.ballsInBox)==true)
                return true;
            else 
                return false;
        }

        public static Box ClearBox(Box box1)
        {
            return new Box();
        }

        ~Box()
        {
            Console.WriteLine("Сработал деструктор");
        }

    }

    class Balls
    {
        public int number;
        public Colors color;

        public Balls(int n, Colors c)
        {
            number = n;
            color = c;
        }

        public Balls()
        {
            number = 0;
            color = Colors.Unknown;
        }

        public enum Colors
        {
            Red,
            Green,
            Blue,
            Yellow,
            Black,
            White,
            Unknown
        }
    }


}
