using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Viddly3.Models;

namespace Viddly3.ViewModels
{
    public class CustomerFormViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name= "Is Subscribed To NewsLetter")]
        public bool IsSubscribedToNewsLetter { get; set; }

        [Display(Name="Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Display(Name = "Membership Type")]
        public short MembershipTypeId { get; set; }

        public IEnumerable<MembershipType> MembershipTypes { get; set; }

        public CustomerFormViewModel()
        {
            Id = 0;
        }
        public CustomerFormViewModel(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            MembershipTypeId = customer.MembershipTypeId;
            BirthDate = customer.BirthDate;
        }


        public string Title { get
            {
                if (Id != 0)
                    return "Edit Customer";
                return "New Customer";
            } }

    }
}