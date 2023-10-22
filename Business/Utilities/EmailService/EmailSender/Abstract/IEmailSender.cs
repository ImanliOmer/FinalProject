using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.EmailService.EmailSender.Abstract
{
	public interface IEmailSender
	{
		void SendEmail(Message message);
	}
}
