using System;
using System.Collections.Generic;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using numl.Model;
using numl.Supervised.DecisionTree;
using ContestManager.Modules;

namespace ContestManager
{
    class Program
    {

        // var randomForestClassifier;
        // var logisticRegressionClassifier;
        // var adaBoostClassifier;



        static void Main(string[] args)
        {
            // Step One Parse ARLEM document into object
            Console.WriteLine("Choose your workplace");
            int WorkplaceId = int.Parse(Console.ReadLine());

            ARLEMDecipher.ARLEMDecipher aRLEMDecipher = new ARLEMDecipher.ARLEMDecipher("127.0.0.1:8080");
            if (!aRLEMDecipher.LoadWorkplace(WorkplaceId))
            {
                Console.ReadKey();
                return;
            }
            int[] availableActivities = aRLEMDecipher.AvailableActivites();
            if (availableActivities.Length < 0)
            {
                Console.ReadKey();
                return;
            }
 
            Console.WriteLine("Please Select an activity");
            aRLEMDecipher.Workplace.Activities.ForEach(activity => {
                Console.WriteLine(aRLEMDecipher.Workplace.Activities.IndexOf(activity) + ": " + activity.Name);
            });
            int ActivityId = int.Parse(Console.ReadLine());
            aRLEMDecipher.LoadActivity(availableActivities[ActivityId]);
            List<string> Sensors = aRLEMDecipher.AvailableSensors();

            List<String> Modules = aRLEMDecipher.RequiredModules();
            List<IModule> ModuleToExecute = new List<IModule>();

            Modules.ForEach(moduleName =>
            {
                IModule BestFitModule = null;
                IModule.ModuleMap[moduleName].ForEach(module =>
                {
                    if(module.IsCalculatable(Sensors))
                    {
                        BestFitModule = module;
                    }
                });
                if(BestFitModule == null)
                {
                    Console.WriteLine(moduleName + " can't be calcualted with available sensors");
                }
                else
                {
                    ModuleToExecute.Add(BestFitModule);
                }
            });


            Console.WriteLine("Activity requirs following sensors. Please connect your sensors and press any key to continue.");
            ModuleToExecute.ForEach(module =>
            {
                module.RequiredSensors.ForEach(urn => Console.WriteLine(urn));
                Console.WriteLine();
            });
            Console.ReadKey();
            ModuleToExecute.ForEach(module =>
            {
                // No need as module already trainted
                // module.Train();
                module.Config((dynamic output) =>
                {
                    if(output == 1)
                    {
                        module.Stop();
                    }
                    Console.WriteLine(output);
                });
                module.Start();
            });

        }


        static void Train()
        {
            // randomForestClassifier = new RandomForestClassifier();
            // randomForestClassifier.Add(new CSVReader("data.txt").AsTrainingData());
            // randomForestClassifier.Add(new CSVReader("label.txt").AsTrainingLabel());
            // randomForestClassifier.Add(new CSVReader("test.txt").AsTestData());
            // randomForestClassifier.Add(new CSVReader("testlabel.txt").AsTestLabel());
            // randomForestClassifier.Train(8, 3, 0);
            // randomForestClassifier.Save("randomForest.xml");

            // logisticRegressionClassifier = new LogisticRegressionClassifier("lgr.xml");
        }

        static void MLTest()
        {
            var pipeline = new LearningPipeline();
            pipeline.Add(new TextLoader("C:\\Users\\ali\\Downloads\\machinelearning-master\\machinelearning-master\\test\\data\\iris.txt").CreateFrom<SentimentData>(separator: ','));
            pipeline.Add(new TextFeaturizer("Features", "SentimentText"));
            pipeline.Add(new FastTreeBinaryClassifier());
            var model = pipeline.Train<SentimentData, SentimentPrediction>();
        }
        

       

        static void NumlTest()
        {
            Console.WriteLine("Hello World!");
            var description = Descriptor.Create<Iris>();
            Console.WriteLine(description);
            var generator = new DecisionTreeGenerator();
            var data = Iris.Load();
            var model = generator.Generate(description, data);
            Console.WriteLine("Generated model:");
            Console.WriteLine(model);
        }

