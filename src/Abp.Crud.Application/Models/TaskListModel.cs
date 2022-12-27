using Abp.Crud.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abp.Crud.Models;

public class TaskListModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [DisplayName("Title")]
    public string Title { get; set; }

    [Required]
    [DisplayName("Assigned To")]
    public string AssignedTo { get; set; }

    [Required]
    [DisplayName("Due Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true)]
    public DateTime DueDate { get; set; }

    [Required]
    public TaskListStatus? Status { get; set; }

    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}