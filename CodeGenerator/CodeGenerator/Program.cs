using CodeGenerator.Library;

Console.WriteLine("Setup CodeGenerator and start it");
Generator gen = new Generator();

if (gen.SetUp())
{
    gen.Execute(); 
    Console.WriteLine("CodeGen success");
}
else
{
    Console.WriteLine("CodeGen failed");
}
