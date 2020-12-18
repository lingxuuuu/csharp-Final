using System;
using System.ComponentModel.DataAnnotations;


namespace BeltExam.Models
{
    public class Participant
    {  
        public int ParticipantId { get; set; } //new modelId
        public int? UserId { get; set; }
        public int ActivityId { get; set; }
        public User User { get; set; }
        public Activity Activity { get; set; }
    }

}