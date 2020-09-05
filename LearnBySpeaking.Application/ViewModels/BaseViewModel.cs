using System;

namespace LearnBySpeaking.Application.ViewModels
{
    public class BaseViewModel
    {
        // you can change this to RefKey to accomodate with the Biteg Software standard
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUser { get; set; }
    }
}