
namespace ShuntingYardCalc
{




    internal partial class Program
    {

  

        static void Main(string[] args)
        {




            while (true)
            {
                int i = 1;

                string? inp = Console.ReadLine();
                List<Tokenizer.Token> ls = Tokenizer.Tokenize(inp);


                List<string> hay = Parser.ToRPN(ls);

                Console.WriteLine(Evaluator.EvalRPN(hay));



            }



        }
    }
}
