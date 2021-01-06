using MifParser.Interfaces;

namespace MifParser.Drivers
{
    class TimeDriver
    {
        IMifScalarField Ms { get; set; }
        IMifVectorField m0 { get; set; }
        string Evolver { get; set; }
        string Mesh { get; set; }
        double StoppingTime { get; set; }
        int StageIterationLimit { get; set; }
        int Total_iteration_limit {get;set;}
        int StageCount { get; set; }
        int StageCountCheck { get; set; }
        string BaseFileName { get; set; }
        string ScalarOutputFormat { get; set; }
        //string VectorFieldOutputFormat { style precision }??
        public TimeDriver()
        {

        }
    }
}
