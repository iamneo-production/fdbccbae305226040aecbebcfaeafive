using System.ComponentModel.DataAnnotations;
using System;
namespace dotnetapp.Models{
public class Student {
    public int StudentID {get;set;}
    [MaxLength(30)]
    public string Name {get;set;}
    [MaxLength(50)]
    public string Email {get;set;}
    public int BatchID {get;set;}
    public Batch Batch {get;set;}
}}