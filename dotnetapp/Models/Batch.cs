public class Batch{
    public int BatchID {get;set;}
    public DateTime StartTime {get;set;}
    public DateTime EndTime {get;set;}
    public int Capacity {get;set;}
    public ICollection<Student> Students {get;set;}
}