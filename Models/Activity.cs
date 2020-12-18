using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; //let us use List<movie>

namespace BeltExam.Models
{
    public class Activity
    {
        [Key] //signifies that this is the unique identifier(example: Like a ssn number)
        public int ActivityId { get; set; }

        [Required(ErrorMessage = "Please provide this info to procced.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Time")]
        public string Time { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [DataType(DataType.Duration)]
        [Display(Name = "Duration")]
        public int Duration { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please provide this info to procced.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public int UserId { get; set; }

        public User PlanedBy { get; set; }

        //many to many, add after creating Middle Model
        //one activity have a list of participants
        public List<Participant> Participants { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}