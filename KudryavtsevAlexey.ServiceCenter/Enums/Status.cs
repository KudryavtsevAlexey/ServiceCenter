using System.ComponentModel.DataAnnotations;

namespace KudryavtsevAlexey.ServiceCenter.Enums
{
	public enum Status
	{
		Accepted,
		[Display(Name ="Under repair")]
		UnderRepair,
		[Display(Name ="Awaiting Payment")]
		AwaitingPayment,
		[Display(Name ="Paid up")]
		PaidUp,
		[Display(Name ="Ready to issue")]
		ReadyToIssue,
		Issued,
	}
}
