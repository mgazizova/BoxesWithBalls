﻿using System;
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
            while (true)
            {
                
                List<Balls> listBalls1 = new List<Balls>();

                Balls greenBalls = new Balls(3, Balls.Colors.Green);
                Balls yellowBalls = new Balls(3, Balls.Colors.Yellow);
                Balls blueBalls = new Balls(3, Balls.Colors.Blue);

                listBalls1.Add(yellowBalls);
                listBalls1.Add(greenBalls);
                listBalls1.Add(blueBalls);

                Box boxWithBalls1 = new Box(listBalls1);
                boxWithBalls1.showBox();

                Console.WriteLine();
                Console.WriteLine();

                List<Balls> listBalls2 = new List<Balls>();

                Balls greenBalls2 = new Balls(1, Balls.Colors.Green);
                Balls yellowBalls2 = new Balls(1, Balls.Colors.White);
                Balls blueBalls2 = new Balls(1, Balls.Colors.Blue);

                listBalls2.Add(yellowBalls2);
                listBalls2.Add(greenBalls2);
                listBalls2.Add(blueBalls2);

                Box boxWithBalls2 = new Box(listBalls2);
                boxWithBalls2.showBox();

                Console.WriteLine();
                Console.WriteLine();
                //Console.WriteLine(listBalls1.Count);
                //boxWithBalls1.GetRandomBalls(5);
                //boxWithBalls1.showBox();
                Box.PutFromOneToAnother(boxWithBalls1,boxWithBalls2, 5);
                //boxWithBalls1.GetColorBall(Balls.Colors.Black);
                //boxWithBalls1.showBox();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.ReadKey();
            }
            
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

        public static void ShowBallsInBox(List<Balls> lb1)
        {
            foreach (Balls balls in lb1)
            {
                Console.WriteLine("Color: {0}   Count: {1}", balls.color, balls.number);
            }
        }

        public static Box PutFromOneToAnother(Box b1, Box b2, int number)
        {
            var b111=new Box(b1.ballsInBox);
            var b11 = GetRandomBalls(b111, number); // это говнище
            var tembBox = b1 - b11;
            b2 = b2 + tembBox;
            return b2;
        }

        public static List<Balls> GetRandomBallFromBox(List<Balls> lb1)
        {
            if (lb1.Count!=0)
            {
                Random rnd = new Random();
                int index = rnd.Next(lb1.Count);
                if (lb1[index].number > 1)
                    lb1[index].number-- ;
                else
                    lb1.RemoveAt(index);
            }
            else
                Console.WriteLine("Error while getting balls! ");  
            return lb1;        
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

        public void showBox()
        {
            ShowBallsInBox(this.ballsInBox);
        }

        public Box ClearBox()
        {
            return new Box();
        } 

        public static Box GetRandomBalls(Box box, int number)
        {
            var newBoxWithoutNumberOfBalls = new Box(box);
            //достать number случайных шаров
            for (int i=0; i<number; i++)
            {
                if (newBoxWithoutNumberOfBalls.ballsInBox.Count != 0)
                {
                    newBoxWithoutNumberOfBalls.ballsInBox = GetRandomBallFromBox(newBoxWithoutNumberOfBalls.ballsInBox);
                }
                else
                {
                    Console.WriteLine("Error while getting balls! ");
                    break;
                }
            }
            return new Box(newBoxWithoutNumberOfBalls.ballsInBox);
        }

        public Box GetColorBall(Balls.Colors color)
        {
            var colorExist = false;
            foreach (var balls in ballsInBox)
            {
                if (balls.color == color)
                {
                    colorExist = true;
                    if (balls.number > 1)
                        balls.number--;
                    else
                        ballsInBox.Remove(balls);
                }
            }

            if (colorExist == false)
            {
                Console.WriteLine($"Error! {color} color does not exist in the box.");
            }

            return new Box(ballsInBox);
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
