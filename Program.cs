

using System;

namespace t1
{
    class Program
    {
        static void Main(string[] args)
        {


            while (true)
            {
                System.Console.WriteLine("~~~~~ POLY TERM ~~~~~");
                System.Console.WriteLine("Usage: ");
                System.Console.WriteLine("     Enter First Polynomial   -> x+2x^2-35x^6");
                System.Console.WriteLine("     Enter Second Polynomial  -> 5x + 2X^3 - 3 + 28x^2");

                System.Console.WriteLine();

                System.Console.Write("     Enter First Polynomial   -> ");
                Poly p1 = new Poly(Console.ReadLine());
                System.Console.Write("     Enter Second Polynomial  -> ");
                Poly p2 = new Poly(Console.ReadLine());
                Poly sum = p2 + p1;
                Poly sub = p1 - p2;
                System.Console.WriteLine("        Summation : " + sum.ToString());
                System.Console.WriteLine("        Submission: " + sub.ToString());
                System.Console.WriteLine(p1.ToString());
                System.Console.WriteLine(p2.ToString());
                Console.ReadLine();

            }
        }
    }


    struct Poly
    {
        public PolyMember[] members;
        public string input;
        public Poly(string input) : this()
        {
            this.input = input.ToLower();
            this._inputToMember();
        }
        public Poly(int Length) : this()
        {
            this.members = new PolyMember[Length];
        }
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < this.members.Length; i++)
            {
                if (members[i].prefix > 0) output += "+";
                output += members[i].ToString();
            }
            return output == "" ? "0" : output;
        }
        //    x+x^2+2x^3
        public static Poly operator +(Poly poly, Poly poly2)
        {

            Poly sum = new Poly(poly.members.Length + poly2.members.Length);
            for (int k = 0; k < poly.members.Length; k++)
            {
                sum.members[k].power = poly.members[k].power;
                sum.members[k].prefix = poly.members[k].prefix;
                sum.members[k].x = poly.members[k].x;
            }
            for (int k = poly.members.Length; k < poly2.members.Length + poly.members.Length; k++)
            {
                sum.members[k].power = poly2.members[k - poly.members.Length].power;
                sum.members[k].prefix = poly2.members[k - poly.members.Length].prefix;
                sum.members[k].x = poly2.members[k - poly.members.Length].x;
            }
            for (int i = 0; i < sum.members.Length; i++)
                for (int j = 0; j < sum.members.Length; j++)
                    if (sum.members[i].power == sum.members[j].power && i != j && sum.members[j].prefix != 0)
                    {
                        sum.members[i].prefix += sum.members[j].prefix;
                        sum.members[j].prefix = 0;
                        sum.members[j].power = 0;
                        if (sum.members[i].power == 0) sum.members[i].x = false;
                    }


            Poly beSummed = poly2;
            // for (int i = 0; i < poly.members.Length; i++)
            //     for (int j = 0; j < beSummed.members.Length; j++)
            //         if (poly.members[i].power == beSummed.members[j].power) sum.members[i].prefix += beSummed.members[j].prefix;
            return sum;
        }

        public static Poly operator -(Poly poly, Poly poly2)
        {
            Poly sub = new Poly(poly.members.Length + poly2.members.Length);
            for (int k = 0; k < poly.members.Length; k++)
            {
                sub.members[k].power = poly.members[k].power;
                sub.members[k].prefix = poly.members[k].prefix;
                sub.members[k].x = poly.members[k].x;
            }
            for (int k = poly.members.Length; k < poly2.members.Length + poly.members.Length; k++)
            {
                sub.members[k].power = poly2.members[k - poly.members.Length].power;
                sub.members[k].prefix = -1 * poly2.members[k - poly.members.Length].prefix;
                sub.members[k].x = poly2.members[k - poly.members.Length].x;
            }
            for (int i = 0; i < sub.members.Length; i++)
                for (int j = 0; j < sub.members.Length; j++)
                    if (sub.members[i].power == sub.members[j].power && i != j && sub.members[j].prefix != 0)
                    {
                        sub.members[i].prefix += sub.members[j].prefix;
                        sub.members[j].prefix = 0;
                        sub.members[j].power = 0;
                        if (sub.members[i].power == 0) sub.members[i].x = false;
                    }

            return sub;

        }
        public void _inputToMember()
        {
            string input = this.input;
            input = input.Replace("+", "p+");
            input = input.Replace("-", "p-");
            input = input.Replace(" ", "");
            string[] inputSep = input.Split('p');
            this.members = new PolyMember[inputSep.Length];
            for (int i = 0; i < inputSep.Length; i++)

                this.members[i] = new PolyMember(inputSep[i]);
            //    +x -32x^2 +x^2+ 2x^3
            // PolyMember[] members2 = members;
            for (int i = 0; i < inputSep.Length; i++)
                for (int j = 0; j < inputSep.Length; j++)
                    if (members[i].power == members[j].power && i != j && members[j].prefix != 0)
                    {
                        members[i].prefix += members[j].prefix;
                        members[j].prefix = 0;
                        members[j].power = 0;
                    }



        }

    }
    struct PolyMember
    {
        // char sign;
        public bool x;
        public int power;
        public double prefix;
        public PolyMember(string item)
        {
            this.x = true;
            this.prefix = 1;
            this.power = 1;
            if (item != "" && item[0] == '-')
            {
                this.prefix *= -1;
                item = item.Remove(0, 1);
            };

            if (item != "" && item[0] == '+') item = item.Remove(0, 1);
            ;
            if (item.Contains("x^"))
            {
                this.prefix *= item.Substring(0, item.Length - item.Substring(item.IndexOf('x')).Length) == "" ? 1 :
                Convert.ToDouble(item.Substring(0, item.Length - item.Substring(item.IndexOf('x')).Length));
            }
            else if (!item.Contains('x'))
            {
                this.power = 0;
                this.x = false;
                this.prefix *= item == "" ? 1 : Convert.ToDouble(item);
            }
            else if (item.Contains('x'))
            {
                this.power = 1;
                this.x = true;
                this.prefix *= item == "x" ? 1 : Convert.ToDouble(item.Replace("x", ""));
            }
            if (item.Contains('^'))
            {
                this.power = Convert.ToInt32(item.Substring(item.IndexOf('^') + 1));
            }

        }
        public override string ToString()
        {
            return this.prefix == 0 ? "" : (this.prefix != 1 ? this.prefix == -1 ? "-" : this.prefix.ToString() : "") +
             (this.x ? "x" : "") +
              (this.power > 1 ? "^" + this.power : "");
        }
    }

}