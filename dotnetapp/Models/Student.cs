using System.ComponentModel.DataAnnotations;
using System;
public class Student{
    public int StudentID {get;set;}
    [MaxLength(30)]
    public string Name {get;set;}
    public string Email {get;set;}
    public int BatchID {get;set;}
}