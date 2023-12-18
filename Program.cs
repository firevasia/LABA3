using System;
using System.Numerics;

namespace testAPlusBSquare
{
    interface IMyNumber<T> where T : IMyNumber<T>
    {
        T Add(T b);
        T Subtract(T b);
        T Multiply(T b);
        T Divide(T b);
    }

    class MyFrac : IMyNumber<MyFrac>
    {
        private BigInteger nom;
        private BigInteger denom;

        public MyFrac(BigInteger nom, BigInteger denom)
        {
            this.nom = nom;
            this.denom = denom;
            Simplify();
        }
        
        public MyFrac(int nom, int denom)
        {
            this.nom = nom;
            this.denom = denom;
            Simplify();
        }
        //евкліда
        private void Simplify()
        {
            BigInteger gcd = GCD(nom, denom);
            if (gcd > 1)
            {
                nom /= gcd;
                denom /= gcd;
            }
        }

        private BigInteger GCD(BigInteger a, BigInteger b)
        {
            while (b != 0)
            {
                BigInteger temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public MyFrac Add(MyFrac that)
        {
            BigInteger a = this.nom;
            BigInteger b = this.denom;
            BigInteger c = that.nom;
            BigInteger d = that.denom;
            BigInteger ad = BigInteger.Multiply(a, d);
            BigInteger bc = BigInteger.Multiply(b, c);
            BigInteger ad_plus_bc = BigInteger.Add(ad, bc);
            BigInteger bd = BigInteger.Multiply(b, d);
            if (bd.IsZero)
            {
                throw new DivideByZeroException();
            }
            return new MyFrac(ad_plus_bc, bd);
        }

        public MyFrac Subtract(MyFrac that)
        {
            BigInteger a = this.nom;
            BigInteger b = this.denom;
            BigInteger c = that.nom;
            BigInteger d = that.denom;
            BigInteger ad = BigInteger.Multiply(a, d);
            BigInteger bc = BigInteger.Multiply(b, c);
            BigInteger ad_minus_bc = BigInteger.Subtract(ad, bc);
            BigInteger bd = BigInteger.Multiply(b, d);
            if (bd.IsZero)
            {
                throw new DivideByZeroException();
            }
            return new MyFrac(ad_minus_bc, bd);
        }

        public MyFrac Multiply(MyFrac that)
        {
            BigInteger a = this.nom;
            BigInteger b = this.denom;
            BigInteger c = that.nom;
            BigInteger d = that.denom;
            BigInteger ac = BigInteger.Multiply(a, c);
            BigInteger bd = BigInteger.Multiply(b, d);
            if (bd.IsZero)
            {
                throw new DivideByZeroException();
            }
            return new MyFrac(ac, bd);
        }

        public MyFrac Divide(MyFrac that)
        {
            BigInteger a = this.nom;
            BigInteger b = this.denom;
            BigInteger c = that.nom;
            BigInteger d = that.denom;
            BigInteger ad = BigInteger.Multiply(a, d);
            BigInteger bc = BigInteger.Multiply(b, c);
            if (bc .IsZero)
            {
                throw new DivideByZeroException();
            }
            return new MyFrac(ad, bc);
        }

        public override string ToString()
        {
            return $"{nom}/{denom}";
        }
    }

    class MyComplex : IMyNumber<MyComplex>
    {
        private double re;
        private double im;

        public MyComplex(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        public MyComplex Add(MyComplex that)
        {
            return new MyComplex(this.re + that.re, this.im + that.im);
        }


        public MyComplex Subtract(MyComplex that)
        {
            return new MyComplex(this.re - that.re, this.im - that.im);
        }
        public MyComplex Multiply(MyComplex that)
        {
            double a = this.re;
            double b = this.im;
            double c = that.re;
            double d = that.im;
            double ac = a * c;
            double bd = b * d;
            double ad = a * d;
            double bc = b * c;
            return new MyComplex(ac - bd, ad + bc);
        }

        public MyComplex Divide(MyComplex that)
        {
            double a = this.re;
            double b = this.im;
            double c = that.re;
            double d = that.im;
            double ac = a * c;
            double bd = b * d;
            double ad = a * d;
            double bc = b * c;
            double c_2plusd_2 = c * c + d * d;
            if (c_2plusd_2 == 0)
            {
                throw new DivideByZeroException();
            }
            return new MyComplex((ac + bd) / c_2plusd_2, (bc - ad) / c_2plusd_2);
        }

        public override string ToString()
        {
            if (this.im > 0)
            {
                return $"{this.re}+{this.im}i";
            }
            else if (this.im == 0)
            {
                return $"{this.re}";
            }
            return $"{this.re}{this.im}i";
        }
    }
    class Program
    {
        static void testAPlusBSquare1<T>(T a, T b) where T : IMyNumber<T>
        {
            Console.WriteLine("=== Starting testing (a+b)^2=a^2+2ab+b^2 with a = " + a + ", b = " + b + " ===");
            T aPlusB = a.Add(b);
            Console.WriteLine("a = " + a);
            Console.WriteLine("b = " + b);
            Console.WriteLine("(a + b) = " + aPlusB);
            Console.WriteLine("(a+b)^2 = " + aPlusB.Multiply(aPlusB));
            Console.WriteLine(" = = = ");
            T curr = a.Multiply(a);
            Console.WriteLine("a^2 = " + curr);
            T wholeRightPart = curr;
            curr = a.Multiply(b); 
            curr = curr.Add(curr); 
            Console.WriteLine("2*a*b = " + curr);
            wholeRightPart = wholeRightPart.Add(curr);
            curr = b.Multiply(b);
            Console.WriteLine("b^2 = " + curr);
            wholeRightPart = wholeRightPart.Add(curr);
            Console.WriteLine("a^2+2ab+b^2 = " + wholeRightPart);
            //-------------------------------------
            Console.WriteLine("a/b: ");
            T aDivideb = a.Divide(b);
            Console.WriteLine("a/b = " + aDivideb);
            Console.WriteLine("a-b: ");
            T aMinusb = a.Subtract(b);
            Console.WriteLine("a-b = " + aMinusb);
            Console.WriteLine("=== Finishing testing (a+b)^2=a^2+2ab+b^2 with a = " + a + ", b = " + b + " ===");

        }
        static void testAPlusBSquare2<T>(T a, T b) where T : IMyNumber<T>
        {
            T aPlusB = a.Add(b);
            Console.WriteLine("z_1 = " + a);
            Console.WriteLine("z_2 = " + b);
            Console.WriteLine("Testing (z_1 + z_2) = " + aPlusB);
            Console.WriteLine("Testing ( z_1 + z_2)^2 = " + aPlusB.Multiply(aPlusB));
            Console.WriteLine("Testing ( z_1 - z_2) = " + a.Subtract(b));
            Console.WriteLine("Testing ( z_1/z_2) = " + a.Divide(b));
        }

        static void Main(string[] args)
        {
            MyFrac a = new MyFrac(-12, 3);
            MyFrac b = new MyFrac(1, 6);
            //Визов функції тесту для екземплярів
            testAPlusBSquare1(a, b);
            MyComplex z_1 = new MyComplex(5, -6);
            MyComplex z_2 = new MyComplex(-3, 2);
            //Визов функції тесту для екземплярів
            testAPlusBSquare2(z_1, z_2);

            Console.ReadKey();
        }
    }
}