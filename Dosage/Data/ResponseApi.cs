namespace Dosage.Data
{
    public class ResponseApi
    {
        public int Code { get; set; }
        public string? Status { get; set; }
        public string? Hint { get; set; }
        public string? Message { get; set; }
        public dynamic Value { get; set; }
    }
    public class DosageLimit
    {
        public string? Content { get; set; }
        public string? Duration { get; set; }
        public int Id { get; set; }
        public double? Max_dosage { get; set; }
        public string? Route { get; set; }
        public string? Substance { get; set; }
        public double? Time_duration { get; set; }
        public string? Unit { get; set; }
    }
    public class Dosage_limitModel
    {
        public int Id { get; set; }
        public string? Sub_name { get; set; }

        //public string? Content { get; set; }
        public string? Duration { get; set; }
        public double? Max_dosage { get; set; }
        public string? Route { get; set; }
        //public string? Substance { get; set; }
        public double? Time_duration { get; set; }
        public string? Unit { get; set; }

        public long Substance_id { get; set; }
        public int Duration_id { get; set; }
        //Mới

    }
    public class SubstanceModel
    {
        public long Id { get; set; }
        public string? Sub_name { get; set; }
        public int Unit_id { get; set; }
        public int Route_id { get; set; }
        public string? Content { get; set; }
        public string? Unit_name { get; set; }
        public string? Route_name { get; set; }

    }
    public class DurationModel
    {
        public int Id { get; set; }
        public string? Dura_name { get; set; }
    }
    public class RouteModel
    {
        public int Id { get; set; }
        public string? Route_name { get; set; }
    }
    public class UnitModel
    {
        public int Id { get; set; }
        public string? Unit_name { get; set; }
    }
    public class ResultSynchronized
    {
        public List<SynchronizedModel>? Success;
        public List<SynchronizedModel>? Failed;
    }
    public class SynchronizedModel
    {
        public string? Content { get; set; }
        public string? Display { get; set; }
        public string? Unit { get; set; }
        public string? Used { get; set; }
    }
    public class ResponLogin
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Work { get; set; }
        public string? Token { get; set; }
        public string? Display { get; set; }
        public string? Expired { get; set; }
        public string? User_name { get; set; }

        public bool Is_change_now { get; set; }
        public List<Functions>? Functions { get; set; }

    }
    public class Functions
    {
        public string? Code { get; set; }
        public bool Allow { get; set; }
        public bool Default { get; set; }
        public string? Display { get; set; }
    }
    public class AllergyModel
    {
        public int Id { get; set; }
        public long Substance_id { get; set; }
        public string? Sub_name { get; set; }
        public string? Unit { get; set; }
        public string? Route { get; set; }
        public string? Content { get; set; }
        public string? Allergy_date { get; set; }
        public string? Symptoms { get; set; }
        public string? Severity_level { get; set; }
        public string? Alternative_drugs { get; set; }
        public string? Treatment_measures { get; set; }

    }


}
