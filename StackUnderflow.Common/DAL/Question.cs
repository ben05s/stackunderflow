//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StackUnderflow.Common.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Question
    {
        public Question()
        {
            this.Answer = new HashSet<Answer>();
        }
    
        public int question_id { get; set; }
        public int user_id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public byte[] created { get; set; }
    
        public virtual User User { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }
    }
}