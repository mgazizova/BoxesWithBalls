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

        public static bool Equals(List<Balls> lb1, List<Balls> lb2)
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

        public static Box operator +(Box box1, Box box2)
        {
            return new Box(MergeBallsInBoxes(box1.ballsInBox, box2.ballsInBox));
        }

        public static bool operator == (Box box1, Box box2)
        {
            //если первый равен второму И второй первому
            if (Equals(box1.ballsInBox, box2.ballsInBox) == true && Equals(box2.ballsInBox, box1.ballsInBox) == true)
                return true;
            else
                return false;
        }

        public static bool operator != (Box box1, Box box2)
        {
            if (Equals(box1.ballsInBox, box2.ballsInBox) == true && Equals(box2.ballsInBox, box1.ballsInBox) == true)
                return false;
            else
                return true;
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