        /*static void AccordTest ()
        {
            int[] ysequence = new int[] { 1, 2, 3, 2 };

            // this generates the correct Y for a given X
            int CalcY(int x) => ysequence[(x - 1) % 4];

            // this generates some inputs - just a few differnt mod of x
            int[] CalcInputs(int x) => new int[] { x % 2, x % 3, x % 4, x % 5, x % 6 };


            // build the training data set
            int numtrainingcases = 12;
            int[][] inputs = new int[numtrainingcases][];
            int[] outputs = new int[numtrainingcases];

            Console.WriteLine("\t\t\t\t x \t y");
            for (int x = 1; x <= numtrainingcases; x++)
            {
                int y = CalcY(x);
                inputs[x - 1] = CalcInputs(x);
                outputs[x - 1] = y;
                Console.WriteLine("TrainingData \t " + x + "\t " + y);
            }

            // define how many values each input can have
            DecisionVariable[] attributes =
            {
            new DecisionVariable("Mod2",2),
            new DecisionVariable("Mod3",3),
            new DecisionVariable("Mod4",4),
            new DecisionVariable("Mod5",5),
            new DecisionVariable("Mod6",6)
        };

            // define how many outputs (+1 only because y doesn't use zero)
            int classCount = outputs.Max() + 1;

            // create the tree
            DecisionTree tree = new DecisionTree(attributes, classCount);

            // Create a new instance of the ID3 algorithm
            ID3Learning id3learning = new ID3Learning(tree);

            // Learn the training instances! Populates the tree
            id3learning.Learn(inputs, outputs);

            Console.WriteLine();
            // now try to predict some cases that werent in the training data
            for (int x = numtrainingcases + 1; x <= 2 * numtrainingcases; x++)
            {
                int[] query = CalcInputs(x);

                int answer = tree.Decide(query); // makes the prediction

                Console.WriteLine("Prediction \t\t " + x + "\t " + answer);
            }

            Console.WriteLine("Hello World!");
            Console.ReadKey();        
        }*/
    }

    public class Iris
    {
        [Feature]
        public decimal SepalLength { get; set; }
        [Feature]
        public decimal SepalWidth { get; set; }
        [Feature]
        public decimal PetalLength { get; set; }
        [Feature]
        public decimal PetalWidth { get; set; }
        [StringLabel]
        public string Class { get; set; }

