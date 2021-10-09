using System.Collections.Generic;

namespace vtb_backend.Models
{
    public class Lesson
    {
        public int stepAmount  { get; set; }
        public List<Step> steps  { get; set; }
    }
    public class Step
    {
        public int stepId  { get; set; }
        public string stepText  { get; set; }
        public List<Button> buttons  { get; set; }
    }
}