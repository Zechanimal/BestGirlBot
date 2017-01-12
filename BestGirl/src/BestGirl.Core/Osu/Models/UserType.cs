using System.ComponentModel;

namespace BestGirl.Core.Osu.Models
{
	public enum UserType
	{
		[Description("id")]
		Id,
		[Description("string")]
		Username,
		[Description("any")]
		Any
	}
}