        public static Iris[] Load()
        {
            return new Iris[]
            {
                new Iris { SepalLength = 5.1m, SepalWidth = 3.5m, PetalLength = 1.4m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.9m, SepalWidth = 3m, PetalLength = 1.4m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.7m, SepalWidth = 3.2m, PetalLength = 1.3m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.6m, SepalWidth = 3.1m, PetalLength = 1.5m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5m, SepalWidth = 3.6m, PetalLength = 1.4m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.4m, SepalWidth = 3.9m, PetalLength = 1.7m, PetalWidth = 0.4m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.6m, SepalWidth = 3.4m, PetalLength = 1.4m, PetalWidth = 0.3m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5m, SepalWidth = 3.4m, PetalLength = 1.5m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.4m, SepalWidth = 2.9m, PetalLength = 1.4m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.9m, SepalWidth = 3.1m, PetalLength = 1.5m, PetalWidth = 0.1m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.4m, SepalWidth = 3.7m, PetalLength = 1.5m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.8m, SepalWidth = 3.4m, PetalLength = 1.6m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.8m, SepalWidth = 3m, PetalLength = 1.4m, PetalWidth = 0.1m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.3m, SepalWidth = 3m, PetalLength = 1.1m, PetalWidth = 0.1m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.8m, SepalWidth = 4m, PetalLength = 1.2m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.7m, SepalWidth = 4.4m, PetalLength = 1.5m, PetalWidth = 0.4m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.4m, SepalWidth = 3.9m, PetalLength = 1.3m, PetalWidth = 0.4m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.1m, SepalWidth = 3.5m, PetalLength = 1.4m, PetalWidth = 0.3m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.7m, SepalWidth = 3.8m, PetalLength = 1.7m, PetalWidth = 0.3m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.1m, SepalWidth = 3.8m, PetalLength = 1.5m, PetalWidth = 0.3m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.4m, SepalWidth = 3.4m, PetalLength = 1.7m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.1m, SepalWidth = 3.7m, PetalLength = 1.5m, PetalWidth = 0.4m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.6m, SepalWidth = 3.6m, PetalLength = 1m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.1m, SepalWidth = 3.3m, PetalLength = 1.7m, PetalWidth = 0.5m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.8m, SepalWidth = 3.4m, PetalLength = 1.9m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5m, SepalWidth = 3m, PetalLength = 1.6m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5m, SepalWidth = 3.4m, PetalLength = 1.6m, PetalWidth = 0.4m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.2m, SepalWidth = 3.5m, PetalLength = 1.5m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.2m, SepalWidth = 3.4m, PetalLength = 1.4m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.7m, SepalWidth = 3.2m, PetalLength = 1.6m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.8m, SepalWidth = 3.1m, PetalLength = 1.6m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.4m, SepalWidth = 3.4m, PetalLength = 1.5m, PetalWidth = 0.4m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.2m, SepalWidth = 4.1m, PetalLength = 1.5m, PetalWidth = 0.1m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.5m, SepalWidth = 4.2m, PetalLength = 1.4m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.9m, SepalWidth = 3.1m, PetalLength = 1.5m, PetalWidth = 0.1m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5m, SepalWidth = 3.2m, PetalLength = 1.2m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.5m, SepalWidth = 3.5m, PetalLength = 1.3m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.9m, SepalWidth = 3.1m, PetalLength = 1.5m, PetalWidth = 0.1m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.4m, SepalWidth = 3m, PetalLength = 1.3m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.1m, SepalWidth = 3.4m, PetalLength = 1.5m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5m, SepalWidth = 3.5m, PetalLength = 1.3m, PetalWidth = 0.3m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.5m, SepalWidth = 2.3m, PetalLength = 1.3m, PetalWidth = 0.3m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.4m, SepalWidth = 3.2m, PetalLength = 1.3m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5m, SepalWidth = 3.5m, PetalLength = 1.6m, PetalWidth = 0.6m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.1m, SepalWidth = 3.8m, PetalLength = 1.9m, PetalWidth = 0.4m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.8m, SepalWidth = 3m, PetalLength = 1.4m, PetalWidth = 0.3m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.1m, SepalWidth = 3.8m, PetalLength = 1.6m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 4.6m, SepalWidth = 3.2m, PetalLength = 1.4m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5.3m, SepalWidth = 3.7m, PetalLength = 1.5m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 5m, SepalWidth = 3.3m, PetalLength = 1.4m, PetalWidth = 0.2m, Class = "Iris-setosa" },
                new Iris { SepalLength = 7m, SepalWidth = 3.2m, PetalLength = 4.7m, PetalWidth = 1.4m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.4m, SepalWidth = 3.2m, PetalLength = 4.5m, PetalWidth = 1.5m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.9m, SepalWidth = 3.1m, PetalLength = 4.9m, PetalWidth = 1.5m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.5m, SepalWidth = 2.3m, PetalLength = 4m, PetalWidth = 1.3m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.5m, SepalWidth = 2.8m, PetalLength = 4.6m, PetalWidth = 1.5m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.7m, SepalWidth = 2.8m, PetalLength = 4.5m, PetalWidth = 1.3m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.3m, SepalWidth = 3.3m, PetalLength = 4.7m, PetalWidth = 1.6m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 4.9m, SepalWidth = 2.4m, PetalLength = 3.3m, PetalWidth = 1m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.6m, SepalWidth = 2.9m, PetalLength = 4.6m, PetalWidth = 1.3m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.2m, SepalWidth = 2.7m, PetalLength = 3.9m, PetalWidth = 1.4m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5m, SepalWidth = 2m, PetalLength = 3.5m, PetalWidth = 1m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.9m, SepalWidth = 3m, PetalLength = 4.2m, PetalWidth = 1.5m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6m, SepalWidth = 2.2m, PetalLength = 4m, PetalWidth = 1m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.1m, SepalWidth = 2.9m, PetalLength = 4.7m, PetalWidth = 1.4m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.6m, SepalWidth = 2.9m, PetalLength = 3.6m, PetalWidth = 1.3m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.7m, SepalWidth = 3.1m, PetalLength = 4.4m, PetalWidth = 1.4m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.6m, SepalWidth = 3m, PetalLength = 4.5m, PetalWidth = 1.5m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.8m, SepalWidth = 2.7m, PetalLength = 4.1m, PetalWidth = 1m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.2m, SepalWidth = 2.2m, PetalLength = 4.5m, PetalWidth = 1.5m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.6m, SepalWidth = 2.5m, PetalLength = 3.9m, PetalWidth = 1.1m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.9m, SepalWidth = 3.2m, PetalLength = 4.8m, PetalWidth = 1.8m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.1m, SepalWidth = 2.8m, PetalLength = 4m, PetalWidth = 1.3m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.3m, SepalWidth = 2.5m, PetalLength = 4.9m, PetalWidth = 1.5m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.1m, SepalWidth = 2.8m, PetalLength = 4.7m, PetalWidth = 1.2m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.4m, SepalWidth = 2.9m, PetalLength = 4.3m, PetalWidth = 1.3m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.6m, SepalWidth = 3m, PetalLength = 4.4m, PetalWidth = 1.4m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.8m, SepalWidth = 2.8m, PetalLength = 4.8m, PetalWidth = 1.4m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.7m, SepalWidth = 3m, PetalLength = 5m, PetalWidth = 1.7m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6m, SepalWidth = 2.9m, PetalLength = 4.5m, PetalWidth = 1.5m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.7m, SepalWidth = 2.6m, PetalLength = 3.5m, PetalWidth = 1m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.5m, SepalWidth = 2.4m, PetalLength = 3.8m, PetalWidth = 1.1m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.5m, SepalWidth = 2.4m, PetalLength = 3.7m, PetalWidth = 1m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.8m, SepalWidth = 2.7m, PetalLength = 3.9m, PetalWidth = 1.2m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6m, SepalWidth = 2.7m, PetalLength = 5.1m, PetalWidth = 1.6m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.4m, SepalWidth = 3m, PetalLength = 4.5m, PetalWidth = 1.5m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6m, SepalWidth = 3.4m, PetalLength = 4.5m, PetalWidth = 1.6m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.7m, SepalWidth = 3.1m, PetalLength = 4.7m, PetalWidth = 1.5m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.3m, SepalWidth = 2.3m, PetalLength = 4.4m, PetalWidth = 1.3m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.6m, SepalWidth = 3m, PetalLength = 4.1m, PetalWidth = 1.3m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.5m, SepalWidth = 2.5m, PetalLength = 4m, PetalWidth = 1.3m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.5m, SepalWidth = 2.6m, PetalLength = 4.4m, PetalWidth = 1.2m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.1m, SepalWidth = 3m, PetalLength = 4.6m, PetalWidth = 1.4m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.8m, SepalWidth = 2.6m, PetalLength = 4m, PetalWidth = 1.2m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5m, SepalWidth = 2.3m, PetalLength = 3.3m, PetalWidth = 1m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.6m, SepalWidth = 2.7m, PetalLength = 4.2m, PetalWidth = 1.3m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.7m, SepalWidth = 3m, PetalLength = 4.2m, PetalWidth = 1.2m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.7m, SepalWidth = 2.9m, PetalLength = 4.2m, PetalWidth = 1.3m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.2m, SepalWidth = 2.9m, PetalLength = 4.3m, PetalWidth = 1.3m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.1m, SepalWidth = 2.5m, PetalLength = 3m, PetalWidth = 1.1m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 5.7m, SepalWidth = 2.8m, PetalLength = 4.1m, PetalWidth = 1.3m, Class = "Iris-versicolor" },
                new Iris { SepalLength = 6.3m, SepalWidth = 3.3m, PetalLength = 6m, PetalWidth = 2.5m, Class = "Iris-virginica" },
                new Iris { SepalLength = 5.8m, SepalWidth = 2.7m, PetalLength = 5.1m, PetalWidth = 1.9m, Class = "Iris-virginica" },
                new Iris { SepalLength = 7.1m, SepalWidth = 3m, PetalLength = 5.9m, PetalWidth = 2.1m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.3m, SepalWidth = 2.9m, PetalLength = 5.6m, PetalWidth = 1.8m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.5m, SepalWidth = 3m, PetalLength = 5.8m, PetalWidth = 2.2m, Class = "Iris-virginica" },
                new Iris { SepalLength = 7.6m, SepalWidth = 3m, PetalLength = 6.6m, PetalWidth = 2.1m, Class = "Iris-virginica" },
                new Iris { SepalLength = 4.9m, SepalWidth = 2.5m, PetalLength = 4.5m, PetalWidth = 1.7m, Class = "Iris-virginica" },
                new Iris { SepalLength = 7.3m, SepalWidth = 2.9m, PetalLength = 6.3m, PetalWidth = 1.8m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.7m, SepalWidth = 2.5m, PetalLength = 5.8m, PetalWidth = 1.8m, Class = "Iris-virginica" },
                new Iris { SepalLength = 7.2m, SepalWidth = 3.6m, PetalLength = 6.1m, PetalWidth = 2.5m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.5m, SepalWidth = 3.2m, PetalLength = 5.1m, PetalWidth = 2m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.4m, SepalWidth = 2.7m, PetalLength = 5.3m, PetalWidth = 1.9m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.8m, SepalWidth = 3m, PetalLength = 5.5m, PetalWidth = 2.1m, Class = "Iris-virginica" },
                new Iris { SepalLength = 5.7m, SepalWidth = 2.5m, PetalLength = 5m, PetalWidth = 2m, Class = "Iris-virginica" },
                new Iris { SepalLength = 5.8m, SepalWidth = 2.8m, PetalLength = 5.1m, PetalWidth = 2.4m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.4m, SepalWidth = 3.2m, PetalLength = 5.3m, PetalWidth = 2.3m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.5m, SepalWidth = 3m, PetalLength = 5.5m, PetalWidth = 1.8m, Class = "Iris-virginica" },
                new Iris { SepalLength = 7.7m, SepalWidth = 3.8m, PetalLength = 6.7m, PetalWidth = 2.2m, Class = "Iris-virginica" },
                new Iris { SepalLength = 7.7m, SepalWidth = 2.6m, PetalLength = 6.9m, PetalWidth = 2.3m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6m, SepalWidth = 2.2m, PetalLength = 5m, PetalWidth = 1.5m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.9m, SepalWidth = 3.2m, PetalLength = 5.7m, PetalWidth = 2.3m, Class = "Iris-virginica" },
                new Iris { SepalLength = 5.6m, SepalWidth = 2.8m, PetalLength = 4.9m, PetalWidth = 2m, Class = "Iris-virginica" },
                new Iris { SepalLength = 7.7m, SepalWidth = 2.8m, PetalLength = 6.7m, PetalWidth = 2m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.3m, SepalWidth = 2.7m, PetalLength = 4.9m, PetalWidth = 1.8m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.7m, SepalWidth = 3.3m, PetalLength = 5.7m, PetalWidth = 2.1m, Class = "Iris-virginica" },
                new Iris { SepalLength = 7.2m, SepalWidth = 3.2m, PetalLength = 6m, PetalWidth = 1.8m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.2m, SepalWidth = 2.8m, PetalLength = 4.8m, PetalWidth = 1.8m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.1m, SepalWidth = 3m, PetalLength = 4.9m, PetalWidth = 1.8m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.4m, SepalWidth = 2.8m, PetalLength = 5.6m, PetalWidth = 2.1m, Class = "Iris-virginica" },
                new Iris { SepalLength = 7.2m, SepalWidth = 3m, PetalLength = 5.8m, PetalWidth = 1.6m, Class = "Iris-virginica" },
                new Iris { SepalLength = 7.4m, SepalWidth = 2.8m, PetalLength = 6.1m, PetalWidth = 1.9m, Class = "Iris-virginica" },
                new Iris { SepalLength = 7.9m, SepalWidth = 3.8m, PetalLength = 6.4m, PetalWidth = 2m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.4m, SepalWidth = 2.8m, PetalLength = 5.6m, PetalWidth = 2.2m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.3m, SepalWidth = 2.8m, PetalLength = 5.1m, PetalWidth = 1.5m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.1m, SepalWidth = 2.6m, PetalLength = 5.6m, PetalWidth = 1.4m, Class = "Iris-virginica" },
                new Iris { SepalLength = 7.7m, SepalWidth = 3m, PetalLength = 6.1m, PetalWidth = 2.3m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.3m, SepalWidth = 3.4m, PetalLength = 5.6m, PetalWidth = 2.4m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.4m, SepalWidth = 3.1m, PetalLength = 5.5m, PetalWidth = 1.8m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6m, SepalWidth = 3m, PetalLength = 4.8m, PetalWidth = 1.8m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.9m, SepalWidth = 3.1m, PetalLength = 5.4m, PetalWidth = 2.1m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.7m, SepalWidth = 3.1m, PetalLength = 5.6m, PetalWidth = 2.4m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.9m, SepalWidth = 3.1m, PetalLength = 5.1m, PetalWidth = 2.3m, Class = "Iris-virginica" },
                new Iris { SepalLength = 5.8m, SepalWidth = 2.7m, PetalLength = 5.1m, PetalWidth = 1.9m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.8m, SepalWidth = 3.2m, PetalLength = 5.9m, PetalWidth = 2.3m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.7m, SepalWidth = 3.3m, PetalLength = 5.7m, PetalWidth = 2.5m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.7m, SepalWidth = 3m, PetalLength = 5.2m, PetalWidth = 2.3m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.3m, SepalWidth = 2.5m, PetalLength = 5m, PetalWidth = 1.9m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.5m, SepalWidth = 3m, PetalLength = 5.2m, PetalWidth = 2m, Class = "Iris-virginica" },
                new Iris { SepalLength = 6.2m, SepalWidth = 3.4m, PetalLength = 5.4m, PetalWidth = 2.3m, Class = "Iris-virginica" },
                new Iris { SepalLength = 5.9m, SepalWidth = 3m, PetalLength = 5.1m, PetalWidth = 1.8m, Class = "Iris-virginica" },
            };
        }
    }
}
