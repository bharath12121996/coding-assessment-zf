using CodingAssessment.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssessment.Domain.Interfaces
{
    public interface ISmtpClientRepository
    {
        Task SendEmailAsync(string emailObject);
    }
}
