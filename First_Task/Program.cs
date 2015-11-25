using Adam.Core;
using Adam.Core.Classifications;
using Adam.Core.Records;
using Adam.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application();

            LogOnStatus status = app.LogOn("LUXDAM", "Eduard_Pasmetukhau", "P2ssw0rd!");

            if (status == LogOnStatus.LoggedOn)
            {
                Console.WriteLine("Ok");
            }
            else Console.WriteLine(status);

            Classification c = new Classification(app);
            ClassificationHelper ch = new ClassificationHelper(app);
            Guid? rootGuid= ch.GetId(new SearchExpression("name = 'Luxottica Content*'"));
            
            Console.WriteLine("List of classification with parent Luxottica Content");
            //Console.ReadKey();


            if (rootGuid != null)
            {
                ClassificationCollection classCollections = new ClassificationCollection(app);
                classCollections.Load(new SearchExpression(String.Format("parent = '{0}'", rootGuid)));
                foreach (Classification classification in classCollections)
                {
                    Console.WriteLine(classification.Label + " Guid: "+classification.Id);
                }
            }
            Console.WriteLine();
            Console.ReadKey();
            Console.WriteLine("First 10 Guids of Records in classification Luxottica Content");

            RecordCollection rc = new RecordCollection(app);
            rc.Load(new SearchExpression(String.Format("classification = '{0}'", rootGuid)));
            

            foreach (Record r in rc.Take<Record>(10))
            {
                Console.WriteLine(r.Id);
            }

            Console.ReadKey();

            
        }
    }

    
}
