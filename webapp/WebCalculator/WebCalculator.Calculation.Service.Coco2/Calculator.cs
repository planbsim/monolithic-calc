using System;
using System.IO;
using Lex = CalcLex;
using Syn = CalcSyn;

namespace WebCalculator.Calculation.Service.Coco2
{
    public class Calculator : ICalculator
    {
        public int Calculate(string expression)
        {
            int result = 0;

            // --- install modules ---
            Utils.InstallModule("Utils", new Utils.ModuleMethodDelegate(Utils.UtilsMethod));
            Utils.InstallModule("Sets", new Utils.ModuleMethodDelegate(Sets.SetsMethod));
            Utils.InstallModule("Errors", new Utils.ModuleMethodDelegate(Errors.ErrorsMethod));

            Utils.InstallModule("CalcLex", new Utils.ModuleMethodDelegate(CalcLex.CalcLexMethod));
            Utils.InstallModule("CalcSem", new Utils.ModuleMethodDelegate(CalcSem.CalcSemMethod));
            Utils.InstallModule("CalcSyn", new Utils.ModuleMethodDelegate(CalcSyn.CalcSynMethod));

            // --- initialize modules ---
            Utils.Modules(Utils.ModuleAction.initModule);

            //Errors.PushAbortMethod(new Errors.AbortMethod(Abort));

            int error = CompileFile(expression);

            result = CalcSem.LastCalcResult;

            return result;
        }

        private int CompileFile(String expression)
        {
           
            try
            {
                Lex.src = new StringReader(expression);
                //Console.WriteLine("parsing ...");
                Syn.Parse();
                Lex.src.Close();
                Lex.src.Dispose();
                Lex.src = null;
            }
            finally
            {
                if (Lex.src != null)
                {
                    Lex.src.Close();
                    Lex.src.Dispose();
                    Lex.src = null;
                } // if
                Utils.Modules(Utils.ModuleAction.resetModule);
            } // try/finally to make sure srcFs and srcReader are closed
            return Utils.EXIT_SUCCESS;
        } // CompileFile
    }
}
