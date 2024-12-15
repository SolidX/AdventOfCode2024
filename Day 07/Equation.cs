namespace Day_07
{
    internal class Equation
    {
        public long TestValue {get; set;}
        public List<int> Operands { get; private set; }
        private List<char> Operators { get; set; }

        public Equation(long tVal, IEnumerable<int> operands)
        {
            TestValue = tVal;
            Operands = operands.ToList();
            Operators = new List<char>(Operands.Count - 1);
        }

        public bool Passes()
        {
            if (Operators.Any())
                return TestValue == Evaluate();

            var possiblities = new List<Equation>();
            
            var p = Clone();
            p.Operators.Add('+');
            possiblities.Add(p);

            var q = Clone();
            q.Operators.Add('*');
            possiblities.Add(q);

            for (var i = 1; i < Operands.Count - 1; i++)
            {
                var tmp = new List<Equation>();

                for (int j = 0; j < possiblities.Count; j++)
                {
                    var a = possiblities[j].Clone();
                    a.Operators.Add('+');
                    tmp.Add(a);
                    var b = possiblities[j].Clone();
                    b.Operators.Add('*');
                    tmp.Add(b);
                }

                possiblities = tmp;
            }

            return possiblities.Any(x => x.TestValue == x.Evaluate());
        }

        public bool PassesWithConcatenation()
        {
            if (Operators.Any())
                return TestValue == Evaluate();

            var possiblities = new List<Equation>();

            var t = Clone();
            t.Operators.Add('+');
            possiblities.Add(t);

            var u = Clone();
            u.Operators.Add('*');
            possiblities.Add(u);

            var v = Clone();
            v.Operators.Add('|');
            possiblities.Add(v);

            for (var i = 1; i < Operands.Count - 1; i++)
            {
                var tmp = new List<Equation>();

                for (int j = 0; j < possiblities.Count; j++)
                {
                    var a = possiblities[j].Clone();
                    a.Operators.Add('+');
                    tmp.Add(a);

                    var b = possiblities[j].Clone();
                    b.Operators.Add('*');
                    tmp.Add(b);

                    var c = possiblities[j].Clone();
                    c.Operators.Add('|');
                    tmp.Add(c);
                }

                possiblities = tmp;
            }

            return possiblities.Any(x => x.TestValue == x.Evaluate());
        }

        public long? Evaluate()
        {
            if (!Operators.Any() || !Operands.Any())
                return null;

            var result = (long)Operands.First();
            for (var i = 0; i < Operators.Count; i++)
            {
                switch (Operators[i])
                {
                    case '+':
                        result += Operands[i + 1];
                        break;
                    case '*':
                        result *= Operands[i + 1];
                        break;
                    case '|':
                        result = Int64.Parse($"{result}{Operands[i + 1]}");
                        break;
                    default:
                        throw new ArgumentException($"Invalid operator: '{Operators[i]}'");
                }
            }

            return result;
        }

        public override string ToString()
        {
            if (Operators.Count == 0)
                return String.Join('?', Operands);

            if (Operands.Count() == 0)
                return string.Empty;

            var str = Operands.First().ToString();
            for (int i = 0; i < Operators.Count; i++)
                str += $"{Operators[i]}{Operands[i + 1]}";
            return str;
        }

        public Equation Clone()
        {
            var operands = new List<int>(Operands.Count);
            operands.AddRange(Operands);

            var operators = new List<char>(Operators.Count);
            operators.AddRange(Operators);

            var eq = new Equation(TestValue, operands);
            eq.Operators = operators;

            return eq;
        }
    }
}
