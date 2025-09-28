using System.Text;

namespace test1
{
    public class TestA
    {
        private readonly Dictionary<int, string> _rules;
        public TestA()
        {
            _rules = new Dictionary<int, string>();
        }

        public void AddRule(int input, string output)
        {
            _rules.Add(input, output);
        }

        public void question4(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var rule in _rules)
                {
                    if (i % rule.Key == 0)
                    {
                        sb.Append(rule.Value);
                    }
                }
                if (sb.Length == 0)
                {
                    sb.Append(i);
                }
                Console.WriteLine(sb.ToString());
            }
        }

        public void question1(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                StringBuilder sb = new StringBuilder();
                if (i % 3 == 0)
                {
                    sb.Append("foo");
                }
                if (i % 5 == 0)
                {
                    sb.Append("bar");
                }
                if(i % 3 != 0 && i%5!=0)
                {
                    sb.Append(i);
                }
                Console.WriteLine(sb.ToString());
            }
        }

        public void question2(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                StringBuilder sb = new StringBuilder();
                if (i % 3 == 0)
                {
                    sb.Append("foo");
                }
                if (i % 5 == 0)
                {
                    sb.Append("bar");
                }
                if (i % 7 == 0)
                {
                    sb.Append("jazz");
                }
                if (i % 3 != 0 && i % 5 != 0 && i%7!=0)
                {
                    sb.Append(i);
                }
                Console.WriteLine(sb.ToString());
            }
        }

        public void question3(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                StringBuilder sb = new StringBuilder();
                if (i % 3 == 0)
                {
                    sb.Append("foo");
                }
                if (i % 4 == 0)
                {
                    sb.Append("baz");
                }
                if (i % 5 == 0)
                {
                    sb.Append("bar");
                }
                if (i % 7 == 0)
                {
                    sb.Append("jazz");
                }
                if (i % 9 == 0)
                {
                    sb.Append("huzz");
                }
                if (i % 3 != 0 && i % 4 != 0 && i % 5 != 0 && i % 7 != 0 && i % 9 != 0)
                {
                    sb.Append(i);
                }
                Console.WriteLine(sb.ToString());
            }
        }

    }
}
